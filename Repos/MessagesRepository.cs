using src.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using src.Repos;
using src.Model;

namespace src.Repos
{
    class MessagesRepository
    {
        private readonly DatabaseConnection dbConn;

        public MessagesRepository(DatabaseConnection dbConn)
        {
            this.dbConn = dbConn;
        }

        public void GiveUserRandomMessage(string userCNP)
        {
            string selectQuery = "SELECT * FROM Messages WHERE Type = 'Congrats-message' ORDER BY NEWID() OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY;";
            DataTable messagesTable = dbConn.ExecuteReader(selectQuery, null, CommandType.Text);

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

            string insertQuery = "INSERT INTO GivenTips (UserCNP, MessageID, Date) VALUES (@UserCNP, @MessageID, GETDATE());";
            dbConn.ExecuteNonQuery(insertQuery, insertParams, CommandType.Text);
        }

        public void GiveUserRandomRoastMessage(string userCNP)
        {
            string selectQuery = "SELECT * FROM Messages WHERE Type = 'Roast-message' ORDER BY NEWID();";
            DataTable roastMessagesTable = dbConn.ExecuteReader(selectQuery, null, CommandType.Text);

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
                string insertQuery = "INSERT INTO GivenTips (UserCNP, MessageID, Date) VALUES (@UserCNP, @MessageID, GETDATE());";
                dbConn.ExecuteNonQuery(insertQuery, insertParams, CommandType.Text);
            }
        }
    }
}
