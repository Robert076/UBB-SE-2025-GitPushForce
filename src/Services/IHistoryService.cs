using System.Collections.Generic;
using Src.Model;

namespace Src.Services
{
    public interface IHistoryService
    {
        public List<CreditScoreHistory> GetHistoryByUserCNP(string userCNP);
        public List<CreditScoreHistory> GetHistoryWeekly(string userCNP);
        public List<CreditScoreHistory> GetHistoryMonthly(string userCNP);
        public List<CreditScoreHistory> GetHistoryYearly(string userCNP);
    }
}
