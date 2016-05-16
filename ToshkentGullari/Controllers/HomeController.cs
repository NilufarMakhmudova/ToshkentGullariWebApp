using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToshkentGullari.Services;

namespace ToshkentGullari.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(String lang)
        {
            GlobalizationHelper.SetLanguage(lang);
            return View();
        }

        public ActionResult About(String lang)
        {
            GlobalizationHelper.SetLanguage(lang);
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact(String lang)
        {
            GlobalizationHelper.SetLanguage(lang);
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}