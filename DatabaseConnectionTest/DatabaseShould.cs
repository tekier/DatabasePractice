using DatabaseConnection;
using FluentAssertions;
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
            string table = "Employees";
            string expectedOutput = "Renek";
            string actualOutput = databaseInitTestObject.DatabaseActivity.GetRowFromEmployeeTableByName("@Surname",
                "Smallings");
            actualOutput.Should().Be(expectedOutput);
            
            //string[] inputToDb = databaseInitTestObject.GetStringFromSplitLine(databaseInitTestObject.ReadFile());
            //databaseInitTestObject.DatabaseActivity.AddRowToDatabase(table, inputToDb);
        }
    }
}
