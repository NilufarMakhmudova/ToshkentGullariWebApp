using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ToshkentGullari.Services{
    public class XmlService {
        // This method is supposed to get XML string and produce a list of the
        // items. 
        public List<Product> Deserialize(String xml) {
            // First, let's guard ourself from empty or null data
            if (String.IsNullOrWhiteSpace(xml))
                throw new Exception("you must pass a valid xml file");
            // Now we need to parse it into a convenient class - XDocument class in order
            // to be able to query
            var doc = XDocument.Parse(xml);
            // Lets get all Item elements from the document
            var products = doc.Root.Elements("Products");
            // This is prepared list for storing the converted Items
            var list = new List<Product>();
            // Now for each found item
            foreach (var item in products)
            {
                // Create a new Item
                var newObject = new Product();
                // Grab the values from xml and set the to the properties
               
                newObject.Name = item.Element("Name").Value;
                newObject.BusinessCode = item.Element("BusinessCode").Value;
                newObject.Price = Decimal.Parse(item.Element("Price").Value);
                newObject.ProductDescription = item.Element("ProductDescription").Value;
                // Add the newly created object into our list
                list.Add(newObject);
            }
            // return our result
            return list;

        }

        // This method is supposed to receive xml of one item
        // and convert it into the sole item
        public Product DeserializeItem(String xml)
        {

            // Guarding
            if (String.IsNullOrWhiteSpace(xml))
                throw new ArgumentNullException("xml");

            // Parsing xml
            var xDoc = XDocument.Parse(xml);

            // Create an item that should be materialized
            var product = new Product();

            // Getting the name node
            product.Name = xDoc.Root.Element("Name").Value;
            product.BusinessCode = xDoc.Root.Element("BusinessCode").Value;
            product.Price = Decimal.Parse(xDoc.Root.Element("Price").Value);
            product.ProductDescription = xDoc.Root.Element("ProductDescription").Value;

           
            // return the newly materialized object
            return product;
        }

        // This method should get an item and produce its xml
        public String Serialize(Product product) {
            // Guard
            if (product == null)
                throw new ArgumentNullException("product");

            // create an xml document
            var doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));

            // make up a root, which is in our case "Item"
            var xItem = new XElement("Product",
                // with relevant subelements with values
                // values are taken from the properties

                    new XElement("Name", product.Name),
                    new XElement("BusinessCode", product.BusinessCode),
                    new XElement("Price", product.Price),
                    new XElement("ProductDescription", product.ProductDescription)
                );

            // add the newly created root into our document
            doc.Add(xItem);

            // Convert the document into string and return it
            return doc.ToString();
        }

        // This method should get a list of items and produce its xml
        public String Serialize(List<Product> products) {
            // Guard
            if (products == null)
                throw new ArgumentNullException("products");

            // Again, prepare a document
            var xDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));

            // Prepare a root. Now the root is Items, as there will be many items inside
            var root = new XElement("Products");

            // add the newly created root into our document
            xDoc.Add(root);

            // Iterate over our list
            foreach (var item in products)
            {

                // create a corresponding xml element for the item
                var xItem = new XElement("Product",
                    // with relevant subelements with values
                    // values are taken from the properties
                   
                    new XElement("Name", item.Name),
                    new XElement("BusinessCode", item.BusinessCode),
                    new XElement("Price", item.Price),
                    new XElement("ProductDescription", item.ProductDescription)
                );

                // and newly created xml element into the root
                root.Add(xItem);
            }

            // generate a string and return it
            return xDoc.ToString();
        }

        public Weather DeserializeWeather(String xml)
        {

            // Guarding
            if (String.IsNullOrWhiteSpace(xml))
                throw new ArgumentNullException("xml");

            // Parsing xml
            var xDoc = XDocument.Parse(xml);

            // Create an item that should be materialized
            var weather = new Weather();

            // Getting  nodes
           
            weather.City = xDoc.Root.Element("city").Attribute("name").Value;
            weather.Temperature = Decimal.Parse(xDoc.Root.Element("temperature").Attribute("value").Value);
            weather.Temperature = Decimal.Parse(xDoc.Root.Element("humidity").Attribute("value").Value);
            
            // return the newly materialized object
            return weather;
        }
    }
}
