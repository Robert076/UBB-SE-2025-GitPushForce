using src.Model;
using src.Services;
using System;
using System.Collections.ObjectModel;

namespace src.ViewModel
{
    public class InvestmentsViewModel
    {
        private readonly IInvestmentsService _investmentsService;

        public ObservableCollection<InvestmentPortfolio> UsersPortofolio { get; set; }

        public InvestmentsViewModel(IInvestmentsService investmentsService)
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
            catch (Exception exception)
            {
                Console.WriteLine($"Error: {exception.Message}");
            }
        }
    }
}
