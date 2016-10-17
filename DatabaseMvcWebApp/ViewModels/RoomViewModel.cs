using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DatabaseConnection;

namespace DatabaseMvcWebApp.ViewModels
{
    public class RoomViewModel
    {
        public List<string> rooms;
        public List<string> coloumnNames;
        private DatabaseFacade facade;

        public RoomViewModel(string tableName, string column)
        {
            facade = new DatabaseFacade();
            rooms = facade.GetRowsUsing(tableName, column);
            coloumnNames = new List<string>
            {
                "ID",
                "Size",
                "Floor",
                "Capacity"
            };
        }

        public void SearchById(string input)
        {
            facade.AddParameter("@RoomId", input);
        }

        public void SearchByFirstName(string input)
        {
            facade.AddParameter("@RoomSize", input);
        }

        public void SearchBySurname(string input)
        {
            facade.AddParameter("@Floor", input);
        }

        public void SearchByDob(string input)
        {
            facade.AddParameter("@Capacity", input);
        }
        public void RefreshTable()
        {
            rooms = facade.GetRowsUsing("Rooms", string.Empty);
        }
    }
}