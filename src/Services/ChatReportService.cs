using System.Collections.Generic;
using System.Threading.Tasks;
using Src.Data;
using Src.Helpers;
using Src.Model;
using Src.Repos;
namespace Src.Services
{
    public class ChatReportService : IChatReportService
    {
        private readonly IChatReportRepository chatReportRepository;

        public ChatReportService(IChatReportRepository chatReportRepository)
        {
            this.chatReportRepository = chatReportRepository;
        }

        public void DoNotPunishUser(ChatReport chatReportToBeSolved)
        {
            chatReportRepository.DeleteChatReport(chatReportToBeSolved.Id);
        }

        public async Task<bool> PunishUser(ChatReport chatReportToBeSolved)
        {
            DatabaseConnection dbConn = new DatabaseConnection();
            UserRepository userRepo = new UserRepository(dbConn);

            User reportedUser = userRepo.GetUserByCnp(chatReportToBeSolved.ReportedUserCnp);

            int noOffenses = reportedUser.NumberOfOffenses;
            const int MINIMUM_NUMBER_OF_OFFENSES_BEFORE_PUNISHMENT_GROWS_DISTOPIANLY_ABSURD = 3;
            const int CREDIT_SCORE_DECREASE_AMOUNT_FLAT_RATE = 15;

            int amount;

            if (noOffenses >= MINIMUM_NUMBER_OF_OFFENSES_BEFORE_PUNISHMENT_GROWS_DISTOPIANLY_ABSURD)
            {
                userRepo.PenalizeUser(chatReportToBeSolved.ReportedUserCnp, noOffenses * CREDIT_SCORE_DECREASE_AMOUNT_FLAT_RATE);
                int decrease = reportedUser.CreditScore - (CREDIT_SCORE_DECREASE_AMOUNT_FLAT_RATE * noOffenses);
                UpdateHistoryForUser(chatReportToBeSolved.ReportedUserCnp, decrease);
                amount = CREDIT_SCORE_DECREASE_AMOUNT_FLAT_RATE * noOffenses;
            }
            else
            {
                userRepo.PenalizeUser(chatReportToBeSolved.ReportedUserCnp, CREDIT_SCORE_DECREASE_AMOUNT_FLAT_RATE);
                int decrease = userRepo.GetUserByCnp(chatReportToBeSolved.ReportedUserCnp).CreditScore - CREDIT_SCORE_DECREASE_AMOUNT_FLAT_RATE;
                UpdateHistoryForUser(chatReportToBeSolved.ReportedUserCnp, decrease);
                amount = CREDIT_SCORE_DECREASE_AMOUNT_FLAT_RATE;
            }
            userRepo.IncrementOffensesCount(chatReportToBeSolved.ReportedUserCnp);
            chatReportRepository.DeleteChatReport(chatReportToBeSolved.Id);
            TipsService service = new TipsService(new TipsRepository(dbConn),new UserRepository(dbConn));
            service.GiveTipToUser(chatReportToBeSolved.ReportedUserCnp);

            int countTips = chatReportRepository.GetNumberOfGivenTipsForUser(chatReportToBeSolved.ReportedUserCnp);

            if (countTips % 3 == 0)
            {
                MessagesService services = new MessagesService(new MessagesRepository(dbConn),new UserRepository(dbConn));
                services.GiveMessageToUser(chatReportToBeSolved.ReportedUserCnp);
            }

            chatReportRepository.UpdateActivityLog(chatReportToBeSolved.ReportedUserCnp, amount);
            return true;
        }
        public async Task<bool> IsMessageOffensive(string messageToBeChecked)
        {
            bool isOffensive = await ProfanityChecker.IsMessageOffensive(messageToBeChecked);
            return isOffensive;
        }

        public void UpdateHistoryForUser(string userCNP, int newScore)
        {
            this.chatReportRepository.UpdateScoreHistoryForUser(userCNP, newScore);
        }

        public List<ChatReport> GetChatReports()
        {
            return chatReportRepository.GetChatReports();
        }

        public void DeleteChatReport(int id)
        {
            chatReportRepository.DeleteChatReport(id);
        }
    }
}
