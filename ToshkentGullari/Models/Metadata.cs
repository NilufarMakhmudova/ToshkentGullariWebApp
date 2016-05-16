using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToshkentGullari.Models
{
    public class SupportMetadata
    {

        [Required]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 50, MinimumLength = 1)]
        [Display(Name = "Category")]
        public string Category { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 500, MinimumLength = 1)]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 150, MinimumLength = 1)]
        [Display(Name = "Name")]
        public string CustomerName { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(maximumLength: 256, MinimumLength = 3)]
        [Display(Name = "Email address")]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact number")]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date added")]
        public System.DateTime TimeAdded { get; set; }

       
    }

    public class ProductMetadata
    {

        [Required]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 20, MinimumLength = 1)]
        [Display(Name = "BusinessCode")]
        public string BusinessCode{ get; set; }

                          
        [Display(Name = "Price")]
        public Nullable<decimal> Price { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 150, MinimumLength = 1)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 2000, MinimumLength = 15)]
        [Display(Name = "ProductDescription")]
        public string ProductDescription { get; set; }
        [Required]
        [Display(Name = "PictureURL")]
        public string PictureURL { get; set; }
        [Required]
        [Display(Name = "Category")]
        public Nullable<int> CategoryID { get; set; }
        [Required]
        [Display(Name = "Subcategory")]
        public Nullable<int> SubcategoryID { get; set; }
        [Required]
        [Display(Name = "PictureVertical")]
        public string PictureVertical { get; set; }


    }
}