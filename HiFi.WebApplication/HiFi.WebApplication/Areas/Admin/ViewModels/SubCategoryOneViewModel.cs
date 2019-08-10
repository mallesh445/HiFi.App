using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HiFi.WebApplication.Areas.Admin.ViewModels
{
    public class SubCategoryOneViewModel
    {
        public int SubCategoryOneId { get; set; }

        [Required]
        public string SubCategoryName { get; set; }

        [Required]
        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public string ImageName { get; set; }
        public string ImagePath { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public int CategoryId { get; set; }

        public List<SelectList> Categories {  set; get; }
        public IEnumerable<CategoryViewModel> MyCategoryList { get; set; }
    }
}
