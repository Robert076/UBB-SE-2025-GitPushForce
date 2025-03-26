using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using src.Data;
using src.Model;

namespace src.Repos
{
    public class UserRepository
    {
        private readonly DatabaseConnection dbConn;

        public UserRepository(DatabaseConnection dbConn)
        {
            this.dbConn = dbConn;
        }

        public int CreateUser(User user)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }
            if(string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName))
            {
                throw new ArgumentException("First and last names cannot be empty");
            }
            var existingUser = GetUserByCNP(user.CNP);
            if(existingUser != null)
            {
                return existingUser.Id;
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CNP", user.CNP),
            };
            try
            {
                int? result = dbConn.ExecuteScalar<int>("CreateUser", parameters, CommandType.StoredProcedure);
                return result ?? 0;
            }
            catch (SqlException exception)
            {
                throw new Exception($"Database error: {exception.Message}");
            }
        }

        public User GetUserByCNP(string CNP)
        {
            if (string.IsNullOrWhiteSpace(CNP))
            {
                throw new ArgumentException("Invalid CNP");
            }
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CNP", CNP)
            };
            DataTable? dataTable = null;
            try
            {
                dataTable = dbConn.ExecuteReader("GetUserByCNP", parameters, CommandType.StoredProcedure);
                if(dataTable.Rows.Count == 0)
                {
                    throw new Exception("User not found");
                }
                var row = dataTable.Rows[0];
                return new User(
                    Convert.ToInt32(row[0]),            // Id
                    row[1]?.ToString() ?? string.Empty, // CNP
                    row[2]?.ToString() ?? string.Empty, // FirstName
                    row[3]?.ToString() ?? string.Empty, // LastName
                    row[4]?.ToString() ?? string.Empty, // Email
                    row[5]?.ToString() ?? string.Empty, // PhoneNumber
                    row[6]?.ToString() ?? string.Empty, // HashedPassword
                    Convert.ToInt32(row[7]),            // NoOffenses
                    Convert.ToInt32(row[8]),            // RiskScore
                    Convert.ToDecimal(row[9]),          // ROI
                    Convert.ToInt32(row[10]),           // CreditScore
                    DateOnly.FromDateTime(Convert.ToDateTime(row[11])), // Birthday (Fix for DateOnly)
                    row[12]?.ToString() ?? string.Empty, // ZodiacSign
                    row[13]?.ToString() ?? string.Empty, // ZodiacAttribute
                    Convert.ToInt32(row[14])  // NoOfBillSharesPaid
                );           
            }
            catch (SqlException exception)
            {
                throw new Exception($"Database error: {exception.Message}");
            }
        }
    }
}
