using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKamp.Controllers
{ 
    public class AdminCategoryController : Controller
    {
        // GET: AdminCategory
        CategoryManager cm = new CategoryManager(new EfCategoryDal());

        //Burada adminRolü B olanların yetkisi var
        //[Authorize(Roles="A")]
        public ActionResult Index()
        {
            var categoryList = cm.GetList();
            return View(categoryList);
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category par)
        {
            CategoryValidator categoryValidator = new CategoryValidator();
            ValidationResult results = categoryValidator.Validate(par);
            if (results.IsValid)
            {
                cm.CategoryAdd(par);
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

        public ActionResult DeleteCategory(int id)
        {
            var categoryList = cm.GetById(id);
            if (categoryList.CategoryStatus == true)
            {
                categoryList.CategoryStatus = false;
            }
            else
            {
                categoryList.CategoryStatus = true;
            }
            cm.CategoryDelete(categoryList);
            return RedirectToAction("Index");

            //var categoryList = cm.GetById(id);
            //cm.CategoryDelete(categoryList);
            //return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            var categoryList = cm.GetById(id);
            return View(categoryList);        
        }

        [HttpPost]
        public ActionResult EditCategory(Category par)
        {
            cm.CategoryUpdate(par);
            return RedirectToAction("Index");
        }

    }
}