using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HiFi.Data.Models
{
    public class ShoppingCartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string ShoppingCartId { get; set; }
        public string ApplicationUserId { get; set; }
        public int ProductId { get; set; }


        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value greater than or equal to {1}")]
        public int Qunatity { get; set; }

        [NotMapped]
        public string StatusMessage { get; set; }
        
        public DateTime CreatedDate { get; set; }
    }
}
