using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HiFi.Data.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int CategoryId { get; set; }
       
        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }

        [Required]
        [Display(Name = "CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Display(Name = "UpdatedDate")]
        public DateTime UpdatedDate { get; set; }

        [Required]
        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }


        [ForeignKey("CreatedByUserId")]
        public virtual ApplicationUser ApplicationUser{ get; set; }
    }
}
