using System;
using System.Collections.Generic;

namespace DatabaseConnection
{
    public class DatabaseFacade
    {
        private DatabaseReadActivity ReadAccessObject;
        private DatabaseWriteActivity WriteAccessObject;
        private DatabaseDeleteActivity DeleteAccessObject;
        private const string StoredProcedureToGetEmployee = "SelectFromEmployees";
        private const string StoredProcedureToGetRoom = "SelectFromRooms";
        private const string StoredProcedureToAddEmployee = "InsertNewEmployee";
        private const string StoredProcedureToAddRoom = "InsertNewRoom";
        private const string StoredProcedureToDeleteEmployee = "DeleteFromEmployees";
        private const string StoredProcedureToDeleteRoom = "DeleteFromRooms";
        private List<Tuple<string, string>> ListOfParameters;

        public DatabaseFacade()
        {
            ReadAccessObject = new DatabaseReadActivity();
            WriteAccessObject = new DatabaseWriteActivity();
            DeleteAccessObject = new DatabaseDeleteActivity();
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
            return ReadAccessObject.GetRowFromTableByNameWithProcedure(ListOfParameters, StoredProcedureToGetEmployee,
                column);
        }

        private List<string> GetRowsFromRooms(string column)
        {
            return ReadAccessObject.GetRowFromTableByNameWithProcedure(ListOfParameters, StoredProcedureToGetRoom, column);
        }
/*-------------------------------------------------------------------------------------------------------------------------*/
        public void AddRowTo(string whichTable)
        {
            try
            {
                if (whichTable.ToLower() == "employees")
                {
                    AddRowToEmployees();
                }
                if (whichTable.ToLower() == "rooms")
                {
                    AddRowToRooms();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void AddRowToRooms()
        {
            WriteAccessObject.AddRowToDatabase(ListOfParameters, StoredProcedureToAddRoom);
        }

        private void AddRowToEmployees()
        {
            WriteAccessObject.AddRowToDatabase(ListOfParameters, StoredProcedureToAddEmployee);
        }

        /*-------------------------------------------------------------------------------------------------------------*/
        public void DeleteRowFrom(string whichTable)
        {
            try
            {
                if (whichTable.ToLower() == "employees")
                {
                    DeleteRowFromEmployees();
                }
                if (whichTable.ToLower() == "rooms")
                {
                    DeleteRowFromRooms();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void DeleteRowFromRooms()
        {
            DeleteAccessObject.DeleteRowFromTableByNameWithStoredProcedure(ListOfParameters, StoredProcedureToDeleteRoom);
        }

        private void DeleteRowFromEmployees()
        {
            DeleteAccessObject.DeleteRowFromTableByNameWithStoredProcedure(ListOfParameters, StoredProcedureToDeleteEmployee);
        }
    }

}
