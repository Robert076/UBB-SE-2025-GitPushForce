using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Data;
using src.Model;
using Microsoft.Data.SqlClient;

namespace src.Repos
{
    public class ZodiacRepository
    {
        private readonly DatabaseConnection dbConn;

        public ZodiacRepository(DatabaseConnection dbConn)
        {
            this.dbConn = dbConn;
        }

        public ZodiacModel? GetZodiacModelByCNP(string cnp)
        {
            if (string.IsNullOrWhiteSpace(cnp))
            {
                throw new ArgumentException("Invalid CNP", nameof(cnp));
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserCNP", cnp)
            };

            try
            {
                DataTable dataTable = dbConn.ExecuteReader("GetUserByCNP", parameters, CommandType.StoredProcedure);

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    return null;
                }

                DataRow row = dataTable.Rows[0];

                
                int id = row["Id"] != DBNull.Value ? Convert.ToInt32(row["Id"]) : 0;
                string userCNP = row["CNP"] != DBNull.Value ? row["CNP"].ToString() : string.Empty;
                DateOnly birthday = row["Birthday"] != DBNull.Value ? DateOnly.FromDateTime(Convert.ToDateTime(row["Birthday"])) : new DateOnly();
                int creditScore = row["CreditScore"] != DBNull.Value ? Convert.ToInt32(row["CreditScore"]) : 0;
                string zodiacSign = row["ZodiacSign"] != DBNull.Value ? row["ZodiacSign"].ToString() : string.Empty;
                string zodiacAttribute = row["ZodiacAttribute"] != DBNull.Value ? row["ZodiacAttribute"].ToString() : string.Empty;

                return new ZodiacModel(id, userCNP, birthday, creditScore, zodiacSign, zodiacAttribute);
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error: {ex.Message}");
            }
        }


    }
}
