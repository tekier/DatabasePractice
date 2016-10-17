using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseMvcWebApp.ViewModels;
using Microsoft.Ajax.Utilities;

namespace DatabaseMvcWebApp.Controllers
{
    public class RoomsController : Controller
    {
        // GET: Rooms
        RoomViewModel model = new RoomViewModel("Rooms", String.Empty);
        public ActionResult Index()
        {
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string id = null, string size = null, string floor = null, string capacity = null)
        {
            if (!id.IsNullOrWhiteSpace())
            {
                model.SearchById(id);
            }
            if (!size.IsNullOrWhiteSpace())
            {
                model.SearchByFirstName(size);
            }
            if (!floor.IsNullOrWhiteSpace())
            {
                model.SearchBySurname(floor);
            }
            if (!capacity.IsNullOrWhiteSpace())
            {
                model.SearchByDob(capacity);
            }
            model.RefreshTable();
            return View(model);
        }
    }
}