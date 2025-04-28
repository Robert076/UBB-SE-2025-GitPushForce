using System;
using System.Collections.Generic;
using System.Data;
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
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }
            if (string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName))
            {
                throw new ArgumentException("First and last names cannot be empty");
            }

            User? existingUser = GetUserByCNP(user.CNP);

            if (existingUser != null)
            {
                return existingUser.Id;
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CNP", user.CNP),
                new SqlParameter("@FirstName", user.FirstName),
                new SqlParameter("@LastName", user.LastName),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@PhoneNumber", user.PhoneNumber ?? (object)DBNull.Value),
                new SqlParameter("@HashedPassword", user.HashedPassword),
                new SqlParameter("@NoOffenses", user.NoOffenses),
                new SqlParameter("@RiskScore", user.RiskScore),
                new SqlParameter("@ROI", user.ROI),
                new SqlParameter("@CreditScore", user.CreditScore),
                new SqlParameter("@Birthday", user.Birthday.ToString("yyyy-MM-dd")),
                new SqlParameter("@ZodiacSign", user.ZodiacSign),
                new SqlParameter("@ZodiacAttribute", user.ZodiacAttribute),
                new SqlParameter("@NoOfBillSharesPaid", user.NoOfBillSharesPaid),
                new SqlParameter("@Income", user.Income),
                new SqlParameter("@Balance", user.Balance)
            };

            string query = @"
                INSERT INTO Users (
                    CNP,
                    FirstName,
                    LastName,
                    Email,
                    PhoneNumber,
                    HashedPassword,
                    NoOffenses,
                    RiskScore,
                    ROI,
                    CreditScore,
                    Birthday,
                    ZodiacSign,
                    ZodiacAttribute,
                    NoOfBillSharesPaid,
                    Income,
                    Balance
                )
                VALUES (
                    @CNP,
                    @FirstName,
                    @LastName,
                    @Email,
                    @PhoneNumber,
                    @HashedPassword,
                    @NoOffenses,
                    @RiskScore,
                    @ROI,
                    @CreditScore,
                    @Birthday,
                    @ZodiacSign,
                    @ZodiacAttribute,
                    @NoOfBillSharesPaid,
                    @Income,
                    @Balance
                );

                SELECT SCOPE_IDENTITY();";

            try
            {
                int? result = dbConn.ExecuteScalar<int>(query, parameters, CommandType.Text);
                return result ?? 0;
            }
            catch (SqlException exception)
            {
                throw new Exception($"Database error: {exception.Message}");
            }
        }

        public User? GetUserByCNP(string CNP)
        {
            if (string.IsNullOrWhiteSpace(CNP))
            {
                throw new ArgumentException("Invalid CNP");
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserCNP", CNP)
            };

            try
            {
                string query = "SELECT * FROM Users WHERE CNP = @UserCNP";
                DataTable? dataTable = dbConn.ExecuteReader(query, parameters, CommandType.Text);

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    throw new Exception("User not found");
                }

                var row = dataTable.Rows[0];

                return new User(
                    Convert.ToInt32(row[0]),                      // Id
                    row[1]?.ToString() ?? string.Empty,          // CNP
                    row[2]?.ToString() ?? string.Empty,          // FirstName
                    row[3]?.ToString() ?? string.Empty,          // LastName
                    row[4]?.ToString() ?? string.Empty,          // Email
                    row[5] is DBNull ? string.Empty : row[5].ToString(), // PhoneNumber
                    row[6]?.ToString() ?? string.Empty,          // HashedPassword
                    row[7] is DBNull ? 0 : Convert.ToInt32(row[7]),   // NoOffenses
                    row[8] is DBNull ? 0 : Convert.ToInt32(row[8]),   // RiskScore
                    row[9] is DBNull ? 0m : Convert.ToDecimal(row[9]), // ROI
                    row[10] is DBNull ? 0 : Convert.ToInt32(row[10]), // CreditScore
                    row[11] is DBNull ? default : DateOnly.FromDateTime(Convert.ToDateTime(row[11])), // Birthday
                    row[12]?.ToString() ?? string.Empty,         // ZodiacSign
                    row[13]?.ToString() ?? string.Empty,         // ZodiacAttribute
                    row[14] is DBNull ? 0 : Convert.ToInt32(row[14]), // NoOfBillSharesPaid
                    row[15] is DBNull ? 0 : Convert.ToInt32(row[15]), // Income
                    row[16] is DBNull ? 0m : Convert.ToDecimal(row[16]) // Balance
                );
            }
            catch (SqlException exception)
            {
                throw new Exception($"Database error: {exception.Message}");
            }
        }

        public void PenalizeUser(string CNP, Int32 amountToBePenalizedWith)
        {
            if (CNP == null)
            {
                throw new ArgumentNullException("PenalizeUser: UserCNP is null");
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CNP", CNP),
                new SqlParameter("@Amount", amountToBePenalizedWith)
            };

            string query = "UPDATE Users SET CreditScore = CreditScore - @Amount WHERE CNP = @CNP";
            dbConn.ExecuteNonQuery(query, parameters, CommandType.Text);

        }

        public void IncrementOffenesesCountByOne(string CNP)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserCNP", CNP),
            };
            string query = "UPDATE Users SET NoOffenses = ISNULL(NoOffenses, 0) + 1 WHERE CNP = @UserCNP";
            dbConn.ExecuteNonQuery(query, parameters, CommandType.Text);
        }

        public void UpdateUserCreditScore(string CNP, int creditScore)
        {
            if (string.IsNullOrWhiteSpace(CNP))
            {
                throw new ArgumentException("CNP-ul este invalid", nameof(CNP));
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserCNP", CNP),
                new SqlParameter("@NewCreditScore", creditScore)
            };

            try
            {
                string query = "UPDATE Users SET CreditScore = @NewCreditScore WHERE CNP = @UserCNP;";
                dbConn.ExecuteNonQuery(query, parameters, CommandType.Text);
            }
            catch (SqlException exception)
            {
                throw new Exception($"Eroare la baza de date: {exception.Message}");
            }
        }

        public void UpdateUserROI(string CNP, decimal ROI)
        {
            if (string.IsNullOrWhiteSpace(CNP))
            {
                throw new ArgumentException("CNP-ul este invalid", nameof(CNP));
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserCNP", CNP),
                new SqlParameter("@NewROI", ROI)
            };

            try
            {
                string query = "UPDATE Users SET ROI = @NewROI WHERE CNP = @UserCNP;";
                dbConn.ExecuteNonQuery(query, parameters, CommandType.Text);
            }
            catch (SqlException exception)
            {
                throw new Exception($"Eroare la baza de date: {exception.Message}");
            }
        }

        public void UpdateUserRiskScore(string CNP, int riskScore)
        {
            if (string.IsNullOrWhiteSpace(CNP))
            {
                throw new ArgumentException("CNP-ul este invalid", nameof(CNP));
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserCNP", CNP),
                new SqlParameter("@NewRiskScore", riskScore)
            };

            try
            {
                string query = "UPDATE Users SET RiskScore = @NewRiskScore WHERE CNP = @UserCNP;";
                dbConn.ExecuteNonQuery(query, parameters, CommandType.Text);
            }
            catch (SqlException exception)
            {
                throw new Exception($"Eroare la baza de date: {exception.Message}");
            }
        }

        public List<User> GetUsers()
        {
            try
            {
                DataTable? dataTable = dbConn.ExecuteReader("SELECT * FROM Users;", null, CommandType.Text);


                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    throw new Exception("Users not found");
                }

                List<User> users = new List<User>();

                foreach (DataRow row in dataTable.Rows)
                {
                    users.Add(new User(
                        Convert.ToInt32(row[0]),                      // Id
                        row[1]?.ToString() ?? string.Empty,          // CNP
                        row[2]?.ToString() ?? string.Empty,          // FirstName
                        row[3]?.ToString() ?? string.Empty,          // LastName
                        row[4]?.ToString() ?? string.Empty,          // Email
                        row[5] is DBNull ? string.Empty : row[5].ToString(), // PhoneNumber
                        row[6]?.ToString() ?? string.Empty,          // HashedPassword
                        row[7] is DBNull ? 0 : Convert.ToInt32(row[7]),   // NoOffenses
                        row[8] is DBNull ? 0 : Convert.ToInt32(row[8]),   // RiskScore
                        row[9] is DBNull ? 0m : Convert.ToDecimal(row[9]), // ROI
                        row[10] is DBNull ? 0 : Convert.ToInt32(row[10]), // CreditScore
                        row[11] is DBNull ? default : DateOnly.FromDateTime(Convert.ToDateTime(row[11])), // Birthday
                        row[12]?.ToString() ?? string.Empty,         // ZodiacSign
                        row[13]?.ToString() ?? string.Empty,         // ZodiacAttribute
                        row[14] is DBNull ? 0 : Convert.ToInt32(row[14]),
                        row[15] is DBNull ? 0 : Convert.ToInt32(row[15]),  // Income
                        row[16] is DBNull ? 0 : Convert.ToDecimal(row[16]) // Balance
                        ));
                }

                return users;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving history for user", ex);
            }

        }
    }
}
 
