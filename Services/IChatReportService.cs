using src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Services
{
    public interface IChatReportService
    {
        public void DoNotPunishUser(ChatReport chatReportToBeSolved);
        public Task<bool> PunishUser(ChatReport chatReportToBeSolved);
        public Task<bool> IsMessageOffensive(string messageToBeChecked);
        public void UpdateHistoryForUser(string UserCNP, int NewScore);
        public List<ChatReport> GetChatReports();

    }
}
