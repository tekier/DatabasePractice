using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DatabaseConnection;

namespace DatabaseMvcWebApp.ViewModels
{
    public class EmployeeViewModel
    {
        public List<string> employees;
        public List<string> coloumnNames;
        private DatabaseFacade facade;

        public EmployeeViewModel(string tableName, string column)
        {
            facade = new DatabaseFacade();
            employees = facade.GetRowsUsing(tableName, column);
            coloumnNames = new List<string>
            {
                "ID",
                "Forename",
                "Surname",
                "DOB",
                "Role",
                "Room Number"
            };
        }
    }
}