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

        public void SearchById(string input)
        {
            facade.AddParameter("@Id", input);
        }
        
        public void SearchByFirstName(string input)
        {
            facade.AddParameter("@Forename", input);
        }
        
        public void SearchBySurname(string input)
        {
            facade.AddParameter("@Surname", input);
        }
        
        public void SearchByDob(string input)
        {
            facade.AddParameter("@Dob", input);
        }
        
        public void SearchByRole(string input)
        {
            facade.AddParameter("@Role", input);
        }
        
        public void SearchByRoom(string input)
        {
            facade.AddParameter("@Room", input);
        }

        public void RefreshTable()
        {
            employees = facade.GetRowsUsing("Employees", string.Empty);
        }

    }
}