using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseMvcWebApp.ViewModels;
using Microsoft.Ajax.Utilities;

namespace DatabaseMvcWebApp.Controllers
{
    public class UpdateRoomsController : Controller
    {
        // GET: UpdateRooms
        RoomViewModel model = new RoomViewModel(String.Empty);

        public ActionResult Index()
        {
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult UpdateNewRoom(string id, string size = null, string floor = null,
            string capacity = null)
        {
            try
            {
                model.SearchById(id);
            }
            catch (Exception e)
            {
                Console.WriteLine("No ID found in controller to check against.");
                throw;
            }

            if (!size.IsNullOrWhiteSpace())
            {
                model.SearchBySize(size);
            }
            if (!floor.IsNullOrWhiteSpace())
            {
                model.SearchByFloor(floor);
            }
            if (!capacity.IsNullOrWhiteSpace())
            {
                model.SearchByCapacity(capacity);
            }

            model.UpdateRoom();
            model.RefreshTable();

            return RedirectToAction("Index");
        }
    }
}