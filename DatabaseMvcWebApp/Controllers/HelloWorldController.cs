using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace DatabaseMvcWebApp.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET: HelloWorld
        public string Index()
        {
            string val = "Hello from the index in the HelloWorldController class.";
            return "<h1>"+val+"</h1>";
        }

        public ActionResult Example(int repeat = 10, string name = "Default")
        {
            ViewBag.Message = name;
            ViewBag.NumTimes = repeat;
            return View();
        }
    }
}