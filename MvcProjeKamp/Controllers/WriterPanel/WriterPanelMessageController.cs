using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKamp.Controllers.WriterPanel
{
    public class WriterPanelMessageController : Controller
    {
        // GET: WriterPanelMessage
        MessageManager mm = new MessageManager(new EfMessageDal());
        MessageValidator messagevalidations = new MessageValidator();
       
        public ActionResult Inbox()
        {
            string p = (string)Session["WriterEmail"];
            var messageList = mm.GetListInbox(p);
            return View(messageList);
        }

        public ActionResult Sendbox()
        {
            string p = (string)Session["WriterEmail"];
            var messageList = mm.GetListSendbox(p);
            return View(messageList);
        }

        public ActionResult GetInBoxMessageDetails(int id)
        {
            var values = mm.GetById(id);
            return View(values);
        }

        public ActionResult GetSendBoxMessageDetails(int id)
        {
            var values = mm.GetById(id);
            return View(values);
        }
   
        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewMessage(Message par)
        {
            string sender = (string)Session["WriterEmail"];
            ValidationResult results = messagevalidations.Validate(par);
            if (results.IsValid)
            {
                par.SenderMail = sender;
                par.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                mm.MessageAdd(par);
                return RedirectToAction("SendBox");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        public PartialViewResult ContactPartial(Context c)
        { 
            var id = (string)Session["WriterEmail"];
            var deger1 = c.Messages.Where(x => x.ReceiverMail == id).Count();
            ViewBag.d1 = deger1;

            var deger2 = c.Messages.Where(x => x.SenderMail == id).Count();
            ViewBag.d2 = deger2;

            return PartialView();
        }

    }
}