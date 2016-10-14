using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnection
{
    public class DatabaseFacade
    {
        private DatabaseActivity AccessObject;
        private const string StoredProcedureToGetEmployee = "SelectFromEmployees";
        private const string StoredProcedureToGetRoom = "SelectFromRooms";
        private List<Tuple<string, string>> ListOfParameters;

        public DatabaseFacade()
        {
            AccessObject = new DatabaseActivity();
            ListOfParameters = new List<Tuple<string, string>>();
        }

        public void AddParameter(string newParameter, string newParameterValue)
        {
            ListOfParameters.Add(Tuple.Create(newParameter, newParameterValue));
        }

        public int GetSizeOfParameterList()
        {
            return ListOfParameters.Count;
        }

        public string GetRowsUsing(string whichTable, string column)
        {
            try
            {
                string returnValue = "No table";

                if (whichTable.ToLower() == "employees")
                {
                   returnValue = GetRowsFromEmployees(column);
                }
                if (whichTable.ToLower() == "rooms")
                {
                   returnValue = GetRowsFromRooms(column);
                }
                return returnValue;
            }
            catch (Exception e)
            {
                
                Console.WriteLine(e.Message);
                return "failed";
            }
        }

        private string GetRowsFromEmployees(string column)
        {
            return AccessObject.GetRowFromTableByNameWithProcedure(ListOfParameters, StoredProcedureToGetEmployee, column);
        }

        private string GetRowsFromRooms(string column)
        {
            return AccessObject.GetRowFromTableByNameWithProcedure(ListOfParameters, StoredProcedureToGetRoom, column);
        }
    }
}
