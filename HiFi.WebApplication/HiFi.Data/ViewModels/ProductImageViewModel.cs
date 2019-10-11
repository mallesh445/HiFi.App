using System;
using System.Collections.Generic;
using System.Text;

namespace HiFi.Data.ViewModels
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
