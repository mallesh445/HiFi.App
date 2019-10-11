using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiFi.WebApplication.Areas.Admin.ViewModels
{
    public class ProductImageViewModel
    {
        public int PKImageId { get; set; }

        public string ImageName { get; set; }
        public string ImagePath { get; set; }

        public int FKProductId { get; set; }
        public bool IsMainImage { get; set; }


        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
