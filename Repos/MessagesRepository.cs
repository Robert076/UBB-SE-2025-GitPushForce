using Microsoft.Data.SqlClient;
using src.Data;
using src.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace src.Repos
{
    public class MessagesRepository
    {
        private readonly DatabaseConnection _dbConnection;

        public MessagesRepository(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void GiveUserRandomMessage(string userCnp)
        {
            if (string.IsNullOrWhiteSpace(userCnp))
            {
                throw new ArgumentException("User CNP cannot be empty", nameof(userCnp));
            }

            try
            {
                const string SelectQuery = @"
                    SELECT TOP 1 Id, Type, Message 
                    FROM Messages 
                    WHERE Type = 'Congrats-message' 
                    ORDER BY NEWID()";

                DataTable messagesTable = _dbConnection.ExecuteReader(SelectQuery, null, CommandType.Text);

                if (messagesTable == null || messagesTable.Rows.Count == 0)
                {
                    throw new Exception("No congratulatory messages found");
                }

                DataRow messageRow = messagesTable.Rows[0];
                Message message = new Message
                {
                    Id = Convert.ToInt32(messageRow["Id"]),
                    Type = messageRow["Type"].ToString(),
                    MessageText = messageRow["Message"].ToString()
                };

                SqlParameter[] insertParameters = new SqlParameter[]
                {
                    new SqlParameter("@UserCnp", userCnp),
                    new SqlParameter("@MessageId", message.Id)
                };

                const string InsertQuery = @"
                    INSERT INTO GivenTips 
                        (UserCnp, MessageId, Date) 
                    VALUES 
                        (@UserCnp, @MessageId, GETDATE())";

                int rowsAffected = _dbConnection.ExecuteNonQuery(InsertQuery, insertParameters, CommandType.Text);

                if (rowsAffected == 0)
                {
                    throw new Exception("Failed to record message for user");
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error giving user random message", exception);
            }
        }

        public void GiveUserRandomRoastMessage(string userCnp)
        {
            if (string.IsNullOrWhiteSpace(userCnp))
            {
                throw new ArgumentException("User CNP cannot be empty", nameof(userCnp));
            }

            try
            {
                const string SelectQuery = @"
                    SELECT TOP 1 Id, Type, Message 
                    FROM Messages 
                    WHERE Type = 'Roast-message' 
                    ORDER BY NEWID()";

                DataTable messagesTable = _dbConnection.ExecuteReader(SelectQuery, null, CommandType.Text);

                if (messagesTable == null || messagesTable.Rows.Count == 0)
                {
                    throw new Exception("No roast messages found");
                }

                DataRow messageRow = messagesTable.Rows[0];
                Message message = new Message
                {
                    Id = Convert.ToInt32(messageRow["Id"]),
                    Type = messageRow["Type"].ToString(),
                    MessageText = messageRow["Message"].ToString()
                };

                SqlParameter[] insertParameters = new SqlParameter[]
                {
                    new SqlParameter("@UserCnp", userCnp),
                    new SqlParameter("@MessageId", message.Id)
                };

                const string InsertQuery = @"
                    INSERT INTO GivenTips 
                        (UserCnp, MessageId, Date) 
                    VALUES 
                        (@UserCnp, @MessageId, GETDATE())";

                int rowsAffected = _dbConnection.ExecuteNonQuery(InsertQuery, insertParameters, CommandType.Text);

                if (rowsAffected == 0)
                {
                    throw new Exception("Failed to record roast message for user");
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error giving user random roast message", exception);
            }
        }

        public List<Message> GetMessagesForGivenUser(string userCnp)
        {
            SqlParameter[] messageParameters = new SqlParameter[]
            {
                 new SqlParameter("@UserCNP", userCnp)
            };
            const string GetQuery = "SELECT m.ID, m.Type, m.Message FROM GivenTips gt INNER JOIN Messages m ON gt.MessageID = m.ID WHERE gt.UserCNP = @UserCNP;";
            DataTable messagesRows = _dbConnection.ExecuteReader(GetQuery, messageParameters, CommandType.Text);
            List<Message> messages = new List<Message>();

            foreach (DataRow row in messagesRows.Rows)
            {
                messages.Add(new Message
                {
                    Id = Convert.ToInt32(row["ID"]),
                    Type = row["Type"].ToString(),
                    MessageText = row["Message"].ToString()
                });
            }
            return messages;
        }
    }
}