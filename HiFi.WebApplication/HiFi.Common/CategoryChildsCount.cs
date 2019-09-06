using System;
using System.Collections.Generic;
using System.Text;

namespace HiFi.Common
{
    public class CategoryChildsCount
    {
        public CategoryChildsCount()
        {
                
        }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int SubCategorycount { get; set; }
        public int ProductCount { get; set; }
    }
}
