using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKamp.Controllers
{
    public class AboutController : Controller
    {
        AboutManager abm = new AboutManager(new EfAboutDal());
        public ActionResult Index()
        {
            var aboutList = abm.GetList();
            return View(aboutList);
        }

        [HttpGet]
        public ActionResult AddAbout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAbout(About par)
        {
            if (par.AboutImage1 != null)
            {
                string filename = Path.GetFileName(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/About/" + filename + extension;
                Request.Files[0].SaveAs(Server.MapPath(path));
                par.AboutImage1 = "/Images/About/" + filename + extension;
            }
            abm.AboutAdd(par);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAbout(int id)
        {
            //abm.AboutDelete(par);
            //return RedirectToAction("Index");

            var aboutList = abm.GetById(id);
            abm.AboutDelete(aboutList);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditAbout(int id)
        {
            var aboutList = abm.GetById(id);
            Session["AboutImage1"] = aboutList.AboutImage1;
            return View(aboutList);
        }

        [HttpPost]
        public ActionResult EditAbout(About par)
        {
            if (par.AboutImage1 != null)
            {
                string filename = Path.GetFileName(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/About/" + filename + extension;
                Request.Files[0].SaveAs(Server.MapPath(path));
                par.AboutImage1 = "/Images/About/" + filename + extension;
                string oldImgPath = Request.MapPath(Session["AboutImage1"].ToString());
                if (System.IO.File.Exists(oldImgPath))
                {
                    System.IO.File.Delete(oldImgPath);
                }
            }
            par.AboutImage1 = Session["AboutImage1"].ToString();
            abm.AboutUpdate(par);
            return RedirectToAction("Index");
        }

        public PartialViewResult AboutPartial()
        {
            return PartialView();
        }
    }
}