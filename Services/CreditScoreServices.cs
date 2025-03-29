using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using src.Data;
using src.Model;
using src.Repos;


namespace src.Services
{
    class CreditScoreServices
    {
        public void GiveTipToUser(string UserCNP)
        {
            DatabaseConnection dbConn = new DatabaseConnection();
            UserRepository userRepository = new UserRepository(dbConn);
            try{
                User user = userRepository.GetUserByCNP(UserCNP);
                if(user.CreditScore < 300 && user.CreditScore >= 100)
                {
                    GiveUserTipForLowBracket(UserCNP);
                }
                else if(user.CreditScore < 550 && user.CreditScore >= 300)
                {
                    GiveUserTipForMediumBracket(UserCNP);
                }
                else
                {
                    GiveUserTipForHighBracket(UserCNP);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message},User is not found");
            }
        }
        public void GiveUserTipForLowBracket(string userCNP)
        {
            DatabaseConnection dbConn = new DatabaseConnection();
            SqlParameter[] parameters = new SqlParameter[]
            {
                 new SqlParameter("@UserCNP", SqlDbType.VarChar, 16) { Value = userCNP }
            };

            DataTable smallCreditTips = dbConn.ExecuteReader("GetLowCreditScoreTips", null, CommandType.StoredProcedure);

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

            dbConn.ExecuteNonQuery("InsertGivenTip", insertParams, CommandType.StoredProcedure);
        }


        public void GiveUserTipForMediumBracket(string userCNP)
        {


        }
        public void GiveUserTipForHighBracket(string userCNP)
        {

        }
    }
        
}
