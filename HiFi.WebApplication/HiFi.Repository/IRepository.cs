using HiFi.Common;
using HiFi.Common.ViewModel;
using HiFi.Data;
using HiFi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HiFi.Repository
{
    public partial interface IRepository<T> where T : class
    {
        T FindBy(Expression<Func<T, bool>> predicate, string includeProperties = "");
        Task<IEnumerable<T>> GetAll();
        IEnumerable<T> GetAll(string[] includes);
        Task<T> GetById(object id);
        Task<bool> Insert(T obj);
        Task<T> InsertData(T entity);
        Task<bool> Update(T obj);
        Task<bool> Delete(object id);
        Task<bool> Save();
        Task<bool> BulkCreate(IList<T> categoriesList);
        ApplicationUser GetApplicationUser(string userid="");
        IEnumerable<ProductImage> GetAllById(int pKProductId);

        Task<Manufacturer> GetManufacturerByProductId(int pKProductId);
        IEnumerable<Product> GetProductsBySubCategoryId(int subcaId = 0);
        Product GetProductByProductId(int productId);
        Task<Product> GetProductById(int id);
        int GetProductCategoriesByProductId(int currentProductId);
        List<CategoryChildsCount> GetNoOfProductsAndSubCategoriesByCategories();
        IEnumerable<SubCategoryOne> GetSubCategoriesByCategoryId(int categoryId);
        List<CategoryNavViewModel> GetCategoriesAndSubCategories();
        bool CheckUserExistInDatabase(string userName);
    }
}
