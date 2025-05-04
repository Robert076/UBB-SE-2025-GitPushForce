using System;
using System.Collections.Generic;
using Src.Data;
using Src.Repos;
using Src.Model;

namespace Src.Services
{
    public class TipsService : ITipsService
    {
        private TipsRepository tipsRepository;

        public TipsService(TipsRepository tipsRepository)
        {
            this.tipsRepository = tipsRepository;
        }

        public void GiveTipToUser(string userCNP)
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            UserRepository userRepository = new UserRepository(dbConnection);

            try
            {
                int userCreditScore = userRepository.GetUserByCnp(userCNP).CreditScore;
                if (userCreditScore < 300)
                {
                    tipsRepository.GiveUserTipForLowBracket(userCNP);
                }
                else if (userCreditScore < 550)
                {
                    tipsRepository.GiveUserTipForMediumBracket(userCNP);
                }
                else if (userCreditScore > 549)
                {
                    tipsRepository.GiveUserTipForHighBracket(userCNP);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception.Message},User is not found");
            }
        }

        public List<Tip> GetTipsForGivenUser(string userCnp)
        {
            return tipsRepository.GetTipsForGivenUser(userCnp);
        }
    }
}
