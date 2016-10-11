using System;
using System.IO;

namespace DatabaseConnectionTest
{

    class DatabaseInitialiser
    {
        public virtual string filename { get; set; }
        //private StreamReader inputFile = new StreamReader();
        private StreamReader inputFile;
        private string pathToFile;
        private char[] delimiters = {' '};

        public DatabaseInitialiser(string filename)
        {
            this.filename = filename;
            string pathToFile = Path.Combine((AppDomain.CurrentDomain.BaseDirectory), "..", "..", filename);
            inputFile = new StreamReader(pathToFile);
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
                return string.Empty;
            } 
        }
    }
}