using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Windows.Security.Authentication.OnlineId;

namespace src.Repositories
{
    public class UserRepository
    {
        public String GetUserName(int Id)
        {
            using(SqlConnection connection= new SqlConnection())
            {
                connection.ConnectionString = "Server=localhost\\SQLEXPRESS;Database=GitPushForce;Trusted_Connection=True;TrustServerCertificate=True;";
                connection.Open();

                SqlCommand cmd = new SqlCommand("Select name from Users", connection);
                cmd.Parameters.Add(new SqlParameter("Id", Id));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        return (string)reader["Name"];
                    }
              
                }
                return "";
            }
        }
    }
}
