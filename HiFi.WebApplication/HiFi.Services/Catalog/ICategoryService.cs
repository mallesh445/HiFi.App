using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HiFi.Common;
using HiFi.Common.ExcelModel;
using HiFi.Common.ViewModel;
using HiFi.Data.Models;

namespace HiFi.Services.Catalog
{
    public interface ICategoryService
    {

        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <param name="storeId">Store identifier; 0 if you want to get all records</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <param name="loadCacheableCopy">A value indicating whether to load a copy that could be cached (workaround until Entity Framework supports 2-level caching)</param>
        /// <returns>Categories</returns>
        Task<IEnumerable<Category>> GetAllCategories(int storeId = 0, bool showHidden = false, bool loadCacheableCopy = true);

        /// <summary>
        /// InsertCategory
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        bool InsertCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(int categoryId);
        int GetProductCategoriesByProductId(int currentProductId);

        List<CategoryChildsCount> GetNoOfProductsAndSubCategoriesByCategories();

        /// <summary>
        /// InsertCategorInBulk
        /// </summary>
        /// <param name="records"></param>
        /// <returns></returns>
        Task<bool> InsertCategorInBulk(List<CategoryImportExcel> records,string userId);
        Task<Category> GetCategoryByIdAsync(int id);
        List<CategoryNavViewModel> GetCategoriesAndSubCategories();
    }
}
