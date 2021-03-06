﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseConnection
{
    public class DatabaseReadActivity
    {
        private string connectionString =
            "Data Source=ASGLH-WL-11919;Initial Catalog=PracticeDatabase;Integrated Security=True";

        public List<string> GetRowFromTableByNameWithProcedure(List<Tuple<string, string>> parameters, string procedureName,
            string columnName)
        {
            List<string> output = new List<string>();
            try
            {
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
                output.Add("failed to open the connection");
                return output;
            }
        }

        private static List<string> ReadFromDatabase(List<Tuple<string, string>> parameters, SqlConnection conn,
            string procedure, string columnName)
        {
            List<string> output = new List<string>();
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
                output.Add("failed to build command");
                return output;
            }
        }

        private static List<string> ExecuteReaderCommand(SqlCommand command, string columnName)
        {
            List<string> list = new List<string>();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    for (int row = 0; row < reader.FieldCount; row++)
                    {
                        list.Add(String.IsNullOrEmpty(columnName)
                            ? reader[row].ToString()
                            : reader[columnName].ToString());
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                list.Add("failed to execute from reader command");
                return list;
            }
        }
    }
}