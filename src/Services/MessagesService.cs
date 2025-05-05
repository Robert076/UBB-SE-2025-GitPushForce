using System;
using System.Collections.Generic;
using Src.Data;
using Src.Model;
using Src.Repos;

namespace Src.Services
{
    public class MessagesService : IMessagesService
    {
        private readonly IMessagesRepository messagesRepository;
        private readonly IUserRepository userRepository;

        public MessagesService(IMessagesRepository messagesRepository,IUserRepository userRepository)
        {
            this.messagesRepository = messagesRepository;
            this.userRepository = userRepository;
        }

        public void GiveMessageToUser(string userCNP)
        {
            if (string.IsNullOrWhiteSpace(userCNP))
            {
                throw new ArgumentException("User CNP cannot be empty", nameof(userCNP));
            }

            var user = userRepository.GetUserByCnp(userCNP);

            if (user == null)
            {
                Console.WriteLine("User not found");
                return; 
            }

            int userCreditScore = user.CreditScore;

            try
            {
                if (userCreditScore >= 550)
                {
                    messagesRepository.GiveUserRandomMessage(userCNP);
                }
                else
                {
                    messagesRepository.GiveUserRandomRoastMessage(userCNP);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}, User not found");
            }
        }


        public List<Message> GetMessagesForGivenUser(string userCnp)
        {
            return messagesRepository.GetMessagesForGivenUser(userCnp);
        }
    }
}
