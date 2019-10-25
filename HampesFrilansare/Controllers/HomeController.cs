using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HampesFrilansare.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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

        public ActionResult SignupFreelance()
        {
            ViewBag.Message = "Your freelance sign up page.";

            return View();
        }

        public ActionResult SignupCustomer()
        {
            ViewBag.Message = "Your customer sign up page.";

            return View();
        }

        public ActionResult LogIn()
        {
            ViewBag.Message = "Your log in page.";

            return View();
        }
    }
}