using System;
using System.Collections.Generic;
using System.Text;
using HiFi.Common.ExcelModel;
using HiFi.Data.Models;
using HiFi.Repository;

namespace HiFi.Services.Catalog
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        public virtual IEnumerable<Category> GetAllCategories(int storeId = 0, bool showHidden = false, bool loadCacheableCopy = true)
        {
            var data = _categoryRepository.GetAll();
            return data;
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

        public bool DeleteCategory(Category category)
        {
            _categoryRepository.Delete(category);
            return true;
        }

        public bool InsertCategorInBulk(List<CategoryImportExcel> categoryExcelList)
        {
            if (categoryExcelList.Count > 0)
            {
                try
                {
                    IList<Category> categoriesList = new List<Category>();
                    ApplicationUser applicationUser = _categoryRepository.GetApplicationUser();
                    foreach (var item in categoryExcelList)
                    {
                        Category categoryExcel = new Category();
                        categoryExcel.CategoryName = item.CategoryName;
                        categoryExcel.Description = item.Description;
                        categoryExcel.DisplayOrder = item.DisplayOrder;
                        if (!string.IsNullOrEmpty(item.CreatedByUser))
                        {
                            categoryExcel.ApplicationUser = _categoryRepository.GetApplicationUser(item.CreatedByUser);
                        }
                        else
                        {
                            categoryExcel.ApplicationUser = applicationUser;
                        }
                        categoryExcel.CreatedDate = DateTime.Now;
                        categoryExcel.UpdatedDate = DateTime.Now;
                        categoriesList.Add(categoryExcel);
                    }
                    return _categoryRepository.BulkCreate(categoriesList);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return true;
        }
    }
}
