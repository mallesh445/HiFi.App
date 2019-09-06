using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

        public ProductController(IProductService productService, IHostingEnvironment hostingEnvironment,
            ICategoryService categoryService, ISubCategoryService subCategoryService, IMapper mapper)
        {
            _productService = productService;
            _hostingEnvironment = hostingEnvironment;
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
            _mapper = mapper;
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

        public IActionResult ProductDetails(int productId)
        {
            var productFromDB = _productService.GetProductByProductId(productId);
            ProductViewModel productViewModel = _mapper.Map<ProductViewModel>(productFromDB);
            return View(productViewModel);
        }

    }
}