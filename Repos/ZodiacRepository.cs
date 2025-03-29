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
    class ZodiacRepository
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

                return new ZodiacModel(
                    Convert.ToInt32(row["Id"]),
                    row["CNP"]?.ToString() ?? string.Empty,
                    row["Birthday"] is DBNull ? new DateOnly() : DateOnly.FromDateTime(Convert.ToDateTime(row["Birthday"])),
                    row["CreditScore"] is DBNull ? 0 : Convert.ToInt32(row["CreditScore"]),
                    row["ZodiacSign"]?.ToString() ?? string.Empty,
                    row["ZodiacAttribute"]?.ToString() ?? string.Empty
                );
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error: {ex.Message}");
            }
        }


    }
}
