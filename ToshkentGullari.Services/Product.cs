using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToshkentGullari.Services
{
    public class Product
    {
        public int ID { get; set; }
        public string BusinessCode { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string ProductDescription { get; set; }
        public string PictureURL { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> SubcategoryID { get; set; }
        public string PictureVertical { get; set; }

         }
}
