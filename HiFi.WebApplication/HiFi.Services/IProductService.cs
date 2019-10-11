using HiFi.Common.ExcelModel;
using HiFi.Data.Models;
using HiFi.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiFi.Services
{
    public interface IProductService
    { 
        Task<IEnumerable<Product>> GetAllProducts(int storeId = 0, bool showHidden = false, bool loadCacheableCopy = true);
        Task<IEnumerable<Product>> GetAllProductsFromDB();
        IEnumerable<Product> GetAllProductsFromBySubCategory(int subCategoryId=0);
        Task<Product> InsertProduct(Product product);
        Task<ProductImage> InsertProductImage(ProductImage product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(Product product);
        IEnumerable<ProductImage> GetAllProductImagesById(int pKProductId);
        Product GetProductByProductId(int id);
        Task<Product> GetProductById(int id);
        Task<bool> InsertProductsInBulk(List<ProductImportExcel> records, string userId);

        Task<int> ProductsCount();
    }
}
