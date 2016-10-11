using System;
using System.Data;
using System.Data.SqlClient;
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
        private string connectionString =
            "Data Source=ASGLH-WL-11919;Initial Catalog=PracticeDatabase;Integrated Security=True";
        
//#############################################################Constructor#####################################################################
        public DatabaseInitialiser(string filename)
        {
            this.filename = filename;
            string pathToFile = Path.Combine((AppDomain.CurrentDomain.BaseDirectory), "..", "..", filename);
            inputFile = new StreamReader(pathToFile);
        }
//#############################################################################################################################################
        public string ReadFile()
        {
            return inputFile.ReadLine();
        }
//#############################################################################################################################################
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
//#######################################################################^ overloads v#########################################################      
        public string[] GetStringFromSplitLine(string lineFromFile)
        {
            return lineFromFile.Split(delimiters); 
        }
//#############################################################################################################################################
        public int ReadWholeFile()
        {
            string line = ReadFile();
            int lineNumber = 0;

            while (line != null)
            {
                string[] splitLine = GetStringFromSplitLine(line);
                line = ReadFile();
                lineNumber++;
            }
            inputFile.Close();
            return lineNumber;
        }
//#############################################################################################################################################
        public void AddRowToDatabase(string table, string[] input)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                DirectToStoredProcedure(conn, table, input);
                conn.Close();
            }
        }
//#############################################################################################################################################
        private void DirectToStoredProcedure(SqlConnection conn, string table, string[] input)
        {
            try
            {
                SqlCommand command;
                
                if (table.ToLower().Equals("employees"))
                {
                    command = new SqlCommand("InsertNewEmployee", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.Add(new SqlParameter("@Forename", input[0]));
                    command.Parameters.Add(new SqlParameter("@Surname", input[1]));
                    command.Parameters.Add(new SqlParameter("@Dob", input[2]));
                    command.Parameters.Add(new SqlParameter("@Role", input[3]));
                    command.Parameters.Add(new SqlParameter("@Room", input[4]));

                    ExecuteSqlStoredProcedure(command);

                }
                if (table.ToLower().Equals("rooms"))
                {
                    command = new SqlCommand("InsertNewRoom", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.Add(new SqlParameter("@RoomSize", input[0]));
                    command.Parameters.Add(new SqlParameter("@FloorNum", input[1]));
                    command.Parameters.Add(new SqlParameter("@Capacity", input[3]));
                    
                    ExecuteSqlStoredProcedure(command);
                }

            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
            }
        }
//#############################################################################################################################################
        private void ExecuteSqlStoredProcedure(SqlCommand command)
        {
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}