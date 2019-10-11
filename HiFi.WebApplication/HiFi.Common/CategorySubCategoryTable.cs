using System;
using System.Collections.Generic;
using System.Text;

namespace HiFi.Common
{
    public class CategorySubCategoryTable
    {
        public CategorySubCategoryTable()
        {

        }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int SubCategoryOneId { get; set; }
        public string SubCategoryName { get; set; }
        //public int SubCategorycount { get; set; }
        public int ProductsCount { get; set; }
    }
}
