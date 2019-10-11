using System;
using System.Collections.Generic;
using System.Text;

namespace HiFi.Common.ViewModel
{
    public class CategoryNavViewModel
    {
        public CategoryNavViewModel()
        {
            SubCategories = new List<SubCategoriesNavViewModel>();
        }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int? NumberOfProducts { get; set; }
        public bool IsCurrentCategory { get; set; }
        public int CategoryId { get; set; }
        public List<SubCategoriesNavViewModel> SubCategories { get; set; }
    }
    public class SubCategoriesNavViewModel
    {
        public string SubCategoryName { get; set; }
        public string Description { get; set; }
        public int? NumberOfProducts { get; set; }
        public bool IsCurrentSubCategory { get; set; }
        public int SubCategoryId { get; set; }
    }
}