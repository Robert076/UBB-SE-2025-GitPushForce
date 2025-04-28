using src.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace src.Repos
{
    public class ActivityRepository
    {

        private readonly DatabaseConnection dbConn;
        private readonly UserRepository userRepository;

        public ActivityRepository(DatabaseConnection dbConn, UserRepository userRepository)
        {
            this.dbConn = dbConn;
            this.userRepository = userRepository;
        }
        public void AddActivity(string userCNP, string activityName, int amount, string details)
        {
            if (string.IsNullOrWhiteSpace(userCNP) || string.IsNullOrWhiteSpace(activityName) || amount <= 0)
            {
                throw new ArgumentException("User CNP, activity name and amount cannot be empty or less than 0");
            }

            User? existingUser;

            try
            {
                existingUser = userRepository.GetUserByCNP(userCNP);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving user", ex);
            }
            string sqlInsert = @"
                            INSERT INTO ActivityLog (UserCNP, Name, LastModifiedAmount, Details)
                                        VALUES (@UserCNP, @Name, @LastModifiedAmount, @Details);
                                ";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserCNP", userCNP),
                new SqlParameter("@Name", activityName),
                new SqlParameter("@LastModifiedAmount", amount),
                new SqlParameter("@Details", details ?? (object)DBNull.Value)
            };

            try
            {
                int rowsAffected = dbConn.ExecuteNonQuery(sqlInsert, parameters, CommandType.Text);
            }
            catch (SqlException exception)
            {
                throw new Exception($"Database error: {exception.Message}");
            }





        }


        public List<ActivityLog> GetActivityForUser(string userCNP)
        {
            if (string.IsNullOrWhiteSpace(userCNP))
            {
                throw new ArgumentException("User CNP cannot be empty");
            }

            string sqlQuery = "SELECT * FROM ActivityLog WHERE UserCNP = @UserCNP";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@UserCNP", userCNP)
            };

            try
            {
                DataTable? dataTable = dbConn.ExecuteReader(sqlQuery, parameters, CommandType.Text);

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    throw new Exception("No activities found for user");
                }

                List<ActivityLog> activitiesList = new List<ActivityLog>();

                foreach (DataRow row in dataTable.Rows)
                {
                    activitiesList.Add(new ActivityLog(
                        id: Convert.ToInt32(row["Id"]),
                        userCNP: row["UserCNP"].ToString()!,
                        name: row["Name"].ToString()!,
                        amount: Convert.ToInt32(row["LastModifiedAmount"]),
                        details: row["Details"].ToString()!
                    ));
                }

                return activitiesList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving activity for user", ex);
            }
        }


    }
}
