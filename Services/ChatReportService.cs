using src.Repos;
using src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Helpers;

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

            if (isReportedMessageOffensive)
            {
                return true;
            }
            else
            {
                return false;
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
