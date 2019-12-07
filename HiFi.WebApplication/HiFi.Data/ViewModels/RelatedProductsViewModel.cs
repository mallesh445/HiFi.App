using System;
using System.Collections.Generic;
using System.Text;

namespace HiFi.Data.ViewModels
{
    public class RelatedProductsViewModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
    }
}
