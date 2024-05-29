using APIInsideExistingMVCProject.Models;
using APIInsideExistingMVCProject.Models.APIEFs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APIInsideExistingMVCProject.Controllers
{
    public class HomeController : Controller
    {
        APIEFs db;
        public ActionResult Index()
        {
            db = new APIEFs();
            var customers = db.Customers.Take(20).ToList();
            return View(customers);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}