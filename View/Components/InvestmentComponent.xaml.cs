using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using src.Services;
using src.Model;
using src.Data;
using src.Repos;
using System;

namespace src.View.Components
{
    public sealed partial class InvestmentComponent : Page
    {
        private readonly InvestmentsService _investmentsService;

        public decimal TotalInvested { get; set; }
        public decimal TotalReturns { get; set; }
        public decimal AverageROI { get; set; }
        public int NumberOfInvestments { get; set; }

        public InvestmentComponent()
        {
            this.InitializeComponent();
            _investmentsService = new InvestmentsService(new UserRepository(new DatabaseConnection()), new InvestmentsRepository(new DatabaseConnection()));
        }

        public void SetPortfolioSummary(InvestmentPortfolio userPortfolio)
        {
            TotalInvested = userPortfolio.TotalAmountInvested;
            TotalReturns = userPortfolio.TotalAmountReturned;
            AverageROI = userPortfolio.AverageROI;
            NumberOfInvestments = userPortfolio.NumberOfInvestments;

            TotalInvestedTextBlock.Text = $"Total Invested: {TotalInvested}";
            TotalReturnsTextBlock.Text = $"Total Returns: {TotalReturns}";
            AverageROITextBlock.Text = $"Average ROI: {AverageROI}";
            NumberOfInvestmentsTextBlock.Text = $"Number of Investments: {NumberOfInvestments}";
        }
    }
}
