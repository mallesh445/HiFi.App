using System;
using System.Collections.Generic;
using System.Text;

namespace HiFi.Common.ExcelModel
{
    public class ProductImportExcel
    {
        public string SubCategoryId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ModelNumber { get; set; }
        public string SerialNumber { get; set; }
        public string Price { get; set; }
        public string ShortDescription { get; set; }
        public string Quantity { get; set; }
        public string DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
