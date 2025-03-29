using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Data;
using src.Repos;
using src.Model;

namespace src.Services
{
    class MessagesService
    {

        public void GiveMessageToUser(string UserCNP)
        {
            DatabaseConnection dbConn = new DatabaseConnection();
            UserRepository userRepository = new UserRepository(dbConn);

            Int32 userCreditScore = userRepository.GetUserByCNP(UserCNP).CreditScore;
            try
            {
                if (userCreditScore >= 550)
                {
                    GiveUserCongratsMessage(UserCNP);
                }
                else
                {
                    GiveUserRoastMessage(UserCNP);
                }
            }

            catch (Exception e)
            {
                Console.WriteLine($"{e.Message},User is not found");
            }
        }

            public void GiveUserCongratsMessage(string userCNP) {
           
            DatabaseConnection dbConn = new DatabaseConnection();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserCNP", System.Data.SqlDbType.VarChar, 16)
                {
                    Value = userCNP
                }
            };

            DataTable congratsMesassage = dbConn.ExecuteReader("GetCongratsMessages", null, CommandType.StoredProcedure);

            Random random = new Random();
            DataRow randomMessage = congratsMesassage.Rows[random.Next(congratsMesassage.Rows.Count)];

            Message selectedMessage = new Message
            {
                Id = Convert.ToInt32(randomMessage["Id"]),
                Type = randomMessage["Type"].ToString(),
                Message = randomMessage["Message"].ToString()
            };
            
            SqlParameter[] insertParams = new SqlParameter[]
            {
                    new SqlParameter("@UserCNP", SqlDbType.VarChar, 16) { Value = userCNP },
                    new SqlParameter("@MessageId", SqlDbType.Int) { Value = selectedMessage.Id }
            };

            dbConn.ExecuteNonQuery("InsertGivenTip", CommandType.StoredProcedure);

        }




        public void GiveUserRoastMessage(string userCNP) {
        }
            

            
        
    }
}
