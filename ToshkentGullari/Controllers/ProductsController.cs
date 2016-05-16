using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToshkentGullari.Models;
using PagedList;
using System.Threading;
using System.Globalization;
using ToshkentGullari.Services;

namespace ToshkentGullari.Controllers
{
    public class ProductsController : Controller
    {
        private ToshkentGullariEntities db = new ToshkentGullariEntities();

        // GET: Products
        public ActionResult Index(string currentFilter, string sortOrder, string searchString, int? page, String lang)
        {
            GlobalizationHelper.SetLanguage(lang);

            if (searchString != null)
            {
                page = 1;
            }

            ViewBag.CurrentFilter = "";
            ViewBag.CurrentSearch = searchString;

            var products = db.Products.Include(p => p.Category).Include(p => p.Subcategory);

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";

            switch (sortOrder)
            {
                
                case "Name_desc":
                    products = products.OrderByDescending(s => s.Name);
                    break;
                case "Price":
                    products = products.OrderBy(s => s.Price);
                    break;
                case "Price_desc":
                    products = products.OrderByDescending(s => s.Price);
                    break;
                default:
                    products = products.OrderBy(s => s.Name);
                    break;
            }


            products= products.OrderBy(p => p.Name).OrderBy(p => p.PictureVertical);

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()) || s.ProductDescription.ToUpper().Contains(searchString.ToUpper()));
            }

                 


            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
           
           
        }

        public ActionResult Browse(string currentFilter, string sortOrder, string searchString, int? page, int? categoryID, int? subcategoryID, String lang)
        {
            GlobalizationHelper.SetLanguage(lang);

            if (searchString != null)
            {
                page = 1;
            }

            ViewBag.CurrentFilter = "";


            var products = db.Products.Include(p => p.Category).Include(p => p.Subcategory);
            ViewBag.CurrentSearch = searchString;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CategoryID = categoryID;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";

            switch (sortOrder)
            {
                case "Name_desc":
                    products = products.OrderByDescending(s => s.Name);
                    break;
                case "Price":
                    products = products.OrderBy(s => s.Price);
                    break;
                case "Price_desc":
                    products = products.OrderByDescending(s => s.Price);
                    break;
                default:
                    products = products.OrderBy(s => s.Name);
                    break;
            }


            products = products.OrderBy(p => p.PictureVertical);

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()) || s.ProductDescription.ToUpper().Contains(searchString.ToUpper()));
            }

            if (categoryID.HasValue)
            {
                products = products.Where(p => p.CategoryID == categoryID);
            }

            if (subcategoryID.HasValue)
            {
                products = products.Where(p => p.SubcategoryID == subcategoryID);
            }
            
            



            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));


        }

        [ChildActionOnly]
        public ActionResult Categories(String lang)
        {
            GlobalizationHelper.SetLanguage(lang);

            var categories = from s in db.Categories select s;
            return PartialView(categories.ToList());
        }

        [ChildActionOnly]
        public ActionResult Subcategories(int categoryID)
        {
            var subcategories = from s in db.Subcategories select s;
            subcategories = subcategories.Where(s => s.CategoryID == categoryID);
            return PartialView(subcategories.ToList());
        }
        
        // GET: Products/Details/5
        public ActionResult Details(int? id, String lang)
        {
            GlobalizationHelper.SetLanguage(lang);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToshkentGullari.Models.Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
