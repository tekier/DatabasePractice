using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseConnection
{
    public class DatabaseActivity
    {
        private string connectionString =
            "Data Source=ASGLH-WL-11919;Initial Catalog=PracticeDatabase;Integrated Security=True";

        public DatabaseActivity()
        {
        }

        public void AddRowToDatabase(string table, string[] input)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                DirectToStoredProcedure(conn, table, input);
                conn.Close();
            }
        }

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