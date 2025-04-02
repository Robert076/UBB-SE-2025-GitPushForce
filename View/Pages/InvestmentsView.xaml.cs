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
    }
}
