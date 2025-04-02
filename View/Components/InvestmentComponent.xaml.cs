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

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public decimal TotalInvested { get; set; }
        public decimal TotalReturns { get; set; }
        public decimal AverageROI { get; set; }
        public int NumberOfInvestments { get; set; }

        public InvestmentComponent()
        {
            this.InitializeComponent();
        }

        public void SetPortfolioSummary(InvestmentPortfolio userPortfolio)
        {
            FirstName = userPortfolio.FirstName;
            SecondName = userPortfolio.SecondName;
            TotalInvested = userPortfolio.TotalAmountInvested;
            TotalReturns = userPortfolio.TotalAmountReturned;
            AverageROI = userPortfolio.AverageROI;
            NumberOfInvestments = userPortfolio.NumberOfInvestments;

            UserFirstNameTextBlock.Text = $"First Name: {FirstName}";
            UserSecondNameTextBlock.Text = $"Second Name: {SecondName}";
            TotalInvestedTextBlock.Text = $"Total Invested: {TotalInvested}";
            TotalReturnsTextBlock.Text = $"Total Returns: {TotalReturns}";
            AverageROITextBlock.Text = $"Average ROI: {AverageROI}";
            NumberOfInvestmentsTextBlock.Text = $"Number of Investments: {NumberOfInvestments}";
        }
    }
}
