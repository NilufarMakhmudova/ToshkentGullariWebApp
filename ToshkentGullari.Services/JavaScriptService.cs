using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ToshkentGullari.Services {
    public class JavaScriptService {
        public List<Product> Deserialize(String json)
        {
            if (String.IsNullOrWhiteSpace(json))
                throw new ArgumentNullException("json");
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<List<Product>>(json);
        }
        public Product DeserializeProduct(String json)
        {
            if (String.IsNullOrWhiteSpace(json))
                throw new ArgumentNullException("json");
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<Product>(json);
        }
        public String Serialize(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(product);
        }
        public String Serialize(List<Product> products)
        {
            if (products == null)
                throw new ArgumentNullException("products");
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(products);
        }
    }
}
