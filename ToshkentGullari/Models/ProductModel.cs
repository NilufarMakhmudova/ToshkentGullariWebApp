using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ToshkentGullari.Models
{
    [DataContract]
    public class ProductModel
    {
        
        [DataMember]
        public string BusinessCode { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Nullable<decimal> Price { get; set; }

        [DataMember]
        public string ProductDescription { get; set; }

        
    }
}