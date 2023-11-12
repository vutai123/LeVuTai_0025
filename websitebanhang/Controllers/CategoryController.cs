using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using websitebanhang.Context;

namespace websitebanhang.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        WebSiteBhEntities objWebSiteBhEntities = new WebSiteBhEntities();
        public ActionResult Index()
        {
           
            var lstCategory = objWebSiteBhEntities.Categories.ToList();
            return View(lstCategory);
        }
        public ActionResult ProductCategory(int Id)
        {
            WebSiteBhEntities objWebSiteBhEntities = new WebSiteBhEntities();
            var listProduct = objWebSiteBhEntities.Products.Where(n => n.CategoryId == Id).ToList();
            return View(listProduct);
        }
    }
}