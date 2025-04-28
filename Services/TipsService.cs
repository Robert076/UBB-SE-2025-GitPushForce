using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.Services.CircuitBreaker;
using src.Data;
using src.Model;
using src.Repos;


namespace src.Services
{
    class TipsService
    {
        TipsRepository _tipsRepository;

        public TipsService(TipsRepository tipsRepository)
        {
            _tipsRepository = tipsRepository;
        }

        public void GiveTipToUser(string UserCNP)
        {
            DatabaseConnection dbConn = new DatabaseConnection();
            UserRepository userRepository = new UserRepository(dbConn);
            
            try{
               
                Int32 userCreditScore = userRepository.GetUserByCNP(UserCNP).CreditScore;
                if (userCreditScore < 300 ) 
                {
                    _tipsRepository.GiveUserTipForLowBracket(UserCNP);
                }
                else if (userCreditScore < 550)
                {
                    _tipsRepository.GiveUserTipForMediumBracket(UserCNP);
                }
                else if (userCreditScore > 549)
                {
                    _tipsRepository.GiveUserTipForHighBracket(UserCNP);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message},User is not found");
            }
        }
    }
}
