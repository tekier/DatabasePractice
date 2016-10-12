using System;
using System.IO;

namespace DatabaseConnection
{
    public class DatabaseInitialiser
    {
        public virtual string filename { get; set; }

        public DatabaseActivity DatabaseActivity
        {
            get { return _databaseActivity; }
        }

        //private StreamReader inputFile = new StreamReader();
        private StreamReader inputFile;
        private string pathToFile;
        private char[] delimiters = {' '};

        private readonly DatabaseActivity _databaseActivity;


        public DatabaseInitialiser(string filename)
        {
            this.filename = filename;
            string pathToFile = Path.Combine((AppDomain.CurrentDomain.BaseDirectory), "..", "..", filename);
            inputFile = new StreamReader(pathToFile);
            _databaseActivity = new DatabaseActivity();
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
                DatabaseActivity.AddRowToDatabase("Employees", splitLine);
                line = ReadFile();
                lineNumber++;
            }
            inputFile.Close();
            return lineNumber;
        }

    }
}