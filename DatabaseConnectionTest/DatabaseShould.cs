using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseConnection;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace DatabaseConnectionTest
{
    [TestFixture]
    class DatabaseShould
    {
        // ReSharper disable once InconsistentNaming
        private DatabaseInitialiser databaseInitTestObject;
        // ReSharper disable once NotAccessedField.Local
        // ReSharper disable once InconsistentNaming
        [TestFixtureSetUp]
        public void SetUp()
        {
            databaseInitTestObject = new DatabaseInitialiser("EmployeeList.txt");
        }
        
        [Test]
        public void WriteFirstLineToDataBase()
        {
            SetUp();
            string[] inputToDb = databaseInitTestObject.GetStringFromSplitLine(databaseInitTestObject.ReadFile());
            string table = "Employee";
            databaseInitTestObject.DatabaseActivity.AddRowToDatabase(table, inputToDb);
            string expectedOutput = "Renek";
            string actualOutput = "";
        }

    }
}
