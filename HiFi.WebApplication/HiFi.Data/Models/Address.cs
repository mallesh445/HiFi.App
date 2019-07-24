using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HiFi.Data.Models
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int AddressId { get; set; }
        /// <summary>
        /// Gets or sets the first name
        /// </summary>
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name
        /// </summary>
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the company
        /// </summary>
        [Display(Name = "Company")]
        public string Company { get; set; }

        /// <summary>
        /// Gets or sets the city
        /// </summary>
        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the address 1
        /// </summary>
        [Required]
        [Display(Name = "Address1")]
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address 2
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the zip/postal code
        /// </summary>
        [Required]
        [Display(Name = "Postal Code")]
        public string ZipPostalCode { get; set; }

        /// <summary>
        /// Gets or sets the phone number
        /// </summary>
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the fax number
        /// </summary>
        [Display(Name = "Fax Number")]
        public string FaxNumber { get; set; }

        /// <summary>
        /// Gets or sets the custom attributes (see "AddressAttribute" entity for more info)
        /// </summary>
        public string CustomAttributes { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the country
        /// </summary>
        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the state/province
        /// </summary>
        public string StateOrProvince { get; set; }

    }
}
