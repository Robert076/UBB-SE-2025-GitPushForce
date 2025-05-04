using System;
using System.Collections.ObjectModel;
using Src.Model;
using Src.Services;

namespace Src.ViewModel
{
    public class InvestmentsViewModel
    {
        private readonly IInvestmentsService investmentsService;

        public ObservableCollection<InvestmentPortfolio> UsersPortofolio { get; set; }

        public InvestmentsViewModel(IInvestmentsService investmentsService)
        {
            this.investmentsService = investmentsService ?? throw new ArgumentNullException(nameof(investmentsService));
            UsersPortofolio = new ObservableCollection<InvestmentPortfolio>();
        }

        public void CalculateAndUpdateRiskScore()
        {
            investmentsService.CalculateAndUpdateRiskScore();
        }

        public void CalculateAndUpdateROI()
        {
            investmentsService.CalculateAndUpdateROI();
        }

        public void CreditScoreUpdateInvestmentsBased()
        {
            investmentsService.CreditScoreUpdateInvestmentsBased();
        }

        public void LoadPortfolioSummary(string userCNP)
        {
            try
            {
                var portfoliosSummary = investmentsService.GetPortfolioSummary();

                foreach (var userPortfolio in portfoliosSummary)
                {
                    UsersPortofolio.Add(userPortfolio);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error: {exception.Message}");
            }
        }
    }
}
