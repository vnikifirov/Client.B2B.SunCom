using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test1708151058.Models;

namespace Test1708151058.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet] // GET /home/index
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet] // GET /home/About
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet] // GET /home/contact
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        } 

        // GET /home/create
        [HttpGet]       
        public ActionResult Create()
        {
            var product = new Product();

            return View(product);
        }
    }
}