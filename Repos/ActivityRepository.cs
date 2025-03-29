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
    class ActivityRepository
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

            User? existingUser = userRepository.GetUserByCNP(userCNP);

            if (existingUser == null)
            {
                throw new ArgumentException("User does not exist");
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserCNP", userCNP),
                new SqlParameter("@Name", activityName),
                new SqlParameter("@LastModifiedAmount", amount),
                new SqlParameter("@Details", details)
            };

            try
            {
                int? result = dbConn.ExecuteScalar<int>("GetActivitiesForUser", parameters, CommandType.StoredProcedure);
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


            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserCNP", userCNP)
            };

            try
            {
                DataTable? dataTable = dbConn.ExecuteReader("GetActivitiesForUser", parameters, CommandType.StoredProcedure);

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    throw new Exception("User not found");
                }

                List<ActivityLog> activitiesList = new List<ActivityLog>();

                foreach (DataRow row in dataTable.Rows)
                {
                    activitiesList.Add(new ActivityLog(
                        id: Convert.ToInt32(row["Id"]),
                        userCNP: row["userCNP"].ToString()!,
                        name: row["Name"].ToString()!,
                        amount: Convert.ToInt32(row["LastModifiedAmount"]),
                        details: row["Details"].ToString()!
                        ));
                }
                ;

                return activitiesList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving activity for user", ex);
            }

        }

    }
}
