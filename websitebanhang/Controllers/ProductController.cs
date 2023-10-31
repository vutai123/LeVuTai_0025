using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using websitebanhang.Context;

namespace websitebanhang.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        WebSiteBhEntities objWebSiteBhEntities = new WebSiteBhEntities();
        // GET: Product
        public ActionResult Detail(int Id)
        {
            var objProduct = objWebSiteBhEntities.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
    }
}