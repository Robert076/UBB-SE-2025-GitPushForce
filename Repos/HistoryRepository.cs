using src.Data;
using src.Model;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Repos
{
    public class HistoryRepository
    {

        private readonly DatabaseConnection dbConn;

        public HistoryRepository(DatabaseConnection dbConn)
        {
            this.dbConn = dbConn;
        }

        public List<HistoryCreditScore> GetHistoryForUser(string userCNP)
        {
            if (string.IsNullOrWhiteSpace(userCNP))
            {
                throw new ArgumentException("Invalid CNP");
            }

            SqlParameter[] parameters = new SqlParameter[]
             {
                new SqlParameter("@UserCNP", userCNP)
             };

            try
            {
                DataTable? dataTable = dbConn.ExecuteReader("GetHistoryForUser", parameters, CommandType.StoredProcedure);

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    throw new Exception("User not found");
                }

                List<HistoryCreditScore> historyList = new List<HistoryCreditScore>();

                foreach (DataRow row in dataTable.Rows)
                {
                    historyList.Add(new HistoryCreditScore(
                        id: Convert.ToInt32(row["Id"]),
                        userCNP: row["userCNP"].ToString()!,
                        date: DateOnly.FromDateTime(((DateTime)row["Date"])),
                        creditScore: Convert.ToInt32(row["Score"])
                        ));
                }

                return historyList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving history for user", ex);
            }

        }
    }
}
