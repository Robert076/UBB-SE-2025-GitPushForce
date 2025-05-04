using Microsoft.UI.Xaml.Controls;
using Src.Model;

namespace Src.View.Components
{
    public sealed partial class InvestmentComponent : Page
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public decimal TotalInvested { get; set; }
        public decimal TotalReturns { get; set; }
        public decimal AverageROI { get; set; }
        public int NumberOfInvestments { get; set; }
        public int RiskFactor { get; set; }

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
            RiskFactor = userPortfolio.RiskFactor;

            UserFirstNameTextBlock.Text = $"First Name: {FirstName}";
            UserSecondNameTextBlock.Text = $"Second Name: {SecondName}";
            TotalInvestedTextBlock.Text = $"Total Invested: {TotalInvested}";
            TotalReturnsTextBlock.Text = $"Total Returns: {TotalReturns}";
            AverageROITextBlock.Text = $"Average ROI: {AverageROI}";
            NumberOfInvestmentsTextBlock.Text = $"Number of Investments: {NumberOfInvestments}";
            RiskFactorTextBlock.Text = $"Risk Score: {RiskFactor}";
        }
    }
}
