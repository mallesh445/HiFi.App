using HiFi.Common.ExcelModel;
using HiFi.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiFi.Services
{
    public interface ISubCategoryService
    {
        Task<IEnumerable<SubCategoryOne>> GetAllSubCategories();

        Task<bool> InsertSubCategory(SubCategoryOne subCategory);
        Task<bool> UpdateSubCategory(SubCategoryOne subCategory);
        Task<bool> DeleteSubCategory(SubCategoryOne subCategory);
        IEnumerable<SubCategoryOne> GetSubCategoriesByCategoryId(int categoryId);
        Task<bool> InsertSubCategoriesInBulk(List<SubCategoryImportExcel> records, string userId);
    }
}
