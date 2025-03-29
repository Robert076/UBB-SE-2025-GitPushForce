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


        public int CreateZodiacModel(ZodiacModel zodiacModel)
        {
            if (zodiacModel == null)
            {
                throw new ArgumentNullException(nameof(zodiacModel), "ZodiacModel cannot be null.");
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CNP", zodiacModel.CNP),
                new SqlParameter("@Birthday", zodiacModel.Birthday.ToString("yyyy-MM-dd")),
                new SqlParameter("@CreditScore", zodiacModel.CreditScore),
                new SqlParameter("@ZodiacSign", zodiacModel.ZodiacSign),
                new SqlParameter("@ZodiacAttribute", zodiacModel.ZodiacAttribute)
            };

            try
            {
                int? result = dbConn.ExecuteScalar<int>("CreateZodiacModel", parameters, CommandType.StoredProcedure);
                return result ?? 0;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error: {ex.Message}");
            }
        }


    }
}
