using Microsoft.Data.SqlClient;
using src.Data;
using src.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace src.Repos
{
    public class UserRepository:IUserRepository
    {
        private readonly DatabaseConnection _dbConnection;

        public UserRepository(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public int CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            if (string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName))
            {
                throw new ArgumentException("First and last names cannot be empty");
            }

            User existingUser = GetUserByCnp(user.Cnp);
            if (existingUser != null)
            {
                return existingUser.Id;
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Cnp", user.Cnp),
                new SqlParameter("@FirstName", user.FirstName),
                new SqlParameter("@LastName", user.LastName),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@PhoneNumber", user.PhoneNumber ?? (object)DBNull.Value),
                new SqlParameter("@HashedPassword", user.HashedPassword),
                new SqlParameter("@NumberOfOffenses", user.NumberOfOffenses),
                new SqlParameter("@RiskScore", user.RiskScore),
                new SqlParameter("@Roi", user.ROI),
                new SqlParameter("@CreditScore", user.CreditScore),
                new SqlParameter("@Birthday", user.Birthday.ToString("yyyy-MM-dd")),
                new SqlParameter("@ZodiacSign", user.ZodiacSign),
                new SqlParameter("@ZodiacAttribute", user.ZodiacAttribute),
                new SqlParameter("@NumberOfBillSharesPaid", user.NumberOfBillSharesPaid),
                new SqlParameter("@Income", user.Income),
                new SqlParameter("@Balance", user.Balance)
            };

            const string InsertQuery = @"
                INSERT INTO Users (
                    Cnp, FirstName, LastName, Email, PhoneNumber, 
                    HashedPassword, NumberOfOffenses, RiskScore, Roi, 
                    CreditScore, Birthday, ZodiacSign, ZodiacAttribute, 
                    NumberOfBillSharesPaid, Income, Balance
                )
                VALUES (
                    @Cnp, @FirstName, @LastName, @Email, @PhoneNumber, 
                    @HashedPassword, @NumberOfOffenses, @RiskScore, @Roi, 
                    @CreditScore, @Birthday, @ZodiacSign, @ZodiacAttribute, 
                    @NumberOfBillSharesPaid, @Income, @Balance
                );
                SELECT SCOPE_IDENTITY();";

            try
            {
                int? result = _dbConnection.ExecuteScalar<int>(InsertQuery, parameters, CommandType.Text);
                return result ?? 0;
            }
            catch (SqlException exception)
            {
                throw new Exception("Database error while creating user", exception);
            }
        }

        public User GetUserByCnp(string cnp)
        {
            if (string.IsNullOrWhiteSpace(cnp))
            {
                throw new ArgumentException("CNP cannot be empty", nameof(cnp));
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Cnp", cnp)
            };

            try
            {
                const string SelectQuery = @"
                    SELECT Id, Cnp, FirstName, LastName, Email, PhoneNumber, 
                           HashedPassword, NumberOfOffenses, RiskScore, Roi, 
                           CreditScore, Birthday, ZodiacSign, ZodiacAttribute, 
                           NumberOfBillSharesPaid, Income, Balance 
                    FROM Users 
                    WHERE Cnp = @Cnp";

                DataTable dataTable = _dbConnection.ExecuteReader(SelectQuery, parameters, CommandType.Text);

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    return null;
                }

                DataRow row = dataTable.Rows[0];
                return CreateUserFromDataRow(row);
            }
            catch (SqlException exception)
            {
                throw new Exception("Database error while retrieving user", exception);
            }
        }

        public void PenalizeUser(string cnp, int penaltyAmount)
        {
            if (string.IsNullOrWhiteSpace(cnp))
            {
                throw new ArgumentException("CNP cannot be empty", nameof(cnp));
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Cnp", cnp),
                new SqlParameter("@Amount", penaltyAmount)
            };

            const string UpdateQuery = @"
                UPDATE Users 
                SET CreditScore = CreditScore - @Amount 
                WHERE Cnp = @Cnp";

            try
            {
                int rowsAffected = _dbConnection.ExecuteNonQuery(UpdateQuery, parameters, CommandType.Text);
                if (rowsAffected == 0)
                {
                    throw new Exception($"No user found with CNP: {cnp}");
                }
            }
            catch (SqlException exception)
            {
                throw new Exception("Database error while penalizing user", exception);
            }
        }

        public void IncrementOffensesCount(string cnp)
        {
            if (string.IsNullOrWhiteSpace(cnp))
            {
                throw new ArgumentException("CNP cannot be empty", nameof(cnp));
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Cnp", cnp)
            };

            const string updateQuery = @"
                UPDATE Users 
                SET NumberOfOffenses = ISNULL(NumberOfOffenses, 0) + 1 
                WHERE Cnp = @Cnp";

            try
            {
                int rowsAffected = _dbConnection.ExecuteNonQuery(updateQuery, parameters, CommandType.Text);
                if (rowsAffected == 0)
                {
                    throw new Exception($"No user found with CNP: {cnp}");
                }
            }
            catch (SqlException exception)
            {
                throw new Exception("Database error while incrementing offenses", exception);
            }
        }

        public void UpdateUserCreditScore(string cnp, int creditScore)
        {
            if (string.IsNullOrWhiteSpace(cnp))
            {
                throw new ArgumentException("CNP cannot be empty", nameof(cnp));
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Cnp", cnp),
                new SqlParameter("@CreditScore", creditScore)
            };

            const string UpdateQuery = @"
                UPDATE Users 
                SET CreditScore = @CreditScore 
                WHERE Cnp = @Cnp";

            try
            {
                int rowsAffected = _dbConnection.ExecuteNonQuery(UpdateQuery, parameters, CommandType.Text);
                if (rowsAffected == 0)
                {
                    throw new Exception($"No user found with CNP: {cnp}");
                }
            }
            catch (SqlException exception)
            {
                throw new Exception("Database error while updating credit score", exception);
            }
        }

        public void UpdateUserROI(string cnp, decimal roi)
        {
            if (string.IsNullOrWhiteSpace(cnp))
            {
                throw new ArgumentException("CNP cannot be empty", nameof(cnp));
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Cnp", cnp),
                new SqlParameter("@Roi", roi)
            };

            const string UpdateQuery = @"
                UPDATE Users 
                SET Roi = @Roi 
                WHERE Cnp = @Cnp";

            try
            {
                int rowsAffected = _dbConnection.ExecuteNonQuery(UpdateQuery, parameters, CommandType.Text);
                if (rowsAffected == 0)
                {
                    throw new Exception($"No user found with CNP: {cnp}");
                }
            }
            catch (SqlException exception)
            {
                throw new Exception("Database error while updating ROI", exception);
            }
        }

        public void UpdateUserRiskScore(string cnp, int riskScore)
        {
            if (string.IsNullOrWhiteSpace(cnp))
            {
                throw new ArgumentException("CNP cannot be empty", nameof(cnp));
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Cnp", cnp),
                new SqlParameter("@RiskScore", riskScore)
            };

            const string UpdateQuery = @"
                UPDATE Users 
                SET RiskScore = @RiskScore 
                WHERE Cnp = @Cnp";

            try
            {
                int rowsAffected = _dbConnection.ExecuteNonQuery(UpdateQuery, parameters, CommandType.Text);
                if (rowsAffected == 0)
                {
                    throw new Exception($"No user found with CNP: {cnp}");
                }
            }
            catch (SqlException exception)
            {
                throw new Exception("Database error while updating risk score", exception);
            }
        }

        public List<User> GetUsers()
        {
            try
            {
                const string SelectQuery = @"
                    SELECT Id, Cnp, FirstName, LastName, Email, PhoneNumber, 
                           HashedPassword, NumberOfOffenses, RiskScore, Roi, 
                           CreditScore, Birthday, ZodiacSign, ZodiacAttribute, 
                           NumberOfBillSharesPaid, Income, Balance 
                    FROM Users";

                DataTable dataTable = _dbConnection.ExecuteReader(SelectQuery, null, CommandType.Text);

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    return new List<User>();
                }

                List<User> users = new List<User>();
                foreach (DataRow row in dataTable.Rows)
                {
                    users.Add(CreateUserFromDataRow(row));
                }

                return users;
            }
            catch (SqlException exception)
            {
                throw new Exception("Database error while retrieving users", exception);
            }
        }

        private User CreateUserFromDataRow(DataRow row)
        {
            return new User(
                id: Convert.ToInt32(row["Id"]),
                cnp: row["Cnp"].ToString(),
                firstName: row["FirstName"].ToString(),
                lastName: row["LastName"].ToString(),
                email: row["Email"].ToString(),
                phoneNumber: row["PhoneNumber"] is DBNull ? string.Empty : row["PhoneNumber"].ToString(),
                hashedPassword: row["HashedPassword"].ToString(),
                numberOfOffenses: row["NumberOfOffenses"] is DBNull ? 0 : Convert.ToInt32(row["NumberOfOffenses"]),
                riskScore: row["RiskScore"] is DBNull ? 0 : Convert.ToInt32(row["RiskScore"]),
                roi: row["Roi"] is DBNull ? 0m : Convert.ToDecimal(row["Roi"]),
                creditScore: row["CreditScore"] is DBNull ? 0 : Convert.ToInt32(row["CreditScore"]),
                birthday: row["Birthday"] is DBNull ? default : DateOnly.FromDateTime(Convert.ToDateTime(row["Birthday"])),
                zodiacSign: row["ZodiacSign"].ToString(),
                zodiacAttribute: row["ZodiacAttribute"].ToString(),
                numberOfBillSharesPaid: row["NumberOfBillSharesPaid"] is DBNull ? 0 : Convert.ToInt32(row["NumberOfBillSharesPaid"]),
                income: row["Income"] is DBNull ? 0 : Convert.ToInt32(row["Income"]),
                balance: row["Balance"] is DBNull ? 0m : Convert.ToDecimal(row["Balance"])
            );
        }
    }
}