using System;
using System.Collections.Generic;
using DatabaseConnection;
using FluentAssertions;
using NUnit.Framework;

namespace DatabaseConnectionTest
{
    [TestFixture]
    class DatabaseIoActivityShould
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
        public void WriteAndReadFirstEmployeeLineToDatabase()
        {
            SetUp();
            string expectedOutput = "Renek";
            List<Tuple<string, string>> parameters = new List<Tuple<string, string>>
            {
                Tuple.Create("@Surname", "Bigg")
            };
            string actualOutput = databaseInitTestObject.DatabaseReadActivity.GetRowFromTableByNameWithProcedure(
                parameters, "SelectFromEmployees", "FORENAME")[0];
            actualOutput.Should().Be(expectedOutput);
        }

        [Test]
        public void WriteAndReadLastEmployeeLineToDatabase()
        {
            SetUp();
            string expectedOutput = "Lena";
            List<Tuple<string, string>> parameters = new List<Tuple<string, string>>
            {
                Tuple.Create("@Room", "3"),
                Tuple.Create("@Surname", "Blos")
            };
            string actualOutput = databaseInitTestObject.DatabaseReadActivity.GetRowFromTableByNameWithProcedure(
                parameters, "SelectFromEmployees", "FORENAME")[0];
            actualOutput.Should().Be(expectedOutput);
        }

        [Test]
        public void WriteAndReadFirstRoomLineToDatabase()
        {
            SetUp();
            string expectedOutput = "29";
            List<Tuple<string, string>> parameters = new List<Tuple<string, string>>
            {
                Tuple.Create("@RoomId", "1")
            };
            string actualOutput = databaseInitTestObject.DatabaseReadActivity.GetRowFromTableByNameWithProcedure(
                parameters, "SelectFromRooms", "CAPACITY")[0];
            actualOutput.Should().Be(expectedOutput);
        }

        [Test]
        public void WriteAndReadLastRoomLineToDatabase()
        {
            SetUp();
            string expectedOutput = "3";
            List<Tuple<string, string>> parameters = new List<Tuple<string, string>>
            {
                Tuple.Create("@Floor", "3"),
                Tuple.Create("@RoomSize", "small")
            };
            string actualOutput = databaseInitTestObject.DatabaseReadActivity.GetRowFromTableByNameWithProcedure(
                parameters, "SelectFromRooms", "CAPACITY")[0];
            actualOutput.Should().Be(expectedOutput);
        }
    }
}