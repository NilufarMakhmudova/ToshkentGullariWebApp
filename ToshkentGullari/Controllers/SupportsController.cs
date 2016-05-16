using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using ToshkentGullari.Models;
using ToshkentGullari.Services;

namespace ToshkentGullari.Controllers
{
    public class SupportsController : Controller
    {
        private ToshkentGullariEntities db = new ToshkentGullariEntities();

        
        // GET: Supports/Create
        public ActionResult Create(String lang)
        {
            GlobalizationHelper.SetLanguage(lang);
            return View();
        }

        // POST: Supports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Category,Content,CustomerName,Email,Phone")] Support support)
        {
            
            if (ModelState.IsValid)
            {
                support.TimeAdded = DateTime.Now;
                db.Supports.Add(support);
                db.SaveChanges();
                //Авторизация на SMTP сервере
                try
                {
                    SmtpClient Smtp = new SmtpClient("mail.wiut.uz", 25);

                    Smtp.Credentials = new NetworkCredential("00002059", "abd16012013");
                    Smtp.EnableSsl = true;

                    //Формирование письма
                    System.Net.Mail.MailMessage Message = new System.Net.Mail.MailMessage();
                    //From
                    Message.From = new MailAddress("nmakhmudova@students.wiut.uz");
                    //To
                    Message.To.Add(new MailAddress("00002059@mail.ru"));
                    Message.Subject = "New item: " + support.Category;
                    string message = "From: " + support.CustomerName + " ";
                    message = message + "\n";

                    if (support.Email != null)
                    {
                        message = message + "E-mail: " + support.Email + "\n";
                    }
                    message = message + support.Content;

                    Message.Body = message;

                    Smtp.Send(Message);//отправка
                }
                catch (Exception)
                {
                    
                    throw;
                }


                return RedirectToAction("Index");
            }

            return View(support);
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
