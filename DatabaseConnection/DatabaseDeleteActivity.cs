using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnection
{
    public class DatabaseDeleteActivity
    {
        private string connectionString =
            "Data Source=ASGLH-WL-11919;Initial Catalog=PracticeDatabase;Integrated Security=True";
        
        public void DeleteRowFromTableByNameWithStoredProcedure(List<Tuple<string, string>> parameters, string procedureName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    DeleteFromDatabase(parameters, conn, procedureName);
                    conn.Close();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void DeleteFromDatabase(List<Tuple<string, string>> parameters, SqlConnection conn, string procedure)
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
                ExecuteReaderCommand(command);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void ExecuteReaderCommand(SqlCommand command)
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
