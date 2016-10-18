using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseConnection
{
    public class DatabaseWriteActivity
    {
        private string connectionString =
        "Data Source=ASGLH-WL-11919;Initial Catalog=PracticeDatabase;Integrated Security=True";

        public void AddRowToDatabase(List<Tuple<string,string>> input, string storedProcedure)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                DirectToStoredProcedure(conn, input, storedProcedure);
                conn.Close();
            }
        }

        private void DirectToStoredProcedure(SqlConnection conn, List<Tuple<string, string>> parameters, string storedProc)
        {
            try
            {
                SqlCommand command = BuildCommand(conn, parameters, storedProc);
                ExecuteSqlStoredProcedure(command);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static SqlCommand BuildCommand(SqlConnection conn, List<Tuple<string,string>> parameters, string storedProc)
        {
            var command = new SqlCommand(storedProc, conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            foreach (var param in parameters)
            {
                var paramSpec = param.Item1;
                var paramVal = param.Item2;
                command.Parameters.Add(new SqlParameter(paramSpec, paramVal));
            }
            return command;
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