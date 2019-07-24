using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HiFi.Data.Models
{
    public class ProductImage: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int PKImageId { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }

        [Required]
        public bool IsMainImage { get; set; }
    }
}
