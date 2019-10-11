using System;
using System.Collections.Generic;
using System.Text;

namespace HiFi.Data.ViewModels
{
    public class LatestProductsViewModel
    {
        public int PKProductId { get; set; }
        public string ProductName { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public int DisplayOrder { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }

        public int SubCategoryOneId { get; set; }
        public List<ProductImageViewModel> ProductImage { get; set; }
    }
}
