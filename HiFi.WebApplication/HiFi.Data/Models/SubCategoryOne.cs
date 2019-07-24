using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HiFi.Data.Models
{
    public class SubCategoryOne: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int SubCategoryOneId { get; set; }

        [Required]
        [Display(Name = "Sub Category")]
        public string SubCategoryName { get; set; }
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
        [Display(Name = "CreatedDate")]
        public DateTime UpdatedDate { get; set; }
        
        [Required]
        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
