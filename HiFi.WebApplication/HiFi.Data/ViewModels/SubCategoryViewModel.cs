using System;
using System.Collections.Generic;
using System.Text;

namespace HiFi.Data.ViewModels
{
    public class SubCategoryViewModel
    {
        public int SubCategoryId { get; set; }

        public string SubCategoryName { get; set; }

        public string Description { get; set; }
        
        public int DisplayOrder { get; set; }

        public string CategoryId { get; set; }

        public string SC_ImageName { get; set; }
        public string SC_ImagePath { get; set; }
    }
}
