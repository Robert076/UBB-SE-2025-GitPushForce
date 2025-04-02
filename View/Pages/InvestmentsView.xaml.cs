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
    public sealed partial class InvestmentsView : Page
    {
        public InvestmentsView()
        {
            this.InitializeComponent();
        }

        private async void UpdateCreditScoreCommand(object sender, RoutedEventArgs e)
        {
            DatabaseConnection dbConn = new DatabaseConnection();
            InvestmentsRepository repo = new InvestmentsRepository(dbConn);
            InvestmentsService service = new InvestmentsService(new UserRepository(dbConn), repo);
            service.CreditScoreUpdateInvestmentsBased();
        }

        private async void CalculateROICommand(object sender, RoutedEventArgs e)
        {
            DatabaseConnection dbConn = new DatabaseConnection();
            InvestmentsRepository repo = new InvestmentsRepository(dbConn);
            InvestmentsService service = new InvestmentsService(new UserRepository(dbConn), repo);
            service.CalculateAndUpdateROI();
        }

        private async void CalculateRiskScoreCommand(object sender, RoutedEventArgs e)
        {
            DatabaseConnection dbConn = new DatabaseConnection();
            InvestmentsRepository repo = new InvestmentsRepository(dbConn);
            InvestmentsService service = new InvestmentsService(new UserRepository(dbConn), repo);
            service.CalculateAndUpdateRiskScore();
        }
        private void LoadInvestmentPortofolio()
        {
            UsersPortofolioContainer.Items.Clear(); // Clear previous items before reloading

            DatabaseConnection dbConn = new DatabaseConnection();
            InvestmentsRepository ivnestmentRepo = new InvestmentsRepository(dbConn);
            UserRepository userRepo = new UserRepository(dbConn);
            InvestmentsService service = new InvestmentsService(userRepo, ivnestmentRepo);

            try
            {
                List<InvestmentPortfolio> usersInvestmentPortofolioo = service.GetPortfolioSummary();

                foreach (var userPortofolio in usersInvestmentPortofolioo)
                {
                    ChatReportComponent reportComponent = new ChatReportComponent();
                    reportComponent.SetReportData(report.Id, report.ReportedUserCNP, report.ReportedMessage);

                    // Subscribe to the event to refresh when a report is solved
                    reportComponent.ReportSolved += OnReportSolved;

                    ChatReportsContainer.Items.Add(reportComponent);
                }
            }
            catch (Exception)
            {
                ChatReportsContainer.Items.Add("There are no user investments.");
            }
        }
    }
}
