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

            List<HistoryCreditScore> history = new List<HistoryCreditScore>();

            try
            {
                history = _historyRepository.GetHistoryForUser(userCNP);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Error retrieving history for user: ", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving history for user: ", ex);
            }

            return history;
        }

        public List<HistoryCreditScore> GetHistoryWeekly(string userCNP)
        {
            List<HistoryCreditScore> history = new List<HistoryCreditScore>();

            try
            {
                history = _historyRepository.GetHistoryForUser(userCNP);
            }
            catch(ArgumentException ex)
            {
                throw new ArgumentException("Error retrieving history for user: ", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving history for user: ", ex);
            }


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

        public List<HistoryCreditScore> GetHistoryMonthly(string userCNP)
        {
            List<HistoryCreditScore> history = new List<HistoryCreditScore>();

            try
            {
                history = _historyRepository.GetHistoryForUser(userCNP);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Error retrieving history for user: ", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving history for user: ", ex);
            }

            List<HistoryCreditScore> monthlyHistory = new List<HistoryCreditScore>();

            foreach (HistoryCreditScore h in history)
            {
                if (h.Date.Month == DateTime.Now.Month)
                {
                    monthlyHistory.Add(h);
                }
            }

            return monthlyHistory;
        }


        public List<HistoryCreditScore> GetHistoryYearly(string userCNP)
        {
            List<HistoryCreditScore> history = new List<HistoryCreditScore>();

            try
            {
                history = _historyRepository.GetHistoryForUser(userCNP);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Error retrieving history for user: ", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving history for user: ", ex);
            }

            List<HistoryCreditScore> yearlyHistory = new List<HistoryCreditScore>();

            foreach (HistoryCreditScore h in history)
            {
                if (h.Date.Year == DateTime.Now.Year)
                {
                    yearlyHistory.Add(h);
                }
            }

            return yearlyHistory;
        }
    }
}
