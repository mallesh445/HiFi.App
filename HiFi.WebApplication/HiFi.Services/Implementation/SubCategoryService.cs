using HiFi.Common.ExcelModel;
using HiFi.Data.Models;
using HiFi.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiFi.Services.Implementation
{
    public class SubCategoryService: ISubCategoryService
    {
        private readonly IRepository<SubCategoryOne> _subCategoryRepository;

        private readonly IShoppingCartRepository _shoppingCartRepository;
        public SubCategoryService(IRepository<SubCategoryOne> subCategoryRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _subCategoryRepository = subCategoryRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }


        public virtual async Task<IEnumerable<SubCategoryOne>> GetAllSubCategories()
        {
            var data = await _subCategoryRepository.GetAll();
            return data;
        }

        public async Task<bool> InsertSubCategory(SubCategoryOne subCategory)
        {
            return await _subCategoryRepository.Insert(subCategory);
        }

        public async Task<bool> UpdateSubCategory(SubCategoryOne subCategory)
        {
            return await _subCategoryRepository.Update(subCategory);
        }

        public async Task<bool> DeleteSubCategory(SubCategoryOne subCategory)
        {
            return await _subCategoryRepository.Delete(subCategory);
        }

        public IEnumerable<SubCategoryOne> GetSubCategoriesByCategoryId(int categoryId)
        {
            return _subCategoryRepository.GetSubCategoriesByCategoryId(categoryId);
        }

        /// <summary>
        /// Inserts bulk Sub Categories
        /// </summary>
        /// <param name="subCategoryExcelList"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> InsertSubCategoriesInBulk(List<SubCategoryImportExcel> subCategoryExcelList, string userId)
        {
            if (subCategoryExcelList.Count > 0)
            {
                try
                {
                    IList<SubCategoryOne> subCategoriesList = new List<SubCategoryOne>();
                    ApplicationUser applicationUser = _subCategoryRepository.GetApplicationUser(userId);
                    foreach (var item in subCategoryExcelList)
                    {
                        SubCategoryOne subCategoryExcel = new SubCategoryOne();
                        subCategoryExcel.CategoryId = Convert.ToInt32(item.CategoryId);
                        subCategoryExcel.SubCategoryName = item.SubCategoryName;
                        subCategoryExcel.Description = item.Description;
                        subCategoryExcel.DisplayOrder = item.DisplayOrder;
                        subCategoryExcel.IsActive = item.IsActive;
                        subCategoryExcel.ApplicationUser = applicationUser; //CreatedByUserId
                        subCategoryExcel.ApplicationUser1 = applicationUser;//UpdatedByUserId
                        subCategoryExcel.CreatedDate = DateTime.Now;
                        subCategoryExcel.UpdatedDate = DateTime.Now;
                        if (!string.IsNullOrEmpty(item.ImageName))
                        {
                            subCategoryExcel.SC_ImageName = item.ImageName;
                        }
                        else
                        {
                            subCategoryExcel.SC_ImageName = "Default";
                        }
                        if (!string.IsNullOrEmpty(item.ImagePath))
                            subCategoryExcel.SC_ImagePath= item.ImagePath;

                        subCategoriesList.Add(subCategoryExcel);
                    }
                    return await _subCategoryRepository.BulkCreate(subCategoriesList);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return true;
        }

        private void SaveImages(SubCategoryOne subCategoryExcel)
        {
            
        }
    }
}
