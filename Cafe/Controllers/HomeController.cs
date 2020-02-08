using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cafe.DAL;

namespace Cafe.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (dbCafeEntities db = new dbCafeEntities())
            {
                var result = (from s in db.Products select s).ToList();
                return View(result);
            }
        }

        public ActionResult Index2()
        {
            return Content(
                "<html><body><h1>message</h1></body></html>"
                );
        }

        public ActionResult About()
        {
            ViewBag.Message = "This is a Cafe project. I hope you will enjoy here.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}