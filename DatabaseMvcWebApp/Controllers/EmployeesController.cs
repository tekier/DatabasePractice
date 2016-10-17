using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseConnection;
using DatabaseMvcWebApp.ViewModels;
using Microsoft.Ajax.Utilities;

namespace DatabaseMvcWebApp.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        private EmployeeViewModel model = new EmployeeViewModel("Employees", string.Empty);

        public ActionResult Index()
        {
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string id = null, string forename = null, string surname = null, string dob = null,
            string role = null, string room = null)
        {
            if (!id.IsNullOrWhiteSpace())
            {
                model.SearchById(id);
            }
            if (!forename.IsNullOrWhiteSpace())
            {
                model.SearchByFirstName(forename);
            }
            if (!surname.IsNullOrWhiteSpace())
            {
                model.SearchBySurname(surname);
            }
            if (!dob.IsNullOrWhiteSpace())
            {
                model.SearchByDob(dob);
            }
            if (!role.IsNullOrWhiteSpace())
            {
                model.SearchByRole(role);
            }
            if (!room.IsNullOrWhiteSpace())
            {
                model.SearchByRoom(room);
            }
            model.RefreshTable();
            return View(model);
        }
    }
}