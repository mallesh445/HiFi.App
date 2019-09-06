using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiFi.WebApplication.Models.ViewModel
{
    public class CategoryNavigationModel
    {
        public CategoryNavigationModel()
        {
            Categories = new List<CategorySimpleModel>();
        }

        public int CurrentCategoryId { get; set; }
        public List<CategorySimpleModel> Categories { get; set; }

        #region Nested classes

        public class CategoryLineModel 
        {
            public int CurrentCategoryId { get; set; }
            public CategorySimpleModel Category { get; set; }
        }

        #endregion
    }
}
