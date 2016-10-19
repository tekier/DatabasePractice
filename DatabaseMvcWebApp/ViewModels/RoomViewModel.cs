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
        private const string tableName = "Rooms";

        public RoomViewModel(string column)
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
        
        public void AddNewRoom()
        {
            facade.AddRowTo(tableName);
        }

        public void SearchById(string input)
        {
            facade.AddParameter("@RoomId", input);
        }

        public void SearchBySize(string input)
        {
            facade.AddParameter("@RoomSize", input);
        }

        public void SearchByFloor(string input)
        {
            facade.AddParameter("@Floor", input);
        }

        public void SearchByCapacity(string input)
        {
            facade.AddParameter("@Capacity", input);
        }
        public void RefreshTable()
        {
            rooms = facade.GetRowsUsing("Rooms", string.Empty);
        }
    }
}