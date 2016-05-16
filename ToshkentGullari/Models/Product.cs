//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ToshkentGullari.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public int ID { get; set; }
        public string BusinessCode { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string ProductDescription { get; set; }
        public string PictureURL { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> SubcategoryID { get; set; }
        public string PictureVerical { get; set; }
        public string PictureVertical { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Subcategory Subcategory { get; set; }
    }
}
