﻿using System.Collections.Specialized;
using System.Security.Policy;
using FluentAssertions;
using NUnit.Framework;

namespace DatabaseConnectionTest
{
    [TestFixture]
    class InitialisedDatabaseClassShould
    {
        // ReSharper disable once InconsistentNaming
        private DatabaseInitialiser databaseInitTestObject;

        [TestFixtureSetUp]
        public void SetUp()
        {
            databaseInitTestObject = new DatabaseInitialiser("EmployeeList.txt");
        }

        [Test]
        public void SeeTheFileExistsAndIsNotEmpty()
        {
            SetUp();
            string output = databaseInitTestObject.ReadFile();
            output.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void ReadTheFirstLineOfTheInputFileCorrectly()
        {
            SetUp();
            string output = databaseInitTestObject.ReadFile();
            output.Should().Be("Renek Smallings 1995-04-09 DEV");
        }

        [Test]
        public void ReadTheSecondLineOfTheInputFileCorrectly()
        {
            string output = databaseInitTestObject.ReadFile();
            output.Should().Be("John Smith 1981-12-25 PR");
        }

        [Test]
        public void ReadTheThirdLineOfTheInputFileCorrectly()
        {
            string output = databaseInitTestObject.ReadFile();
            output.Should().Be("Tegan Marbles 1994-08-30 MARKETING");
        }

        [TestCase(0, "Renek")]
        [TestCase(1, "Smallings")]
        [TestCase(2, "1995-04-09")]
        [TestCase(3, "DEV")]
        [TestCase(5,"")]
        public void SplitTheFirstLineCorrectly(int index, string expectation)
        {
            SetUp();
            string output = databaseInitTestObject.ReadFile();
            string firstItemInLine = databaseInitTestObject.GetStringFromSplitLine(output, index);
            firstItemInLine.Should().Be(expectation);
        }

    }
}
