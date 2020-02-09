using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HiFi.Data.Models;
using HiFi.Data.ViewModels;
using HiFi.Services;
using HiFi.Services.Catalog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace HiFi.WebApplication.Controllers
{
    public class ProductController : Controller
    {
        IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ISubCategoryService _subCategoryService;
        private readonly IManufacturerService _manufacturerService;

        public ProductController(IProductService productService, IHostingEnvironment hostingEnvironment, IManufacturerService manufacturerService,
            ICategoryService categoryService, ISubCategoryService subCategoryService, IMapper mapper)
        {
            _productService = productService;
            _hostingEnvironment = hostingEnvironment;
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
            _mapper = mapper;
            _manufacturerService = manufacturerService;
        }

        // GET: Admin/SubCategoryOne
        public IActionResult Index()
        {
            return View();
        }

       

        public IActionResult GetAllProductsBySubCategory(int subCategoryId)
        {
            return View(GetAllProductsBySubCategoryId(subCategoryId));
        }

        /// <summary>
        /// GetAllProductsBySubCategoryId
        /// </summary>
        /// <param name="subCategoryId"></param>
        /// <returns></returns>
        public IEnumerable<ProductViewModel> GetAllProductsBySubCategoryId(int subCategoryId)
        {
            var listOfDbProducts = _productService.GetAllProductsFromBySubCategory(subCategoryId);
            IEnumerable<ProductViewModel> listOfProductsVM = _mapper.Map<IEnumerable<ProductViewModel>>(listOfDbProducts);
            if (listOfProductsVM.Count() > 0)
            {
                ViewBag.SubCategoryName = listOfDbProducts.FirstOrDefault().SubCategoryOne.SubCategoryName.ToString();
            }
            else
            {
                ViewBag.SubCategoryName = "No Products Under this "+ subCategoryId.ToString();
            }
            
            return listOfProductsVM;
        }

        /// <summary>
        /// Search products in Inventory
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetProductsBySearch()
        {
            int selectedCategoryId = Convert.ToInt32(Request.Form["CategoryId"]);
            string searchValue = Request.Form["txtSearch"].ToString();
            //var listOfDbProducts = _productService.GetAllProducts().Result;
            //listOfDbProducts = listOfDbProducts.Where(a => a.ProductName.Contains(searchValue)).ToList();
            var listOfDbProducts = await _productService.GetProductsBySearchValue(searchValue, selectedCategoryId);
            IEnumerable<ProductViewModel> listOfProductsVM = _mapper.Map<IEnumerable<ProductViewModel>>(listOfDbProducts);
            if (listOfProductsVM.Count() > 0)
            {
                ViewBag.SubCategoryName = selectedCategoryId;
            }
            else
            {
                ViewBag.SubCategoryName = searchValue + " products Under this " + selectedCategoryId.ToString()+" doesn't exist. Please try other";
            }
            return View(listOfProductsVM);
        }

        /// <summary>
        /// Retrieves Item details based on productId.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public IActionResult ProductDetails(int productId)
        {
            try
            {
                var productFromDB = _productService.GetProductByProductId(productId);
                ProductViewModel productViewModel = _mapper.Map<ProductViewModel>(productFromDB);
                productViewModel.RelatedProducts = GetRelatedProducts(productFromDB.SubCategoryOneId, productFromDB.PKProductId);
                var manufacturer = _manufacturerService.GetManufacturerByProductId(productId).Result;
                if (manufacturer != null)
                {
                    productViewModel.ManufacturerName = manufacturer.Name;
                    productViewModel.ManufacturerDescription = manufacturer.Description;
                }
                else
                {
                    productViewModel.ManufacturerName = "Not Available";
                    productViewModel.ManufacturerDescription = "Not Available";
                }
                return View(productViewModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets products of same sub category of selcted product.
        /// </summary>
        /// <param name="subCategoryId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        [NonAction]
        private IEnumerable<RelatedProductsViewModel> GetRelatedProducts(int subCategoryId, int productId)
        {
            try
            {
                List<RelatedProductsViewModel> relatedProductsList= new List<RelatedProductsViewModel>();
                var products = _productService.GetAllProductsFromBySubCategory(subCategoryId).ToList();
                if (products != null)
                {
                    foreach (var item in products)
                    {
                        if (item.PKProductId != productId)
                        {
                            RelatedProductsViewModel relatedProduct = new RelatedProductsViewModel()
                            {
                                ProductId = item.PKProductId,
                                Description = item.Description,
                                ProductName = item.ProductName,
                                Price = item.Price
                            };
                            if (item.ProductImage != null && item.ProductImage.Count > 0)
                            {
                                relatedProduct.ImageName = item.ProductImage[0].ImageName;
                                relatedProduct.ImagePath = item.ProductImage[0].ImagePath;
                            }
                            relatedProductsList.Add(relatedProduct);
                        }
                    }
                    //return _mapper.Map<List<RelatedProductsViewModel>>(products);
                }
                return relatedProductsList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}