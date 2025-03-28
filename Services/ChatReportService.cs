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
                return false;
            }

            DatabaseConnection dbConn = new DatabaseConnection();
            UserRepository userRepo = new UserRepository(dbConn);

            Int32 noOffenses = userRepo.GetUserByCNP(chatReportToBeSolved.ReportedUserCNP).NoOffenses;

            if (noOffenses > 3)
            {

            }
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
