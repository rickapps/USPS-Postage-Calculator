using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestBed.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "USPS Postage Rate Calculator.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "RickApps";

            return View();
        }
    }
}