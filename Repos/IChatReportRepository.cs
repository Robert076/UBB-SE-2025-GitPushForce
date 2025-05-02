using src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Repos
{
    public interface IChatReportRepository
    {
        public List<ChatReport> GetChatReports();

        public void DeleteChatReport(Int32 id);

        public void UpdateScoreHistoryForUser(string UserCNP, int NewScore);

        public int GetNumberOfGivenTipsForUser(string reportedUserCnp);

        public void UpdateActivityLog(string reportedUserCnp, int amount);
    }
}
