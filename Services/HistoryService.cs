using src.Model;
using src.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Services
{
    public class HistoryService
    {
        private readonly HistoryRepository _historyRepository;

        public HistoryService(HistoryRepository historyRepository)
        {
            _historyRepository = historyRepository ?? throw new ArgumentNullException(nameof(historyRepository));
        }

        public List<HistoryCreditScore> GetHistoryByUserId(string userCNP)
        {
            if (string.IsNullOrWhiteSpace(userCNP))
            {
                throw new ArgumentException("User ID cannot be null");
            }
            return _historyRepository.GetHistoryForUser(userCNP);
        }

        public List<HistoryCreditScore> GetHistoryWeekly(string userCNP)
        {
            List<HistoryCreditScore> history = _historyRepository.GetHistoryForUser(userCNP);
            List<HistoryCreditScore> weeklyHistory = new List<HistoryCreditScore>();
            foreach (HistoryCreditScore h in history)
            {
                if(h.Date.Month == DateTime.Now.Month && h.Date.Day >= DateTime.Now.Day - 7)
                {
                    weeklyHistory.Add(h);
                }
            }

            return weeklyHistory;
        }

    }
}
