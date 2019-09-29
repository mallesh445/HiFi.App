using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiFi.WebApplication.Areas.Admin.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            ProductImageModel = new List<ProductImageViewModel>();
        }
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }
         
        public int DisplayOrder { get; set; }
         
        public int Quantity { get; set; } 
        public decimal Price { get; set; }

        public string SubCategoryId { get; set; }
        public string ModelNumber { get; set; }
        public string SerialNumber { get; set; }
        public bool IsActive { get; set; }

        public List<ProductImageViewModel> ProductImageModel { get; set; }

    }
}
