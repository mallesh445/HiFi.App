using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiFi.Common.ExcelModel
{
    public class SubCategoryImportExcel
    {
        public string CategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public int UpdatedByUserId { get; set; }
        public bool IsActive { get; set; }

    }
}
