using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKamp.Controllers
{
    public class WriterController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterDal());
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        WriterValidator writervalidator = new WriterValidator();
       
        public ActionResult Index()
        {
            var WriterList = wm.GetList();
            return View(WriterList);
        }

        public ActionResult MyHeading(int id)
        {            
           var values = hm.GetListByWriter(id);        
           return View(values);
        }

        [HttpGet]
        public ActionResult AddWriter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddWriter(Writer par)
        {          
            ValidationResult results = writervalidator.Validate(par);
            if (results.IsValid)
            {
                if (par.WriterImage != null)
                {
                    string filename = Path.GetFileName(Request.Files[0].FileName);
                    string extension = Path.GetExtension(Request.Files[0].FileName);
                    string path = "~/Images/" + filename + extension;
                    Request.Files[0].SaveAs(Server.MapPath(path));
                    par.WriterImage = "/Images/" + filename + extension;
                }
                wm.WriterAdd(par);
                return RedirectToAction("Index");
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

        [HttpGet]
        public ActionResult EditWriter(int id)
        {
            var writerList = wm.GetById(id);
            Session["WriterImage"] = writerList.WriterImage;
            return View(writerList);
        }

        [HttpPost]
        public ActionResult EditWriter(Writer par)
        {         
            ValidationResult results = writervalidator.Validate(par);
            if (results.IsValid)
            {
                if (par.WriterImage != null)
                {
                    string filename = Path.GetFileName(Request.Files[0].FileName);
                    string extension = Path.GetExtension(Request.Files[0].FileName);
                    string path = "~/Images/" + filename + extension;
                    Request.Files[0].SaveAs(Server.MapPath(path));
                    par.WriterImage = "/Images/" + filename + extension;
                    string oldImgPath = Request.MapPath(Session["WriterImage"].ToString());
                    if (System.IO.File.Exists(oldImgPath))
                    {
                        System.IO.File.Delete(oldImgPath);
                    }
                }
                //par.WriterImage = Session["WriterImage"].ToString();
                wm.WriterUpdate(par);
                return RedirectToAction("Index");
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

    }
}