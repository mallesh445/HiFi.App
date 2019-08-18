using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiFi.Data.Models;
using HiFi.Data.ViewModels;
using HiFi.Repository;

namespace HiFi.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> productRepository;
        private readonly IRepository<ProductImage> imageRepository;

        public ProductService(IRepository<Product> _productRepository, IRepository<ProductImage> _imageRepository)
        {
            productRepository = _productRepository;
            imageRepository = _imageRepository;
        }

        public IEnumerable<Product> GetAllProducts(int storeId = 0, bool showHidden = false, bool loadCacheableCopy = true)
        {
            var data = productRepository.GetAll();
            return data;
        }

        public IEnumerable<Product> GetAllProductsFromDB()
        {
            var data = productRepository.GetAll().ToList();
            return data;
        }

        public IEnumerable<Product> GetAllProductsFromBySubCategory(int subCategoryId=0)
        {
            //var data = productRepository.GetAll().ToList();
            //string[] includes = new string[2] {"SubCategoryOne", "ProductImage" };
            //var data = productRepository.GetAll(includes);
            //var products = data.Where(p => p.SubCategoryOneId == subCategoryId);

            var products = productRepository.GetProductsBySubCategoryId(subCategoryId);
            return products;
        }
        public IEnumerable<ProductImage> GetAllProductImages()
        {
            var data = imageRepository.GetAll();
            return data;
        }

        public Product GetProductByProductId(int id)
        {
            var data = productRepository.GetProductByProductId(id);
            return data;
        }

        public async Task<Product> GetProductById(int id)
        {
            return await productRepository.GetProductById(id);
        }
        public IEnumerable<ProductImage> GetAllProductImagesById(int pKProductId)
        {
            var data = imageRepository.GetAllById(pKProductId);
            return data;
        }

        public Product InsertProduct(Product product)
        {
            var entity =productRepository.InsertData(product);
            return entity;
        }
        public ProductImage InsertProductImage(ProductImage image)
        {
            ProductImage entity = imageRepository.InsertData(image);
            return entity;
        }

        public bool UpdateProduct(Product product)
        {
            productRepository.Update(product);
            return true;
        }
        public bool DeleteProduct(Product product)
        {
            productRepository.Delete(product);
            return true;
        }

    }
}
