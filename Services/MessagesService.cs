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
        MessagesRepository _messagesRepository;

        public MessagesService(MessagesRepository messagesRepository)
        {
            _messagesRepository = messagesRepository;
        }

        public void GiveMessageToUser(string UserCNP)
        {
            DatabaseConnection dbConn = new DatabaseConnection();
            UserRepository userRepository = new UserRepository(dbConn);

            Int32 userCreditScore = userRepository.GetUserByCNP(UserCNP).CreditScore;
            try
            {
                if (userCreditScore >= 550)
                {
                    _messagesRepository.GiveUserRandomMessage(UserCNP);
                }
                else
                {
                    _messagesRepository.GiveUserRandomRoastMessage(UserCNP);
                }
            }

            catch (Exception e)
            {
                Console.WriteLine($"{e.Message},User is not found");
            }
        }
    }
}
