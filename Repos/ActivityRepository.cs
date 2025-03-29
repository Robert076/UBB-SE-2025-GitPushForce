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



    }
}
