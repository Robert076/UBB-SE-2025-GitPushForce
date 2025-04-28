using src.Data;
using src.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace src.Repos
{
    class TipsRepository
    {
        private readonly DatabaseConnection dbConn;

        public TipsRepository(DatabaseConnection dbConn)
        {
            this.dbConn = dbConn;
        }

        public void GiveUserTipForLowBracket(string userCNP)
        {
            string query = "SELECT * FROM Tips WHERE CreditScoreBracket = 'Low-credit';";
            DataTable smallCreditTips = dbConn.ExecuteReader(query, null, CommandType.Text);

            Random random = new Random();
            DataRow randomTip = smallCreditTips.Rows[random.Next(smallCreditTips.Rows.Count)];

            Tip selectedTip = new Tip
            {
                Id = Convert.ToInt32(randomTip["ID"]),
                CreditScoreBracket = randomTip["CreditScoreBracket"].ToString(),
                TipText = randomTip["TipText"].ToString()
            };

            SqlParameter[] insertParams = new SqlParameter[]
            {
                    new SqlParameter("@UserCNP", SqlDbType.VarChar, 16) { Value = userCNP },
                    new SqlParameter("@TipID", SqlDbType.Int) { Value = selectedTip.Id }
            };

            string insertQuery = "INSERT INTO GivenTips (UserCNP, TipID, MessageID, Date) VALUES (@UserCNP, @TipID, NULL, GETDATE());";
            dbConn.ExecuteNonQuery(insertQuery, insertParams, CommandType.Text);
        }


        public void GiveUserTipForMediumBracket(string userCNP)
        {
            string query = "SELECT * FROM Tips WHERE CreditScoreBracket = 'Medium-credit';";
            DataTable mediumCreditTips = dbConn.ExecuteReader(query, null, CommandType.Text);

            Random random = new Random();
            DataRow randomTip = mediumCreditTips.Rows[random.Next(mediumCreditTips.Rows.Count)];

            Tip selectedTip = new Tip
            {
                Id = Convert.ToInt32(randomTip["ID"]),
                CreditScoreBracket = randomTip["CreditScoreBracket"].ToString(),
                TipText = randomTip["TipText"].ToString()
            };

            SqlParameter[] insertParams = new SqlParameter[]
            {
                    new SqlParameter("@UserCNP", SqlDbType.VarChar, 16) { Value = userCNP },
                    new SqlParameter("@TipID", SqlDbType.Int) { Value = selectedTip.Id }
            };
            string insertQuery = "INSERT INTO GivenTips (UserCNP, TipID, MessageID, Date) VALUES (@UserCNP, @TipID, NULL, GETDATE());";
            dbConn.ExecuteNonQuery(insertQuery, insertParams, CommandType.Text);

        }
        public void GiveUserTipForHighBracket(string userCNP)
        {
            string query = "SELECT * FROM Tips WHERE CreditScoreBracket = 'High-credit';";
            DataTable highCreditTips = dbConn.ExecuteReader(query, null, CommandType.Text);

            Random random = new Random();
            DataRow randomTip = highCreditTips.Rows[random.Next(highCreditTips.Rows.Count)];

            Tip selectedTip = new Tip
            {
                Id = Convert.ToInt32(randomTip["ID"]),
                CreditScoreBracket = randomTip["CreditScoreBracket"].ToString(),
                TipText = randomTip["TipText"].ToString()
            };

            SqlParameter[] insertParams = new SqlParameter[]
            {
                    new SqlParameter("@UserCNP", SqlDbType.VarChar, 16) { Value = userCNP },
                    new SqlParameter("@TipID", SqlDbType.Int) { Value = selectedTip.Id }
            };
            string insertQuery = "INSERT INTO GivenTips (UserCNP, TipID, MessageID, Date) VALUES (@UserCNP, @TipID, NULL, GETDATE());";
            dbConn.ExecuteNonQuery(insertQuery, insertParams, CommandType.Text);
        }
    }
}
