using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HiFi.Common.ViewModel;
using HiFi.Data.ViewModels;
using HiFi.Services;
using HiFi.Services.Catalog;
using Microsoft.AspNetCore.Mvc;

namespace HiFi.WebApplication.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly ISubCategoryService _subCategoryService;
        public CategoryController(ISubCategoryService subCategoryService, IMapper mapper, 
            ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }
        public IActionResult GetSubCategoriesByCategory(int categoryId)
        {
            return View(GetSubCategoriesByCategoryId(categoryId));
        }

        private IEnumerable<SubCategoryViewModel> GetSubCategoriesByCategoryId(int categoryId)
        {
            var listOfDbSubCatList = _subCategoryService.GetSubCategoriesByCategoryId(categoryId);
            IEnumerable<SubCategoryViewModel> listOfProductsVM = _mapper.Map<IEnumerable<SubCategoryViewModel>>(listOfDbSubCatList);
            ViewBag.CategoryName = listOfDbSubCatList.FirstOrDefault().Category.CategoryName.ToString();

            return listOfProductsVM;
        }
        private IEnumerable<CategoryNavViewModel> GetCategoriesAndSubCategories(int categoryId=0)
        {
            var categoryNavViewModels = _categoryService.GetCategoriesAndSubCategories();
            
            return categoryNavViewModels;
        }

    }
}