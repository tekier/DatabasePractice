using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseMvcWebApp.ViewModels;
using Microsoft.Ajax.Utilities;

namespace DatabaseMvcWebApp.Controllers
{
    public class UpdateEmployeesController : Controller
    {
        // GET: Update
        private EmployeeViewModel model = new EmployeeViewModel(string.Empty);

        public ActionResult Index()
        {
            return View(model);
        }

        [HttpPost] 
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult UpdateNewEmployee(string id, string forename = null, string surname = null, string dob = null,
            string role = null, string room = null)
        {
            try
            {
                model.SearchById(id);

            }
            catch (Exception exception)
            {
                Console.WriteLine("No ID found in controller to check against.");
                throw;
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
            model.UpdateEmployee();
            model.RefreshTable();
            return RedirectToAction("Index");
        }
    }
}