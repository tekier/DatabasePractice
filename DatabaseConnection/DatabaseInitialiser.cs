using System;
using System.Collections.Generic;
using System.IO;

namespace DatabaseConnection
{
    public class DatabaseInitialiser
    {
        public virtual string filename { get; set; }

        public DatabaseReadActivity DatabaseReadActivity
        {
            get { return _databaseReadActivity; }
        }

        //private StreamReader inputFile = new StreamReader();
        private StreamReader inputFile;
        private string pathToFile;
        private char[] delimiters = {' '};

        private readonly DatabaseReadActivity _databaseReadActivity;


        public DatabaseInitialiser(string filename)
        {
            this.filename = filename;
            string pathToFile = Path.Combine((AppDomain.CurrentDomain.BaseDirectory), "..", "..", filename);
            inputFile = new StreamReader(pathToFile);
            _databaseReadActivity = new DatabaseReadActivity();
            int i = 0;
        }

        public string ReadFile()
        {
            return inputFile.ReadLine();
        }

        public string GetStringFromSplitLine(string lineFromFile, int index)
        {
            string[] words = lineFromFile.Split(delimiters);
            try
            {
                   return words[index];
            }
            catch (IndexOutOfRangeException exception)
            {
                Console.WriteLine(exception.Message);
                return string.Empty;
            } 
        }

        public string[] GetStringFromSplitLine(string lineFromFile)
        {
            return lineFromFile.Split(delimiters); 
        }

        public int ReadWholeFile()
        {
            string line = ReadFile();
            int lineNumber = 0;

            while (line != null)
            {
                string[] splitLine = GetStringFromSplitLine(line);
                CreateTupledListAndAddToDatabase(splitLine, filename);
                //DatabaseReadActivity.AddRowToDatabase("Employees", inputList);
                line = ReadFile();
                lineNumber++;
            }
            inputFile.Close();
            return lineNumber;
        }

        public void CreateTupledListAndAddToDatabase(string[] entries, string file)
        {
            List<Tuple<string, string>> returnList = new List<Tuple<string, string>>();
            if (file == "EmployeeList.txt")
            {
                returnList.Add(Tuple.Create("@Forename", entries[0]));
                returnList.Add(Tuple.Create("@Surname", entries[1]));
                returnList.Add(Tuple.Create("@Dob", entries[2]));
                returnList.Add(Tuple.Create("Role",entries[3]));
                returnList.Add(Tuple.Create("@Room", entries[4]));
                //DatabaseReadActivity.AddRowToDatabase(returnList, "InsertNewEmployee");
            }
            if (file == "RoomList.txt")
            {
                returnList.Add(Tuple.Create("@RoomSize", entries[0]));
                returnList.Add(Tuple.Create("@Floor", entries[1]));
                returnList.Add(Tuple.Create("@Capacity", entries[2]));
                //DatabaseReadActivity.AddRowToDatabase(returnList, "InsertNewRoom");
            }
        }

    }
}