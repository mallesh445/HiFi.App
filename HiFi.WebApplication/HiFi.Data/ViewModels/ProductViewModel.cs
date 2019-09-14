using System;
using System.Collections.Generic;
using System.Text;

namespace HiFi.Data.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            ProductImage = new List<ProductImageViewModel>();
        }
        public int PKProductId { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public int DisplayOrder { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public string SubCategoryOneId { get; set; }

        public string SerialNumber { get; set; }

        public string ModelNumber { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<ProductImageViewModel> ProductImage { get; set; }

    }
}
