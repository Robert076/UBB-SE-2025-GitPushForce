using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Src.Data;
using Src.Model;

namespace Src.Repos
{
    public class TipsRepository : ITipsRepository
    {
        private readonly DatabaseConnection dbConnection;

        public TipsRepository(DatabaseConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public void GiveUserTipForLowBracket(string userCnp)
        {
            GiveUserTipByBracket(userCnp, "Low-credit");
        }

        public void GiveUserTipForMediumBracket(string userCnp)
        {
            GiveUserTipByBracket(userCnp, "Medium-credit");
        }

        public void GiveUserTipForHighBracket(string userCnp)
        {
            GiveUserTipByBracket(userCnp, "High-credit");
        }

        private void GiveUserTipByBracket(string userCnp, string creditScoreBracket)
        {
            if (string.IsNullOrWhiteSpace(userCnp))
            {
                throw new ArgumentException("User CNP cannot be empty", nameof(userCnp));
            }

            if (string.IsNullOrWhiteSpace(creditScoreBracket))
            {
                throw new ArgumentException("Credit score bracket cannot be empty", nameof(creditScoreBracket));
            }

            try
            {
                const string SelectQuery = @"
                    SELECT TOP 1 Id, CreditScoreBracket, TipText 
                    FROM Tips 
                    WHERE CreditScoreBracket = @CreditScoreBracket
                    ORDER BY NEWID()";

                SqlParameter[] selectParameters = new SqlParameter[]
                {
                    new SqlParameter("@CreditScoreBracket", creditScoreBracket)
                };

                DataTable tipsTable = dbConnection.ExecuteReader(SelectQuery, selectParameters, CommandType.Text);

                if (tipsTable == null || tipsTable.Rows.Count == 0)
                {
                    throw new Exception($"No tips found for {creditScoreBracket} bracket");
                }

                DataRow tipRow = tipsTable.Rows[0];
                var tip = new Tip
                {
                    Id = Convert.ToInt32(tipRow["Id"]),
                    CreditScoreBracket = tipRow["CreditScoreBracket"].ToString(),
                    TipText = tipRow["TipText"].ToString()
                };

                SqlParameter[] insertParameters = new SqlParameter[]
                {
                    new SqlParameter("@UserCnp", userCnp),
                    new SqlParameter("@TipId", tip.Id)
                };

                const string InsertQuery = @"
                    INSERT INTO GivenTips 
                        (UserCnp, TipId, MessageId, Date) 
                    VALUES 
                        (@UserCnp, @TipId, NULL, GETDATE())";

                int rowsAffected = dbConnection.ExecuteNonQuery(InsertQuery, insertParameters, CommandType.Text);

                if (rowsAffected == 0)
                {
                    throw new Exception("Failed to record tip for user");
                }
            }
            catch (Exception exception)
            {
                throw new Exception($"Error giving user {creditScoreBracket} tip", exception);
            }
        }

        public List<Tip> GetTipsForGivenUser(string userCnp)
        {
            SqlParameter[] tipsParameters = new SqlParameter[]
            {
                new SqlParameter("@UserCnp", userCnp)
            };
            const string GetQuery = "SELECT T.ID, T.CreditScoreBracket, T.TipText, GT.Date FROM GivenTips GT INNER JOIN Tips T ON GT.TipID = T.ID WHERE GT.UserCnp = @UserCnp;";
            DataTable tipsRows = dbConnection.ExecuteReader(GetQuery, tipsParameters, CommandType.Text);
            List<Tip> tips = new List<Tip>();

            foreach (DataRow row in tipsRows.Rows)
            {
                tips.Add(new Tip
                {
                    Id = Convert.ToInt32(row["ID"]),
                    CreditScoreBracket = row["CreditScoreBracket"].ToString(),
                    TipText = row["TipText"].ToString()
                });
            }
            return tips;
        }
    }
}
