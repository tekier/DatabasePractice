using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DatabaseConnection
{
    public class DatabaseActivity
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

/*-----------------------------------------------------------------------------------------------------------------------------*/

        public string GetRowFromTableByNameWithProcedure(List<Tuple<string, string>> parameters, string procedureName,
            string columnName)
        {
            try
            {
                string output;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    output = ReadFromDatabase(parameters, conn, procedureName, columnName);
                    conn.Close();
                }
                return output;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return "failed to open the connection :(";
            }
        }

        private static string ReadFromDatabase(List<Tuple<string, string>> parameters, SqlConnection conn,
            string procedure, string columnName)
        {
            try
            {
                SqlCommand command = new SqlCommand(procedure, conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                foreach (var param in parameters)
                {
                    var paramSpec = param.Item1;
                    var paramVal = param.Item2;
                    command.Parameters.Add(new SqlParameter(paramSpec, paramVal));
                }
                return ExecuteReaderCommand(command, columnName);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                return "failed to build command";
            }
        }

        private static string ExecuteReaderCommand(SqlCommand command, string columnName)
        {
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                List<string> list = new List<string>();
                while (reader.Read())
                {
                    list.Add(reader[columnName].ToString());
                }
                return list[0];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "failed from execute reader command";
            }
        }
    }
}