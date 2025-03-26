using Microsoft.Data.SqlClient;
using System;
using System.Data;


namespace src.Data
{
    public class DatabaseConnection
    {
        private SqlConnection sqlConnection;
        private readonly string connectionString;

        public DatabaseConnection()
        {
            connectionString = "Server=localhost\\SQLEXPRESS;Database=GitPushForce;Trusted_Connection=True;TrustServerCertificate=True;";
            try
            {
                sqlConnection = new SqlConnection(connectionString);
            } 
            catch(Exception exception)
            {
                throw new Exception($"Error initializing SQL Connection {exception.Message}");
            }
        }

        public void OpenConnection()
        {
            if(sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if(sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }
        }


    }
}
