using src.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Model;
using System.Data;
using Microsoft.Data.SqlClient;

namespace src.Repos
{
    public class ChatReportRepository
    {
        private readonly DatabaseConnection dbConn;

        public ChatReportRepository(DatabaseConnection dbConn)
        {
            this.dbConn = dbConn;
        }

        public List<ChatReport> GetChatReports()
        {
            try
            {
                string query = "SELECT Id, ReportedUserCNP, ReportedMessage FROM ChatReports";

                DataTable? dataTable = dbConn.ExecuteReader(query, null, CommandType.Text);

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    throw new Exception("Chat reports table is empty");
                }

                List<ChatReport> chatReports = new List<ChatReport>();

                foreach (DataRow row in dataTable.Rows)
                {
                    ChatReport chatReport = new ChatReport
                    {
                        Id = row["Id"] != DBNull.Value ? Convert.ToInt32(row["Id"]) : 0,
                        ReportedUserCNP = row["ReportedUserCNP"]?.ToString() ?? "",
                        ReportedMessage = row["ReportedMessage"]?.ToString() ?? ""
                    };

                    chatReports.Add(chatReport);
                }

                return chatReports;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving chat reports", ex);
            }
        }


        public void DeleteChatReport(int id)
        {
            try
            {
                string query = "DELETE FROM ChatReports WHERE ID = @ChatReportId";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@ChatReportId", SqlDbType.Int) { Value = id }
                };

                int rowsAffected = dbConn.ExecuteNonQuery(query, parameters, CommandType.Text);

                if (rowsAffected == 0)
                {
                    throw new Exception($"No chat report found with Id {id}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting chat report: {ex.Message}", ex);
            }
        }


        public void UpdateHistoryForUser(string UserCNP, int NewScore)
        {
            try
            {
                string query = @"
            IF EXISTS (SELECT 1 FROM CreditScoreHistory WHERE UserCNP = @UserCNP AND Date = CAST(GETDATE() AS DATE))
            BEGIN
                UPDATE CreditScoreHistory
                SET Score = @NewScore
                WHERE UserCNP = @UserCNP AND Date = CAST(GETDATE() AS DATE);
            END
            ELSE
            BEGIN
                INSERT INTO CreditScoreHistory (UserCNP, Date, Score)
                VALUES (@UserCNP, CAST(GETDATE() AS DATE), @NewScore);
            END";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@UserCNP", SqlDbType.VarChar, 16) { Value = UserCNP },
            new SqlParameter("@NewScore", SqlDbType.Int) { Value = NewScore }
                };

                int rowsAffected = dbConn.ExecuteNonQuery(query, parameters, CommandType.Text);

                if (rowsAffected == 0)
                {
                    throw new Exception("No changes were made to the CreditScoreHistory.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating credit score history: {ex.Message}", ex);
            }
        }

        

    }
}
