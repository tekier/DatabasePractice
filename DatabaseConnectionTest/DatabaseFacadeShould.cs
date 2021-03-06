﻿using NUnit.Framework;
using DatabaseConnection;
using FluentAssertions;

namespace DatabaseConnectionTest
{
    [TestFixture]
    class DatabaseFacadeShould
    {
        private DatabaseFacade facadeTestObject;

        [TestFixtureSetUp]
        public void SetUp()
        {
            facadeTestObject = new DatabaseFacade();
        }

        [Test]
        public void ReturnZeroWhenNoParametersHaveBeenSet()
        {
            SetUp();
            int actualLength = facadeTestObject.GetSizeOfParameterList();
            int expectedLength = 0;
            expectedLength.Should().Be(actualLength);

        }


        [Test]
        public void CorrectlyAddAParameterToParametersList()
        {
            SetUp();
            string inputParam = "@Forename";
            string inputParamValue = "Example Name";
            facadeTestObject.AddParameter(inputParam, inputParamValue);
            inputParam = "@Surname";
            inputParamValue = "yooo";
            facadeTestObject.AddParameter(inputParam, inputParamValue);
            int actualLength = facadeTestObject.GetSizeOfParameterList();
            int expectedLength = 2;

            actualLength.Should().Be(expectedLength);
        }

        [Test]
        public void CorrectlyRetrieveTheRightRowFromEmployeesTableBasedOnInputParameters()
        {
            SetUp();
            string inputParam = "@Forename";
            string inputParamValue = "Carlos";
            facadeTestObject.AddParameter(inputParam, inputParamValue);
            string actualOutput = facadeTestObject.GetRowsUsing("Employees", "SURNAME")[0];
            string expectedOutput = "Matos";

            actualOutput.Should().Be(expectedOutput);

        }

        [Test]
        public void CorrectlyRetrieveTheRightRowFromRoomsTableBasedOnInputParameters()
        {
            SetUp();
            string inputParam = "@RoomId";
            string inputParamValue = "5";
            facadeTestObject.AddParameter(inputParam, inputParamValue);
            string actualOutput = facadeTestObject.GetRowsUsing("Rooms", "ROOM_SIZE")[0];
            string expectedOutput = "small";

            actualOutput.Should().Be(expectedOutput);

        }

        [Test]
        public void CorrectlyRetrieveTheRightRowFromEmployeesTableBasedOnMultipleInputParameters()
        {
            SetUp();
            string inputParam1 = "@Surname";
            string inputParam2 = "@Role";
            string inputParam2Value = "DEV";
            string inputParam1Value = "Nimusiima";
            facadeTestObject.AddParameter(inputParam2, inputParam2Value);
            facadeTestObject.AddParameter(inputParam1, inputParam1Value);
            string actualOutput = facadeTestObject.GetRowsUsing("Employees", "FORENAME")[0];
            string expectedOutput = "Paul";

            actualOutput.Should().Be(expectedOutput);
        }

        [Test]
        public void CorrectlyRetrieveTheRightRowFromRoomsTableBasedOnMultipleInputParameters()
        {
            SetUp();
            string inputParam1 = "@RoomSize";
            string inputParam2 = "@Floor";
            string inputParam2Value = "3";
            string inputParam1Value = "small";
            facadeTestObject.AddParameter(inputParam2, inputParam2Value);
            facadeTestObject.AddParameter(inputParam1, inputParam1Value);
            string actualOutput = facadeTestObject.GetRowsUsing("Rooms", "CAPACITY")[0];
            string expectedOutput = "3";

            actualOutput.Should().Be(expectedOutput);
        }



        [Test]
        public void CorrectlyRetrieveMultipleRowsFromEmployeesTableBasedOnInputParameters()
        {
            SetUp();
            string inputParam = "@Room";
            string inputParamValue = "1";
            facadeTestObject.AddParameter(inputParam, inputParamValue);
            string actualOutput = facadeTestObject.GetRowsUsing("Employees", string.Empty)[8];
            string expectedOutput = "Smith";

            actualOutput.Should().Be(expectedOutput);
        }

        [Test]
        public void CorrectlyRetrieveMultipleRowsFromRoomsTableBasedOnInputParameters()
        {
            SetUp();
            string inputParam = "@RoomSize";
            string inputParamValue = "small";
            facadeTestObject.AddParameter(inputParam, inputParamValue);
            string actualOutput = facadeTestObject.GetRowsUsing("Rooms", string.Empty)[(4*4)-1];
            string expectedOutput = "5";

            actualOutput.Should().Be(expectedOutput);
        }

        //This test passed. Removed to avoid adding multiple entries to the database as
        //delete functionality has not been added in.
        //[Test]
        //public void CorrectlyWriteANewEntryToTheEmployeeDatabase()
        //{
        //    SetUp();
        //    List<Tuple<string,string>> testList = new List<Tuple<string,string>>
        //    {
        //        Tuple.Create("@Forename","New"),
        //        Tuple.Create("@Surname", "Entry"),
        //        Tuple.Create("@Dob", "2016-10-14"),
        //        Tuple.Create("@Role", "DEV"),
        //        Tuple.Create("@Room", "1")
        //    };

        //    facadeTestObject.AddRowTo("Employees", testList);
        //    facadeTestObject.AddParameter("@Surname", "Entry");
        //    string expectedOutput = "DEV";
        //    string actualOutput = facadeTestObject.GetRowsUsing("Employees","ROLE")[0];

        //    actualOutput.Should().Be(expectedOutput);

        //}

        //[Test]
        //public void CorrectlyWriteNewEntryToTheRoomDatabase()
        //{
        //    SetUp();
        //    List<Tuple<string, string>> testList = new List<Tuple<string, string>>
        //    {
        //        Tuple.Create("@RoomSize","medium"),
        //        Tuple.Create("@FloorNum","2"),
        //        Tuple.Create("@Capacity","1")
        //    };

        //    //facadeTestObject.AddRowTo("Rooms", testList);
        //    facadeTestObject.AddParameter("@Capacity", "1");
        //    facadeTestObject.AddParameter("@Floor", "2");
        //    string expectedOutput = "medium";
        //    string actualOutput = facadeTestObject.GetRowsUsing("Rooms", "ROOM_SIZE")[0];

        //    actualOutput.Should().Be(expectedOutput);
        //}
    }
}
