using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ToshkentGullari.Models;

namespace ToshkentGullari.API
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Products" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Products.svc or Products.svc.cs at the Solution Explorer and start debugging.
    public class Products : IProducts
    {
        private ToshkentGullariDBEntities db = new ToshkentGullariDBEntities();


        public List<Models.ProductModel> GetAll()
        {
            var list = new List<Models.ProductModel>();
            var products = db.Products;
            foreach (var item in products)
            {
                list.Add(new Models.ProductModel
                {
                    BusinessCode = item.BusinessCode,
                    Name = item.Name,
                    Price = item.Price,
                    ProductDescription = item.ProductDescription
                });
            }
            return list;
        }

        public Models.ProductModel GetById(int id)
        {
           var item = db.Products.Find(id);
           return new Models.ProductModel
           {
               BusinessCode = item.BusinessCode,
               Name = item.Name,
               Price = item.Price,
               ProductDescription = item.ProductDescription
           };
        }        
    }
}
