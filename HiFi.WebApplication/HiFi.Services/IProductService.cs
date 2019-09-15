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
        IEnumerable<Product> GetAllProducts(int storeId = 0, bool showHidden = false, bool loadCacheableCopy = true);
        IEnumerable<Product> GetAllProductsFromDB();
        IEnumerable<Product> GetAllProductsFromBySubCategory(int subCategoryId=0);
        Product InsertProduct(Product product);
        ProductImage InsertProductImage(ProductImage product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(Product product);
        IEnumerable<ProductImage> GetAllProductImagesById(int pKProductId);
        Product GetProductByProductId(int id);
        Task<Product> GetProductById(int id);
        bool InsertProductsInBulk(List<ProductImportExcel> records, string userId);
    }
}
