using src.Model;
using src.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace src.ViewModel
{
    public class InvestmentsViewModel
    {
        private readonly InvestmentsService _investmentsService;

        public ObservableCollection<InvestmentPortfolio> UsersPortofolio { get; set; }


        public InvestmentsViewModel(InvestmentsService investmentsService)
        {
            _investmentsService = investmentsService ?? throw new ArgumentNullException(nameof(investmentsService));
            UsersPortofolio = new ObservableCollection<InvestmentPortfolio>();
        }

        public void CalculateAndUpdateRiskScore()
        {
            _investmentsService.CalculateAndUpdateRiskScore();
        }

        public void CalculateAndUpdateROI()
        {
            _investmentsService.CalculateAndUpdateROI();
        }

        public void CreditScoreUpdateInvestmentsBased()
        {
            _investmentsService.CreditScoreUpdateInvestmentsBased();
        }

        public void LoadPortfolioSummary(string userCNP)
        {
            try
            {
                var portfoliosSummary = _investmentsService.GetPortfolioSummary();

                foreach (var userPortfolio in portfoliosSummary)
                {
                    UsersPortofolio.Add(userPortfolio);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
