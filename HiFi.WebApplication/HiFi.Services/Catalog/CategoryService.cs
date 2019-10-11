using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HiFi.Common;
using HiFi.Common.ExcelModel;
using HiFi.Common.ViewModel;
using HiFi.Data.Models;
using HiFi.Repository;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace HiFi.Services.Catalog
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMemoryCache _memoryCache;
        public CategoryService(IRepository<Category> categoryRepository,IMemoryCache memoryCache)
        {
            _categoryRepository = categoryRepository;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Gets all categories from db
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="showHidden"></param>
        /// <param name="loadCacheableCopy"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<Category>> GetAllCategories(int storeId = 0, bool showHidden = false, bool loadCacheableCopy = true)
        {
            var data = await _categoryRepository.GetAll();
            foreach (var item in data)
            {
                if (item.Description.Length > 40)
                {
                    item.Description = item.Description.Substring(0, 40);
                }
            }
            return data;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var result = await _categoryRepository.GetById(id);
            return result;
        }

        public bool InsertCategory(Category category)
        {
            _categoryRepository.Insert(category);
            return true;
        }

        public bool UpdateCategory(Category category)
        {
            _categoryRepository.Update(category);
            return true;
        }

        public bool DeleteCategory(int categoryId)
        {
            _categoryRepository.Delete(categoryId);
            return true;
        }

        public async Task<bool> InsertCategorInBulk(List<CategoryImportExcel> categoryExcelList,string userId)
        {
            if (categoryExcelList.Count > 0)
            {
                try
                {
                    IList<Category> categoriesList = new List<Category>();
                    ApplicationUser applicationUser = _categoryRepository.GetApplicationUser(userId);
                    foreach (var item in categoryExcelList)
                    {
                        Category categoryExcel = new Category();
                        categoryExcel.CategoryName = item.CategoryName;
                        categoryExcel.Description = item.Description;
                        categoryExcel.DisplayOrder = item.DisplayOrder;
                        categoryExcel.IsActive = item.IsActive;
                        if (!string.IsNullOrEmpty(item.CreatedByUser))
                        {
                            //categoryExcel.ApplicationUser = _categoryRepository.GetApplicationUser(item.CreatedByUser);
                            categoryExcel.ApplicationUser = applicationUser;
                        }
                        else
                        {
                            categoryExcel.ApplicationUser = applicationUser;
                        }
                        categoryExcel.CreatedDate = DateTime.Now;
                        categoryExcel.UpdatedDate = DateTime.Now;
                        categoriesList.Add(categoryExcel);
                    }
                    return await _categoryRepository.BulkCreate(categoriesList);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return true;
        }

        public int GetProductCategoriesByProductId(int currentProductId)
        {
           return _categoryRepository.GetProductCategoriesByProductId(currentProductId);
        }

        public List<CategoryChildsCount> GetNoOfProductsAndSubCategoriesByCategories()
        {
            return _categoryRepository.GetNoOfProductsAndSubCategoriesByCategories();
        }

        public List<CategoryNavViewModel> GetCategoriesAndSubCategories()
        {
            var categoryNavViewModels = SetGetCategoriesAndSubCategoriesMemoryCache();
            return categoryNavViewModels;
            //return _categoryRepository.GetCategoriesAndSubCategories();
        }

        /// <summary>
        /// Gets & Sets CategoryNavList in Cache from Database.
        /// </summary>
        /// <returns></returns>
        private List<CategoryNavViewModel> SetGetCategoriesAndSubCategoriesMemoryCache()
        {
            try
            {
                //2
                string cacheKey = CacheKeys.CategoryNavListCache;
                List<CategoryNavViewModel> categoryList;

                //3: We will try to get the Cache data
                //If the data is present in cache the 
                //Condition will be true else it is false 
                if (!_memoryCache.TryGetValue(cacheKey, out categoryList))
                {
                    //4.fetch the data from the object
                    categoryList = _categoryRepository.GetCategoriesAndSubCategories();
                    //5.Save the received data in cache
                    _memoryCache.Set(cacheKey, categoryList,
                        new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(30)));
                    //_logger.LogInformation($"{cacheKey} updated from source.");
                }
                else
                {
                    categoryList = _memoryCache.Get(cacheKey) as List<CategoryNavViewModel>;
                    //_logger.LogInformation($"{cacheKey} retrieved from cache.");
                }
                return categoryList;
            }
            catch (Exception ex)
            {
                //_logger.LogError($"{ex.Message}");
                throw ex;
            }
        }
    }
}
