using HiFi.Services;
using HiFi.Services.Catalog;
using HiFi.WebApplication.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiFi.WebApplication.Components
{
    public class CategoryNavigationViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly ISubCategoryService _subCategoryService;
        public CategoryNavigationViewComponent(ICategoryService categoryService, ISubCategoryService subCategoryService)
        {
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
        }
        public IViewComponentResult Invoke(int currentCategoryId, int currentProductId)
        {
            var query = Request.Query;
            var model = PrepareCategoryNavigationModel(currentCategoryId, currentProductId);
            return View(model);
        }

        public virtual CategoryNavigationModel PrepareCategoryNavigationModel(int currentCategoryId, int currentProductId)
        {
            //get active category
            var activeCategoryId = 0;
            if (currentCategoryId > 0)
            {
                //category details page
                activeCategoryId = currentCategoryId;
            }
            else if (currentProductId > 0)
            {
                //product details page
                int productCategories = _categoryService.GetProductCategoriesByProductId(currentProductId);
                if (productCategories != null)
                    activeCategoryId = productCategories;
                //if (productCategories.Any())
                //    activeCategoryId = productCategories[0].CategoryId;
            }

            var cachedCategoriesModel = PrepareCategorySimpleModels(0);
            var model = new CategoryNavigationModel
            {
                CurrentCategoryId = activeCategoryId,
                Categories = cachedCategoriesModel
            };

            return model;
        }

        private List<CategorySimpleModel> PrepareCategorySimpleModels(int rootCategoryId, bool loadSubCategories = true)
        {
            var result = new List<CategorySimpleModel>();
            var catSubCatProductCountsList = _categoryService.GetNoOfProductsAndSubCategoriesByCategories();
            var allCategories = _categoryService.GetAllCategories();
            var categories = allCategories.Where(c => c.CategoryId == rootCategoryId).ToList();
            foreach (var category in allCategories)
            {
                var categoryModel = new CategorySimpleModel
                {
                    Id = category.CategoryId,
                    Name = category.CategoryName,
                    SeName = category.CategoryName,
                    IncludeInTopMenu = true
                };

                //number of products in each category
                categoryModel.NumberOfProducts = catSubCatProductCountsList.Where(a => a.CategoryId == categoryModel.Id)
                    .Select(c => c.ProductCount).FirstOrDefault();

                if (loadSubCategories)
                {
                    var subCategories = PrepareSubCategorySimpleModels(category.CategoryId, loadSubCategories);
                    categoryModel.SubCategories.AddRange(subCategories);
                }
                result.Add(categoryModel);
            }

            return result;
        }

        private List<CategorySimpleModel> PrepareSubCategorySimpleModels(int currentCategoryId, bool loadSubCategories)
        {
            var result = new List<CategorySimpleModel>();

            var allSubCategories = _subCategoryService.GetAllSubCategories();
            var subCategories = allSubCategories.Where(c => c.CategoryId == currentCategoryId).ToList();
            foreach (var subCategory in subCategories)
            {
                var categoryModel = new CategorySimpleModel
                {
                    Id = subCategory.CategoryId,
                    Name = subCategory.SubCategoryName,
                    SeName = subCategory.SubCategoryName,
                    IncludeInTopMenu = true
                };

                //number of products in each category
                categoryModel.NumberOfProducts = 5;

                result.Add(categoryModel);
            }

            return result;
        }
    }
}
