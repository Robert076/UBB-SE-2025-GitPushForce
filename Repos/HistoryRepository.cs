using src.Data;
using src.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@UserCNP", userCNP)
            };

            
        }
    }
}
