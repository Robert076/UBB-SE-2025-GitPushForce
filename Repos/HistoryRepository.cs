using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Src.Data;
using Src.Model;

namespace Src.Repos
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly DatabaseConnection dbConnection;

        public HistoryRepository(DatabaseConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public List<CreditScoreHistory> GetHistoryForUser(string userCnp)
        {
            if (string.IsNullOrWhiteSpace(userCnp))
            {
                throw new ArgumentException("User CNP cannot be empty");
            }

            try
            {
                const string SelectQuery = @"
                    SELECT Id, UserCnp, Date, Score 
                    FROM CreditScoreHistory 
                    WHERE UserCnp = @UserCnp
                    ORDER BY Date DESC";

                SqlParameter[] selectParameters = new SqlParameter[]
                {
                    new SqlParameter("@UserCnp", userCnp)
                };

                DataTable? creditScoreDataTable = dbConnection.ExecuteReader(SelectQuery, selectParameters, CommandType.Text);

                if (creditScoreDataTable == null)
                {
                    return new List<CreditScoreHistory>();
                }

                List<CreditScoreHistory> historyList = new List<CreditScoreHistory>();

                foreach (DataRow row in creditScoreDataTable.Rows)
                {
                    historyList.Add(new CreditScoreHistory(
                        id: Convert.ToInt32(row["Id"]),
                        userCnp: row["UserCnp"].ToString() !,
                        date: DateOnly.FromDateTime(Convert.ToDateTime(row["Date"])),
                        creditScore: Convert.ToInt32(row["Score"])));
                }

                return historyList;
            }
            catch (Exception exception)
            {
                throw new Exception("Error retrieving credit score history", exception);
            }
        }
    }
}