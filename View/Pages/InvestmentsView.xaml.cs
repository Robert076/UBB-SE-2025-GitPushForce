using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Src.Data;
using Src.Model;
using Src.Repos;
using Src.Services;
using Src.View.Components;

namespace Src.View
{
    public sealed partial class InvestmentsView : Page
    {
        private DatabaseConnection dbConnection;
        private InvestmentsRepository investmentsRepository;
        private InvestmentsService investmentsService;
        private UserRepository userRepository;
        public InvestmentsView()
        {
            dbConnection = new DatabaseConnection();
            investmentsRepository = new InvestmentsRepository(dbConnection);
            userRepository = new UserRepository(dbConnection);
            investmentsService = new InvestmentsService(userRepository, investmentsRepository);

            this.InitializeComponent();
            LoadInvestmentPortofolio();
        }

        private async void UpdateCreditScoreCommand(object sender, RoutedEventArgs e)
        {
            investmentsService.CreditScoreUpdateInvestmentsBased();
        }

        private async void CalculateROICommand(object sender, RoutedEventArgs e)
        {
            investmentsService.CalculateAndUpdateROI();
        }

        private async void CalculateRiskScoreCommand(object sender, RoutedEventArgs e)
        {
            investmentsService.CalculateAndUpdateRiskScore();
            this.LoadInvestmentPortofolio();
        }
        private void LoadInvestmentPortofolio()
        {
            UsersPortofolioContainer.Items.Clear();
            try
            {
                List<InvestmentPortfolio> usersInvestmentPortofolioo = investmentsService.GetPortfolioSummary();

                foreach (var userPortofolio in usersInvestmentPortofolioo)
                {
                    InvestmentComponent investmentComponent = new InvestmentComponent();
                    investmentComponent.SetPortfolioSummary(userPortofolio);

                    UsersPortofolioContainer.Items.Add(investmentComponent);
                }
            }
            catch (Exception)
            {
                UsersPortofolioContainer.Items.Add("There are no user investments.");
            }
        }
    }
}
