using HiFi.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiFi.Services
{
    public interface IProductService
    { 
        IEnumerable<Product> GetAllProducts(int storeId = 0, bool showHidden = false, bool loadCacheableCopy = true);

        Product InsertProduct(Product category);
        ProductImage InsertProductImage(ProductImage category);
        bool UpdateProduct(Product category);
        bool DeleteProduct(Product category);
        IEnumerable<ProductImage> GetAllProductImagesById(int pKProductId);

        //bool InsertProductInBulk(List<ProductImportExcel> records);
    }
}
