using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Src.Data
{
    public class DatabaseConnection
    {
        private SqlConnection sqlConnection;
        private readonly string connectionString;

        public DatabaseConnection()
        {
            connectionString = "Server=JAMAL;Database=GitPushForce;Trusted_Connection=True;TrustServerCertificate=True;";

            try
            {
                sqlConnection = new SqlConnection(connectionString);
            }
            catch (Exception exception)
            {
                throw new Exception($"Error initializing SQL Connection {exception.Message}");
            }
        }

        public void OpenConnection()
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }
        }
        // TODO
        public T? ExecuteScalar<T>(string query, SqlParameter[] sqlParameters, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.CommandType = commandType;

                    if (sqlParameters != null)
                    {
                        command.Parameters.AddRange(sqlParameters);
                    }

                    var result = command.ExecuteScalar();
                    if (result == DBNull.Value || result == null)
                    {
                        return default;
                    }

                    return (T)Convert.ChangeType(result, typeof(T));
                }
            }
            catch (Exception exception)
            {
                throw new Exception($"Error - ExecutingScalar: {exception.Message}");
            }
            finally
            {
                CloseConnection();
            }
        }

        public DataTable ExecuteReader(string query, SqlParameter[] sqlParameters, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.CommandType = commandType;

                    if (sqlParameters != null)
                    {
                        command.Parameters.AddRange(sqlParameters);
                    }

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        return dataTable;
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception($"Error - ExecuteReader: {exception.Message}");
            }
            finally
            {
                CloseConnection();
            }
        }

        public int ExecuteNonQuery(string query, SqlParameter[] sqlParameters, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                OpenConnection();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.CommandType = commandType;

                    if (sqlParameters != null)
                    {
                        sqlCommand.Parameters.AddRange(sqlParameters);
                    }

                    return sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception($"Exception - ExecuteNonQuery: {exception.Message}");
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
