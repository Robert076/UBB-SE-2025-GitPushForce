using src.Data;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using src.Model;
using Windows.ApplicationModel.Contacts;

namespace src.Repos
{
    public class ChatReportRepository: IChatReportRepository
    {
        private readonly DatabaseConnection _dbConnection;

        public ChatReportRepository(DatabaseConnection dbConnection)
        {
            this._dbConnection = dbConnection;
        }

        public int GetNumberOfGivenTipsForUser(string reportedUserCnp)
        {
            SqlParameter[] tipsParameters = new SqlParameter[]
            {
                 new SqlParameter("@UserCnp", reportedUserCnp)
            };
            const string GetQuery = "SET NOCOUNT ON; SELECT COUNT(*) AS NumberOfTips FROM GivenTips WHERE UserCnp = @UserCnp;";
            int countTips = _dbConnection.ExecuteScalar<int>(GetQuery, tipsParameters, CommandType.Text);
            return countTips;
        }

        public void UpdateActivityLog(string reportedUserCnp, int amount)
        {
            SqlParameter[] activityParameters = new SqlParameter[]
            {
                new SqlParameter("@UserCnp", reportedUserCnp),
                new SqlParameter("@ActivityName", "Chat"),
                new SqlParameter("@LastModifiedAmount", amount),
                new SqlParameter("@ActivityDetails", "Chat abuse")
            };
            const string UpdateQuery = "DECLARE @count INT; SELECT @count = COUNT(*) FROM ActivityLog a WHERE a.UserCnp = @UserCnp and a.ActivityName = @ActivityName; IF @count = 0 BEGIN INSERT INTO ActivityLog (ActivityName, UserCnp, LastModifiedAmount, ActivityDetails) VALUES (@ActivityName, @UserCnp, @LastModifiedAmount, @ActivityDetails); END ELSE BEGIN UPDATE ActivityLog SET LastModifiedAmount = @LastModifiedAmount, ActivityDetails = @ActivityDetails WHERE UserCnp = @UserCnp AND ActivityName = @ActivityName; END;";
            _dbConnection.ExecuteNonQuery(UpdateQuery, activityParameters, CommandType.Text);
        }

        public List<ChatReport> GetChatReports()
        {
            try
            {
                const string SelectQuery = @"
                    SELECT Id, ReportedUserCnp, ReportedMessage 
                    FROM ChatReports";

                DataTable? chatReportsDataTable = _dbConnection.ExecuteReader(SelectQuery, null, CommandType.Text);

                if (chatReportsDataTable == null)
                {
                    return new List<ChatReport>();
                }

                List<ChatReport> chatReports = new List<ChatReport>();

                foreach (DataRow row in chatReportsDataTable.Rows)
                {
                    chatReports.Add(new ChatReport
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        ReportedUserCnp = row["ReportedUserCnp"].ToString() ?? string.Empty,
                        ReportedMessage = row["ReportedMessage"].ToString() ?? string.Empty,
                    });
                }

                return chatReports;
            }
            catch (Exception exception)
            {
                throw new Exception("Error retrieving chat reports", exception);
            }
        }

        public void DeleteChatReport(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid report ID");
            }

            try
            {
                const string DeleteQuery = "DELETE FROM ChatReports WHERE Id = @Id";

                SqlParameter[] deleteParameters = new SqlParameter[]
                {
                    new SqlParameter("@Id", id)
                };

                int rowsAffected = _dbConnection.ExecuteNonQuery(DeleteQuery, deleteParameters, CommandType.Text);

                if (rowsAffected == 0)
                {
                    throw new Exception($"No chat report found with Id {id}");
                }
            }
            catch (Exception exception)
            {
                throw new Exception($"Error deleting chat report: {exception.Message}", exception);
            }
        }

        public void UpdateScoreHistoryForUser(string UserCnp, int NewScore)
        {
            try
            {
                const string UpdateScoreHistoryQuery = @"
            IF EXISTS (SELECT 1 FROM CreditScoreHistory WHERE UserCNP = @UserCnp AND Date = CAST(GETDATE() AS DATE))
            BEGIN
                UPDATE CreditScoreHistory
                SET Score = @NewScore
                WHERE UserCnp = @UserCnp AND Date = CAST(GETDATE() AS DATE);
            END
            ELSE
            BEGIN
                INSERT INTO CreditScoreHistory (UserCnp, Date, Score)
                VALUES (@UserCnp, CAST(GETDATE() AS DATE), @NewScore);
            END";

                SqlParameter[] scoreHistoryParameters = new SqlParameter[]
                {
            new SqlParameter("@UserCnp", SqlDbType.VarChar, 16) { Value = UserCnp },
            new SqlParameter("@NewScore", SqlDbType.Int) { Value = NewScore }
                };

                int rowsAffected = _dbConnection.ExecuteNonQuery(UpdateScoreHistoryQuery, scoreHistoryParameters, CommandType.Text);

                if (rowsAffected == 0)
                {
                    throw new Exception("No changes were made to the CreditScoreHistory.");
                }
            }
            catch (Exception exception)
            {
                throw new Exception($"Error updating credit score history: {exception.Message}", exception);
            }
        }
    }
}