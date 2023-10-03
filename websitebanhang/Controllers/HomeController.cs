using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using websitebanhang.Context;

namespace websitebanhang.Controllers
{
    public class HomeController : Controller
    {
        WebSiteBhEntities ojbModel = new WebSiteBhEntities();
        public ActionResult Index()
        {
            var lstProduct = ojbModel.Products.ToList();
            return View(lstProduct);
        }

    }
}