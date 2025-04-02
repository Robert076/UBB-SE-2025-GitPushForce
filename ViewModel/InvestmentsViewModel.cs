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
    public class InvestmentsViewModel : INotifyPropertyChanged
    {
        private readonly InvestmentsService _investmentsService;
        public Dictionary<string, decimal> PortfolioSummary { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public InvestmentsViewModel(InvestmentsService investmentsService)
        {
            _investmentsService = investmentsService ?? throw new ArgumentNullException(nameof(investmentsService));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
                PortfolioSummary = _investmentsService.GetPortfolioSummary(userCNP);
                OnPropertyChanged(nameof(PortfolioSummary));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}