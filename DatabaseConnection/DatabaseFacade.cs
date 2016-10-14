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
        private DatabaseActivity accessObject;
        private const string defaultOption = "ignore";
        private const string StoredProcedureToGetEmployee = "SelectFromEmployee";
        private List<Tuple<string, string>> listOfParameters;

        public DatabaseFacade()
        {
            accessObject = new DatabaseActivity();
            listOfParameters = new List<Tuple<string, string>>();
        }

        public void AddParameter(string newParameter, string newParameterValue)
        {
            listOfParameters.Add(Tuple.Create(newParameter, newParameterValue));
        }

        public int GetSizeOfParameterList()
        {
            return listOfParameters.Count;
        }
    }
}
