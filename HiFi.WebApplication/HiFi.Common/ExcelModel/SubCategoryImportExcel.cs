using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiFi.Common.ExcelModel
{
    public class SubCategoryImportExcel
    {
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public int FKCreatedByUserId { get; set; }
        public Nullable<int> FKUpdatedByUserId { get; set; }
        public bool IsActive { get; set; }

    }
}
