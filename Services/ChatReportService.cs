using src.Repos;
using src.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using src.Helpers;
using src.Data;
using System.Data;
using Microsoft.Data.SqlClient;
namespace src.Services
{
    public class ChatReportService : IChatReportService
    {
        IChatReportRepository _chatReportRepository;

        public ChatReportService(IChatReportRepository chatReportRepository)
        {
            _chatReportRepository = chatReportRepository;
        }

        public void DoNotPunishUser(ChatReport chatReportToBeSolved)
        {
            _chatReportRepository.DeleteChatReport(chatReportToBeSolved.Id);
        }

        public async Task<bool> PunishUser(ChatReport chatReportToBeSolved)
        {
            DatabaseConnection dbConn = new DatabaseConnection();
            UserRepository userRepo = new UserRepository(dbConn);

            User reportedUser = userRepo.GetUserByCnp(chatReportToBeSolved.ReportedUserCnp);

            Int32 noOffenses = reportedUser.NumberOfOffenses;
            const Int32 MINIMUM_NUMBER_OF_OFFENSES_BEFORE_PUNISHMENT_GROWS_DISTOPIANLY_ABSURD = 3;
            const Int32 CREDIT_SCORE_DECREASE_AMOUNT_FLAT_RATE = 15;

            int amount;

            if (noOffenses >= MINIMUM_NUMBER_OF_OFFENSES_BEFORE_PUNISHMENT_GROWS_DISTOPIANLY_ABSURD)
            {
                userRepo.PenalizeUser(chatReportToBeSolved.ReportedUserCnp, noOffenses * CREDIT_SCORE_DECREASE_AMOUNT_FLAT_RATE);
                Int32 decrease = reportedUser.CreditScore - CREDIT_SCORE_DECREASE_AMOUNT_FLAT_RATE * noOffenses;
                UpdateHistoryForUser(chatReportToBeSolved.ReportedUserCnp, decrease);
                amount = CREDIT_SCORE_DECREASE_AMOUNT_FLAT_RATE * noOffenses;
            }
            else
            {
                userRepo.PenalizeUser(chatReportToBeSolved.ReportedUserCnp, CREDIT_SCORE_DECREASE_AMOUNT_FLAT_RATE);
                Int32 decrease = userRepo.GetUserByCnp(chatReportToBeSolved.ReportedUserCnp).CreditScore - CREDIT_SCORE_DECREASE_AMOUNT_FLAT_RATE;
                UpdateHistoryForUser(chatReportToBeSolved.ReportedUserCnp, decrease);
                amount = CREDIT_SCORE_DECREASE_AMOUNT_FLAT_RATE;
            }
            userRepo.IncrementOffensesCount(chatReportToBeSolved.ReportedUserCnp);
            _chatReportRepository.DeleteChatReport(chatReportToBeSolved.Id);
            TipsService service = new TipsService(new TipsRepository(dbConn));
            service.GiveTipToUser(chatReportToBeSolved.ReportedUserCnp);

            int countTips = _chatReportRepository.GetNumberOfGivenTipsForUser(chatReportToBeSolved.ReportedUserCnp);

            if (countTips % 3 == 0)
            {
                MessagesService services = new MessagesService(new MessagesRepository(dbConn));
                services.GiveMessageToUser(chatReportToBeSolved.ReportedUserCnp);
            }

            _chatReportRepository.UpdateActivityLog(chatReportToBeSolved.ReportedUserCnp, amount);
            return true;
        }


        public async Task<bool> IsMessageOffensive(string messageToBeChecked)
        {
            bool isOffensive = await ProfanityChecker.IsMessageOffensive(messageToBeChecked);
            return isOffensive;
        }

        public void UpdateHistoryForUser(string UserCNP, int NewScore)
        {
            this._chatReportRepository.UpdateScoreHistoryForUser(UserCNP, NewScore);
        }

        public List<ChatReport> GetChatReports()
        {
            return _chatReportRepository.GetChatReports();
        }

        public void DeleteChatReport(int id)
        {
            _chatReportRepository.DeleteChatReport(id);
        }
       
        
    }
}
