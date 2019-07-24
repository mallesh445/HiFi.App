using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HiFi.Data.Models
{
    public class Product: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int PKProductId { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }
        
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [ForeignKey("CreatedByUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("UpdatedByUserId")]
        public virtual ApplicationUser ApplicationUser1 { get; set; }
        public bool IsActive { get; set; }
    }
}
