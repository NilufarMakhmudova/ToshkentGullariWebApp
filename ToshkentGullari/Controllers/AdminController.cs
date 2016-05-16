using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToshkentGullari.Models;
using System.Xml.Linq;
using PagedList.Mvc;
using PagedList;
using System.Xml.Xsl;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Schema;


namespace ToshkentGullari.Controllers
{
    public class AdminController : Controller
    {
        private ToshkentGullariEntities db = new ToshkentGullariEntities();

        #region Products
        // GET: Admin
        public ActionResult ProductsIndex(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }

            ViewBag.CurrentFilter = "";


            var products = db.Products.Include(p => p.Category).Include(p => p.Subcategory);
            products = products.OrderBy(p => p.PictureVertical);
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()) || s.ProductDescription.ToUpper().Contains(searchString.ToUpper()));
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));


        }

        // GET: Admin/Details/5
        public ActionResult ProductDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult TransferProducts(string mode)
        {
            if (String.IsNullOrEmpty(mode)) {
                mode = "ViewHTML";
            }
            List<Product> prList = db.Products.ToList();
            if (prList.Count > 0)
            {
                var xEle = new XElement("Products",
                    from product in prList
                    select new XElement("Product",
                        new XElement("BusinessCode", product.BusinessCode),
                        new XElement("Name", product.Name),
                        new XElement("Desctiption", product.ProductDescription),
                        new XElement("Price", product.Price),
                        new XElement("Category", product.Category.Name),
                        new XElement("Subcategory", product.Subcategory.Name)
                        ));

                string xsdMarkup = @"<xs:schema 
xmlns:xs='http://www.w3.org/2001/XMLSchema'>
  <xs:element name='Products'>
    <xs:complexType>
      <xs:sequence>
        <xs:element name='Product' maxOccurs='unbounded'>
          <xs:complexType>
            <xs:sequence>
              <xs:element name='BusinessCode' type='xs:string' minOccurs='1' maxOccurs='1'/>
              <xs:element name='Name' type='xs:string' minOccurs='1' maxOccurs='1'/>
              <xs:element name='Desctiption' type='xs:string' minOccurs='1' maxOccurs='1'/>
              <xs:element name='Price' type='xs:int' minOccurs='1' maxOccurs='1'/>
              <xs:element name='Category' type='xs:string' minOccurs='1' maxOccurs='1'/>
              <xs:element name='Subcategory' type='xs:string' minOccurs='1' maxOccurs='1'/>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>";
     
                XmlSchemaSet schemas = new XmlSchemaSet();
                schemas.Add("", XmlReader.Create(new StringReader(xsdMarkup)));

                XDocument doc1 = new XDocument(xEle);
                doc1.Validate(schemas, (sender, e) =>
                {
                    ViewData["Failed"] = "Data was not validated against XML Schema";
                   
                }, true);

                    switch (mode) { 
                        case "SaveXML":
                        HttpContext context = System.Web.HttpContext.Current;
                        context.Response.Write(xEle);
                        context.Response.ContentType = "application/xml";
                        context.Response.AppendHeader("Content-Disposition", "attachment; filename=Products.xml");
                        context.Response.End();
                        break;
                        case "Switch to CSV":
                        GenerateHtmlString(xEle, "~/XSLT/XSLTFileProductCSV.xslt");
                        break;
                        case "ViewHTML":
                        GenerateHtmlString(xEle, "~/XSLT/XSLTFileProduct.xslt");
                        break;                  
                                        
                    }
                return View();                
            }
            return View();
        }

     private HtmlString GenerateHtmlString(XElement xEle, string path) {
     XslCompiledTransform t = new XslCompiledTransform();
                t.Load(Server.MapPath(path));
                XsltArgumentList args = new XsltArgumentList();
                StringWriter writer = new StringWriter();
                    t.Transform(xEle.CreateReader(),args,writer);
                    HtmlString htmlString = new HtmlString(writer.ToString());
                    ViewBag.htmlString = htmlString;
                return htmlString;
    }
           
        


        // GET: Admin/Create
        public ActionResult CreateProduct()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "Name");
            ViewBag.SubcategoryID = new SelectList(db.Subcategories, "Id", "Name");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct([Bind(Include = "ID,BusinessCode,Name,Price,ProductDescription,PictureURL,CategoryID,SubcategoryID,PictureVertical")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("ProductsIndex");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "Name", product.CategoryID);
            ViewBag.SubcategoryID = new SelectList(db.Subcategories, "Id", "Name", product.SubcategoryID);
            return View(product);
        }

        // GET: Admin/Edit/5
        public ActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "Name", product.CategoryID);
            ViewBag.SubcategoryID = new SelectList(db.Subcategories, "Id", "Name", product.SubcategoryID);
            return View(product);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct([Bind(Include = "ID,BusinessCode,Name,Price,ProductDescription,PictureURL,CategoryID,SubcategoryID,PictureVertical")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ProductsIndex");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "Name", product.CategoryID);
            ViewBag.SubcategoryID = new SelectList(db.Subcategories, "Id", "Name", product.SubcategoryID);
            return View(product);
        }

        // GET: Admin/Delete/5
        public ActionResult DeleteProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProductConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("ProductsIndex");
        } 
        #endregion


        #region Support
        // GET: Supports
        public ActionResult SupportsIndex()
        {
            return View(db.Supports.ToList());
        }

        // GET: Supports/Details/5
        public ActionResult SupportDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Support support = db.Supports.Find(id);
            if (support == null)
            {
                return HttpNotFound();
            }
            return View(support);
        }

        // GET: Supports/Delete/5
        public ActionResult DeleteSupport(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Support support = db.Supports.Find(id);
            if (support == null)
            {
                return HttpNotFound();
            }
            return View(support);
        }

        // POST: Supports/Delete/5
        [HttpPost, ActionName("DeleteSupport")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSupportConfirmed(int id)
        {
            Support support = db.Supports.Find(id);
            db.Supports.Remove(support);
            db.SaveChanges();
            return RedirectToAction("SupportsIndex");
        }

        
        #endregion

        #region Answers
        // GET: Answers
        public ActionResult AnswersIndex()
        {
            var answers = db.Answers.Include(a => a.Support);
            return View(answers.ToList());
        }

        // GET: Answers/Details/5
        public ActionResult AnswerDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        // GET: Answers/Create
        public ActionResult CreateAnswer()
        {
            ViewBag.QuestionID = new SelectList(db.Supports, "ID", "Category");
            return View();
        }

        // POST: Answers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAnswer([Bind(Include = "ID,AdminID,Answer1,QuestionID,TimeAnswered")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                db.Answers.Add(answer);
                db.SaveChanges();
                return RedirectToAction("AnswersIndex");
            }

            ViewBag.QuestionID = new SelectList(db.Supports, "ID", "Category", answer.QuestionID);
            return View(answer);
        }

        // GET: Answers/Edit/5
        public ActionResult EditAnswer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionID = new SelectList(db.Supports, "ID", "Category", answer.QuestionID);
            return View(answer);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAnswer([Bind(Include = "ID,AdminID,Answer1,QuestionID,TimeAnswered")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(answer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AnswersIndex");
            }
            ViewBag.QuestionID = new SelectList(db.Supports, "ID", "Category", answer.QuestionID);
            return View(answer);
        }

        // GET: Answers/Delete/5
        public ActionResult DeleteAnswer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("DeleteAnswer")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAnswerConfirmed(int id)
        {
            Answer answer = db.Answers.Find(id);
            db.Answers.Remove(answer);
            db.SaveChanges();
            return RedirectToAction("AnswersIndex");
        } 
        #endregion


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
