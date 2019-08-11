using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HiFi.Services;
using HiFi.Services.Catalog;
using HiFi.WebApplication.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace HiFi.WebApplication.Controllers
{
    public class ProductController : Controller
    {

        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ISubCategoryService _subCategoryService;

        public ProductController(IProductService productService, IHostingEnvironment hostingEnvironment,
            ICategoryService categoryService, ISubCategoryService subCategoryService)
        {
            _productService = productService;
            _hostingEnvironment = hostingEnvironment;
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
        }

        // GET: Admin/SubCategoryOne
        public IActionResult Index()
        {
           
           
            return View();
        }

        public List<ProductViewModel> GetAllProductsUnderSubCategory(int subCateforyId)
        {
            var products = _productService.GetAllProducts();
            List<ProductViewModel> list= products.Where(x => x.SubCategoryOneId == subCateforyId).ToList();
            return list;
        }

    }
}