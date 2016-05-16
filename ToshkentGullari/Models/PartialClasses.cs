using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToshkentGullari.Models
{

    //needed for data validation 
    //accesses metadata class and validates classes of EF
    [MetadataType(typeof(SupportMetadata))]
    public partial class Support
    {
    }

    [MetadataType(typeof(ProductMetadata))]
    public partial class Product
    {
    }

    
}