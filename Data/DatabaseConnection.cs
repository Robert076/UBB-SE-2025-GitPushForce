using Microsoft.Data.SqlClient;
using System;


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
    }
}
