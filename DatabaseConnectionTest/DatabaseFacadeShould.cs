using NUnit.Framework;
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
            string actualOutput = facadeTestObject.GetRowsUsing("Employees", "SURNAME");
            string expectedOutput = "Matos";

            actualOutput.Should().Be(expectedOutput);

        }

        [Test]
        public void CorrectlyRetrieveTheRightRowFromRowsTableBasedOnInputParameters()
        {
            SetUp();
            string inputParam = "@RoomId";
            string inputParamValue = "5";
            facadeTestObject.AddParameter(inputParam, inputParamValue);
            string actualOutput = facadeTestObject.GetRowsUsing("Rooms", "ROOM_SIZE");
            string expectedOutput = "small";

            actualOutput.Should().Be(expectedOutput);

        }
    }
}
