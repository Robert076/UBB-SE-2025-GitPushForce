using System;
using System.Collections.Generic;
using Src.Model;

namespace Src.Repos
{
    public interface IChatReportRepository
    {
        public List<ChatReport> GetChatReports();

        public void DeleteChatReport(int id);

        public void UpdateScoreHistoryForUser(string userCNP, int newScore);

        public int GetNumberOfGivenTipsForUser(string reportedUserCnp);

        public void UpdateActivityLog(string reportedUserCnp, int amount);
    }
}
