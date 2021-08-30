using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKamp.Controllers.WriterPanel
{
    public class WriterPanelContentController : Controller
    {
        // GET: WriterPanelContent
        ContentManager cm = new ContentManager(new EfContentDal());
        Context c = new Context();
        public ActionResult MyContent(string p)
        {
            Context c = new Context();
            p = (string)Session["WriterEmail"];
            var writeridinfo = c.Writers.Where(x => x.WriterEmail == p).Select(y => y.WriterID).FirstOrDefault();         
            var contentList = cm.GetListByWriter(writeridinfo);
            return View(contentList);
        }

        [HttpGet]
        public ActionResult AddContent(int id)
        {
            ViewBag.d = id;
            return View();
        }

        [HttpPost]
        public ActionResult AddContent(Content par)
        {
            string mail = (string)Session["WriterEmail"];
            var writeridinfo = c.Writers.Where(x => x.WriterEmail == mail).Select(y => y.WriterID).FirstOrDefault();
            par.ContentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            par.WriterID = writeridinfo;
            par.ContentStatus = true;
            cm.ContentAdd(par);
            return RedirectToAction("MyContent");
        }

    }
}