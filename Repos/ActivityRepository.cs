using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Src.Data;
using Src.Model;

namespace Src.Repos
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly DatabaseConnection dbConnection;
        private readonly UserRepository userRepository;

        public ActivityRepository(DatabaseConnection dbConnection, UserRepository userRepository)
        {
            this.dbConnection = dbConnection;
            this.userRepository = userRepository;
        }

        public void AddActivity(string userCnp, string activityName, int amount, string details)
        {
            if (string.IsNullOrWhiteSpace(userCnp) || string.IsNullOrWhiteSpace(activityName) || amount <= 0)
            {
                throw new ArgumentException("User CNP, activity name and amount cannot be empty or less than 0");
            }

            try
            {
                User? existingUser = userRepository.GetUserByCnp(userCnp);
                if (existingUser == null)
                {
                    throw new ArgumentException("User not found");
                }
            }
            catch (ArgumentException exception)
            {
                throw new ArgumentException("Invalid user CNP", exception);
            }
            catch (Exception exception)
            {
                throw new Exception("Error retrieving user", exception);
            }

            const string InsertQuery = @"
                INSERT INTO ActivityLog (UserCnp, ActivityName, LastModifiedAmount, ActivityDetails)
                VALUES (@UserCnp, @ActivityName, @LastModifiedAmount, @ActivityDetails)";

            SqlParameter[] activityParameters = new SqlParameter[]
            {
                new SqlParameter("@UserCnp", userCnp),
                new SqlParameter("@ActivityName", activityName),
                new SqlParameter("@LastModifiedAmount", amount),
                new SqlParameter("@ActivityDetails", details ?? (object)DBNull.Value)
            };

            try
            {
                int rowsAffected = dbConnection.ExecuteNonQuery(InsertQuery, activityParameters, CommandType.Text);
                if (rowsAffected == 0)
                {
                    throw new Exception("No rows were inserted");
                }
            }
            catch (SqlException exception)
            {
                throw new Exception($"Database error: {exception.Message}", exception);
            }
        }

        public List<ActivityLog> GetActivityForUser(string userCnp)
        {
            if (string.IsNullOrWhiteSpace(userCnp))
            {
                throw new ArgumentException("User CNP cannot be empty");
            }

            const string SelectQuery = @"
                SELECT Id, UserCnp, ActivityName, LastModifiedAmount, ActivityDetails 
                FROM ActivityLog 
                WHERE UserCnp = @UserCnp";

            SqlParameter[] selectQueryParameter = new SqlParameter[]
            {
                new SqlParameter("@UserCnp", userCnp)
            };

            try
            {
                DataTable? userActivityTable = dbConnection.ExecuteReader(SelectQuery, selectQueryParameter, CommandType.Text);

                if (userActivityTable == null || userActivityTable.Rows.Count == 0)
                {
                    return new List<ActivityLog>();
                }

                List<ActivityLog> activitiesList = new List<ActivityLog>();

                foreach (DataRow row in userActivityTable.Rows)
                {
                    activitiesList.Add(new ActivityLog(
                        id: Convert.ToInt32(row["Id"]),
                        userCNP: row["UserCnp"].ToString() !,
                        name: row["ActivityName"].ToString() !,
                        amount: Convert.ToInt32(row["LastModifiedAmount"]),
                        details: row["ActivityDetails"].ToString() ?? string.Empty));
                }

                return activitiesList;
            }
            catch (Exception exception)
            {
                throw new Exception("Error retrieving activity for user", exception);
            }
        }
    }
}