using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToshkentGullari.Services;

namespace ToshkentGullari.Controllers
{
    public class ApiClientController : Controller
    {
        // GET: ApiClient
        public ActionResult Index()
        {
            var client = new SoapServiceReference.ProductsClient();
            var externalData = client.GetAll();
            return View(externalData);

        }

        public ActionResult Json()
        {
            var client = new RestClient();
            var products = client.GetProducts();
            return View(products);

        }

        public ActionResult Weather()
        {
            var client = new RestClient();
            var weather = client.GetWeatherInformation();
            return View(weather);

        }



    }
}