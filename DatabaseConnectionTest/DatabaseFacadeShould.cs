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
            string inputParamValue = "exampleName";
            facadeTestObject.AddParameter(inputParam, inputParamValue);

            int expectedLength = 1;
            int actualLength = facadeTestObject.GetSizeOfParameterList();

            actualLength.Should().Be(expectedLength);
        }
    }
}
