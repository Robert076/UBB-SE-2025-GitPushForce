using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
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
                    GiveUserRandomMessage(UserCNP);
                }
                else
                {
                    GiveUserRandomRoastMessage(UserCNP);
                }
            }

            catch (Exception e)
            {
                Console.WriteLine($"{e.Message},User is not found");
            }
        }

        public void GiveUserRandomMessage(string userCNP)
        {
            DatabaseConnection dbConn = new DatabaseConnection();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserCNP", SqlDbType.VarChar, 16) { Value = userCNP }
            };
            DataTable messagesTable = dbConn.ExecuteReader("GetRandomCongratsMessage", null, CommandType.StoredProcedure);

            Random random = new Random();
            DataRow randomMessage = messagesTable.Rows[random.Next(messagesTable.Rows.Count)];

            Message selectedMessage = new Message
            {
                Id = Convert.ToInt32(randomMessage["ID"]),
                Type = randomMessage["Type"].ToString(),
                MessageText = randomMessage["Message"].ToString()
            };

            SqlParameter[] insertParams = new SqlParameter[]
            {
        new SqlParameter("@UserCNP", SqlDbType.VarChar, 16) { Value = userCNP },
        new SqlParameter("@MessageID", SqlDbType.Int) { Value = selectedMessage.Id }
            };

            dbConn.ExecuteNonQuery("InsertGivenMessage", insertParams, CommandType.StoredProcedure);
        }





        public void GiveUserRandomRoastMessage(string userCNP)
        {
            DatabaseConnection dbConn = new DatabaseConnection();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserCNP", SqlDbType.VarChar, 16) { Value = userCNP }
            };

            DataTable roastMessagesTable = dbConn.ExecuteReader("GetRandomRoastMessage", null, CommandType.StoredProcedure);

            if (roastMessagesTable.Rows.Count > 0)
            {
                Random random = new Random();
                DataRow randomRoastMessage = roastMessagesTable.Rows[random.Next(roastMessagesTable.Rows.Count)];

                Message selectedRoastMessage = new Message
                {
                    Id = Convert.ToInt32(randomRoastMessage["ID"]),
                    Type = randomRoastMessage["Type"].ToString(),
                    MessageText = randomRoastMessage["Message"].ToString()
                };

                SqlParameter[] insertParams = new SqlParameter[]
                {
                    new SqlParameter("@UserCNP", SqlDbType.VarChar, 16) { Value = userCNP },
                    new SqlParameter("@MessageID", SqlDbType.Int) { Value = selectedRoastMessage.Id }
                };

                dbConn.ExecuteNonQuery("InsertGivenRoastMessage", insertParams, CommandType.StoredProcedure);
            }
        }





    }
}
