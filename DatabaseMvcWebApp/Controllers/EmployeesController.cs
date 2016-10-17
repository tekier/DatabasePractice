using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseConnection;
using DatabaseMvcWebApp.ViewModels;

namespace DatabaseMvcWebApp.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        public ActionResult Index()
        {
            EmployeeViewModel model = new EmployeeViewModel("Employees", String.Empty);
            return View(model);
        }
    }
}