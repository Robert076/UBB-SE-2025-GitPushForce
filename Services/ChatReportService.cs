using src.Repos;
using src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Services
{
    public class ChatReportService
    {
        ChatReportRepository _chatReportRepository;

        public ChatReportService(ChatReportRepository chatReportRepository)
        {
            _chatReportRepository = chatReportRepository;
        }
        static public void SolveChatReport(ChatReport chatReportToBeSolved)
        {
            string reportedMessage = chatReportToBeSolved.ReportedMessage;

            bool isReportedMessageOffensive = 
        }
        static public bool IsMessageOffensive(string messageToBeChecked)
        {
            return true;
        }
    }
}
