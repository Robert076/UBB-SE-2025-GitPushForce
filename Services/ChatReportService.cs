using src.Repos;
using src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Helpers;
using src.Data;

namespace src.Services
{
    public class ChatReportService
    {
        ChatReportRepository _chatReportRepository;

        public ChatReportService(ChatReportRepository chatReportRepository)
        {
            _chatReportRepository = chatReportRepository;
        }

        public async Task<bool> SolveChatReport(ChatReport chatReportToBeSolved)
        {
            string reportedMessage = chatReportToBeSolved.ReportedMessage;

            bool isReportedMessageOffensive = await IsMessageOffensive(reportedMessage);

            if (!isReportedMessageOffensive)
            {
                _chatReportRepository.DeleteChatReport(chatReportToBeSolved.Id);
                return false;
            }

            DatabaseConnection dbConn = new DatabaseConnection();
            UserRepository userRepo = new UserRepository(dbConn);

            Int32 noOffenses = userRepo.GetUserByCNP(chatReportToBeSolved.ReportedUserCNP).NoOffenses;
            const Int32 MINIMUM_NUMBER_OF_OFFENSES_BEFORE_PUNISHMENT_GROWS_DISTOPIANLY_ABSURD = 3;
            const Int32 CREDIT_SCORE_DECREASE_AMOUNT_FLAT_RATE = 15;

            if (noOffenses >= MINIMUM_NUMBER_OF_OFFENSES_BEFORE_PUNISHMENT_GROWS_DISTOPIANLY_ABSURD)
            {
                userRepo.PenalizeUser(chatReportToBeSolved.ReportedUserCNP, noOffenses * CREDIT_SCORE_DECREASE_AMOUNT_FLAT_RATE);
            }
            else
            {
                userRepo.PenalizeUser(chatReportToBeSolved.ReportedUserCNP, CREDIT_SCORE_DECREASE_AMOUNT_FLAT_RATE);
            }
            userRepo.IncrementOffenesesCountByOne(chatReportToBeSolved.ReportedUserCNP);
            _chatReportRepository.DeleteChatReport(chatReportToBeSolved.Id);
            return true;
        }
        public async Task<bool> IsMessageOffensive(string messageToBeChecked)
        {
            bool isOffensive = await ProfanityChecker.IsMessageOffensive(messageToBeChecked);
            return isOffensive;
        }

        public List<ChatReport> GetChatReports()
        {
            return _chatReportRepository.GetChatReports();
        }
    }
}
