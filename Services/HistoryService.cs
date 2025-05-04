using System;
using System.Collections.Generic;
using Src.Model;
using Src.Repos;

namespace Src.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepository historyRepository;

        public HistoryService(IHistoryRepository historyRepository)
        {
            this.historyRepository = historyRepository ?? throw new ArgumentNullException(nameof(historyRepository));
        }

        public List<CreditScoreHistory> GetHistoryByUserCNP(string userCNP)
        {
            if (string.IsNullOrWhiteSpace(userCNP))
            {
                throw new ArgumentException("User CNP cannot be null");
            }

            List<CreditScoreHistory> history = new List<CreditScoreHistory>();

            try
            {
                history = historyRepository.GetHistoryForUser(userCNP);
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

        public List<CreditScoreHistory> GetHistoryWeekly(string userCNP)
        {
            List<CreditScoreHistory> history = new List<CreditScoreHistory>();

            try
            {
                history = historyRepository.GetHistoryForUser(userCNP);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Error retrieving history for user: ", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving history for user: ", ex);
            }
            List<CreditScoreHistory> weeklyHistory = new List<CreditScoreHistory>();

            foreach (CreditScoreHistory h in history)
            {
                if (h.Date >= DateOnly.FromDateTime(DateTime.Now.AddDays(-7)))
                {
                    weeklyHistory.Add(h);
                }
            }

            return weeklyHistory;
        }

        public List<CreditScoreHistory> GetHistoryMonthly(string userCNP)
        {
            List<CreditScoreHistory> history = new List<CreditScoreHistory>();

            try
            {
                history = historyRepository.GetHistoryForUser(userCNP);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Error retrieving history for user: ", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving history for user: ", ex);
            }

            List<CreditScoreHistory> monthlyHistory = new List<CreditScoreHistory>();

            foreach (CreditScoreHistory h in history)
            {
                if (h.Date >= DateOnly.FromDateTime(DateTime.Now.AddMonths(-1)))
                {
                    monthlyHistory.Add(h);
                }
            }

            return monthlyHistory;
        }
        public List<CreditScoreHistory> GetHistoryYearly(string userCNP)
        {
            List<CreditScoreHistory> history = new List<CreditScoreHistory>();

            try
            {
                history = historyRepository.GetHistoryForUser(userCNP);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Error retrieving history for user: ", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving history for user: ", ex);
            }

            List<CreditScoreHistory> yearlyHistory = new List<CreditScoreHistory>();

            foreach (CreditScoreHistory h in history)
            {
                if (h.Date >= DateOnly.FromDateTime(DateTime.Now.AddYears(-1)))
                {
                    yearlyHistory.Add(h);
                }
            }

            return yearlyHistory;
        }
    }
}
