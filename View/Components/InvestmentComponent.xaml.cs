
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using src.Services;
using src.Model;
using src.Data;
using src.Repos;
using System;
using System.Collections.Generic;

namespace src.View.Components
{
    public sealed partial class InvestmentComponent : Page
    {
        private readonly InvestmentsService _investmentsService;
        public event EventHandler ReportSolved;

        public double TotalInvested { get; set; }
        public double TotalReturns { get; set; }
        public double AverageROI { get; set; }
        public int NumberOfInvestments { get; set; }

        public int ReportId { get; set; }

        //public InvestmentComponent()
        //{
        //    this.InitializeComponent();
        //    DatabaseConnection databaseConnection = new DatabaseConnection();
        //    _investmentsService = new InvestmentsService(new UserRepository(databaseConnection), new InvestmentsRepository(databaseConnection));
        //}

        public void SetPortfolioSummary(Dictionary<string, decimal> portfolioSummary)
        {
            // Display the data in text blocks
            //if (portfolioSummary.ContainsKey("Total Invested"))
            //    TotalInvestedTextBlock.Text = $"Total Invested: {portfolioSummary["Total Invested"]}";

            //if (portfolioSummary.ContainsKey("Total Returns"))
            //    TotalReturnsTextBlock.Text = $"Total Returns: {portfolioSummary["Total Returns"]}";

            //if (portfolioSummary.ContainsKey("Average ROI"))
            //    AverageROITextBlock.Text = $"Average ROI: {portfolioSummary["Average ROI"]}";

            //if (portfolioSummary.ContainsKey("Number of Investments"))
            //    NumberOfInvestmentsTextBlock.Text = $"Number of Investments: {portfolioSummary["Number of Investments"]}";
        }
    }
}
