using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        private EmployeeViewModel model = new EmployeeViewModel(string.Empty);

        public ActionResult Index()
        {
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult AddNewEmployee(string forename = null, string surname = null, string dob = null,
            string role = null, string room = null)
        {

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
            if (room != null)
            {
                model.AddNewEmployee();
            } 
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult DeleteEmployee(string id = null, string forename = null, string surname = null, string dob = null,
            string role = null, string room = null)
        {
            int count = 0;

            if (!id.IsNullOrWhiteSpace())
            {
                model.SearchById(id);
                count++;
            }
            if (!forename.IsNullOrWhiteSpace())
            {
                model.SearchByFirstName(forename);
                count++;
            }
            if (!surname.IsNullOrWhiteSpace())
            {
                model.SearchBySurname(surname);
                count++;
            }
            if (!dob.IsNullOrWhiteSpace())
            {
                model.SearchByDob(dob);
                count++;
            }
            if (!role.IsNullOrWhiteSpace())
            {
                model.SearchByRole(role);
                count++;
            }
            if (!room.IsNullOrWhiteSpace())
            {
                model.SearchByRoom(room);
                count++;
            }

            if (count > 0)
            {
                model.DeleteEmployee();
            }

            return RedirectToAction("Index");
        }
    }
}