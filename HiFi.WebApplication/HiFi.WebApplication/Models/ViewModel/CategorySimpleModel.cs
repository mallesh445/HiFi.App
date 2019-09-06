using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiFi.WebApplication.Models.ViewModel
{
    public class CategorySimpleModel
    {
        public CategorySimpleModel()
        {
            SubCategories = new List<CategorySimpleModel>();
        }
        public string Name { get; set; }

        public string SeName { get; set; }

        public int? NumberOfProducts { get; set; }

        public bool IncludeInTopMenu { get; set; }

        public int Id { get; set; }
        public List<CategorySimpleModel> SubCategories { get; set; }
    }
}
