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
    public class SliderController : Controller
    {
        SliderManager sm = new SliderManager(new EfSliderDal());
        public ActionResult Index()
        {
            var slider = sm.GetList();
            return View(slider);
        }

        [HttpGet]
        public ActionResult AddSlider()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSlider(Slider par)
        {
            if (par.SliderPath != null)
            {
                string filename = Path.GetFileName(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/Slider/" + filename + extension;
                Request.Files[0].SaveAs(Server.MapPath(path));
                par.SliderPath = "/Images/Slider/" + filename + extension;
            }
            sm.SliderAdd(par);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult EditSlider(int id)
        {
            var sliderList = sm.GetById(id);
            Session["AdminID"] = sliderList.SliderPath;
            return View(sliderList);
        }

        [HttpPost]
        public ActionResult EditSlider(Slider par)
        {
            if (par.SliderPath != null)
            {
                string filename = Path.GetFileName(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/Slider/" + filename + extension;
                Request.Files[0].SaveAs(Server.MapPath(path));
                par.SliderPath = "/Images/Slider/" + filename + extension;
                string oldImgPath = Request.MapPath("~/Images/Slider/" + filename);
                if (System.IO.File.Exists(oldImgPath))
                {
                    System.IO.File.Delete(oldImgPath);
                }
            }           
            //par.SliderPath = Session["AdminID"].ToString();
            sm.SliderUpdate(par);
            return RedirectToAction("Index");
        }


    }
}