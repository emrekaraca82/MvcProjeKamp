using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKamp.Controllers
{
    public class ContactController : Controller
    {
        ContactManager cm = new ContactManager(new EfContactDal());
        ContactValidator cv = new ContactValidator();
        public ActionResult Index()
        {
            var contactList = cm.GetList();          
            return View(contactList);
        }

        public ActionResult GetContactDetails(int id)
        {
            var contactList = cm.GetById(id);
            return View(contactList);
        }

        public PartialViewResult ContactPartial()
        {
            Context c = new Context();
            var id = (string)Session["WriterEmail"];

            var deger1 = c.Messages.Where(x=>x.ReceiverMail==id).Count();
            ViewBag.d1 = deger1;

            var deger2 = c.Messages.Where(x => x.SenderMail == id).Count();
            ViewBag.d2 = deger2;

            var deger3 = c.Contacts.Count();
            ViewBag.d3 = deger3;
            return PartialView();
        }

    }
}