using System;
using System.Collections.Generic;

namespace DatabaseConnection
{
    public class DatabaseFacade
    {
        private DatabaseActivity AccessObject;
        private const string StoredProcedureToGetEmployee = "SelectFromEmployees";
        private const string StoredProcedureToGetRoom = "SelectFromRooms";
        private const string StoredProcedureToAddEmployee = "InsertNewEmployee";
        private const string StoredProcedureToAddRoom = "InsertNewRoom";
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

        public List<string> GetRowsUsing(string whichTable, string column)
        {
            List<string> returnValues = new List<string>();
            try
            {
                if (whichTable.ToLower() == "employees")
                {
                    returnValues = GetRowsFromEmployees(column);
                }
                if (whichTable.ToLower() == "rooms")
                {
                    returnValues = GetRowsFromRooms(column);
                }
                return returnValues;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                returnValues.Add("failed");
                return returnValues;
            }
        }

        private List<string> GetRowsFromEmployees(string column)
        {
            return AccessObject.GetRowFromTableByNameWithProcedure(ListOfParameters, StoredProcedureToGetEmployee,
                column);
        }

        private List<string> GetRowsFromRooms(string column)
        {
            return AccessObject.GetRowFromTableByNameWithProcedure(ListOfParameters, StoredProcedureToGetRoom, column);
        }

        public void AddRowTo(string whichTable, List<Tuple<string, string>> inputList)
        {
            try
            {
                if (whichTable.ToLower() == "employees")
                {
                    AddRowToEmployees(inputList);
                }
                if (whichTable.ToLower() == "rooms")
                {
                    AddRowToRooms(inputList);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void AddRowToRooms(List<Tuple<string, string>> inputList)
        {
            AccessObject.AddRowToDatabase(inputList, StoredProcedureToAddRoom);
        }

        private void AddRowToEmployees(List<Tuple<string, string>> inputList)
        {
            AccessObject.AddRowToDatabase(inputList, StoredProcedureToAddEmployee);   
        }
    }
}
