﻿using System;
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
                new SqlParameter("@NoOfBillSharesPaid", user.NoOfBillSharesPaid)
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
                DataTable? dataTable = dbConn.ExecuteReader("GetUserByCNP", parameters, CommandType.StoredProcedure);

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
                    row[14] is DBNull ? 0 : Convert.ToInt32(row[14]) // NoOfBillSharesPaid
                );
            }
            catch (SqlException exception)
            {
                throw new Exception($"Database error: {exception.Message}");
            }
        }
    }
}
