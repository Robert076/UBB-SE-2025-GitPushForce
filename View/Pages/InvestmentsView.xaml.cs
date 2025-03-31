using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using src.Data;
using src.Repos;
using src.Services;
using src.Model;
using src.View.Components;
using System;

namespace src.Views
{
    public sealed partial class InvestmentReportView : Page
    {
        public InvestmentReportView()
        {
            this.InitializeComponent();
            LoadInvestmentReports();
        }

        private void LoadInvestmentReports()
        {
            //InvestmentReportsContainer.Items.Clear(); // Clear previous items before reloading

            //DatabaseConnection dbConn = new DatabaseConnection();
            //InvestmentsRepository repo = new InvestmentsRepository(dbConn);
            //InvestmentsService service = new InvestmentsService(new UserRepository(dbConn), repo);

            //try
            //{
            //    List<Investment> investmentReports = service.GetPortfolioSummary();

            //    foreach (var report in investmentReports)
            //    {
            //        InvestmentComponent investmentComponent = new InvestmentComponent();
            //        investmentComponent.SetInvestmentData(report.UserCNP, report.TotalInvested, report.TotalReturns, report.AverageROI, report.NumberOfInvestments);
            //        investmentComponent.ReportId = report.Id;

            //        // Subscribe to the event to refresh when a report is solved
            //        investmentComponent.ReportSolved += OnReportSolved;

            //        InvestmentReportsContainer.Items.Add(investmentComponent);
            //    }
            //}
            //catch (Exception)
            //{
            //    InvestmentReportsContainer.Items.Add("There are no investment reports that need solving.");
            //}
        }

        private void OnReportSolved(object sender, EventArgs e)
        {
            LoadInvestmentReports(); // Refresh the list instantly when a report is solved
        }
    }
}
