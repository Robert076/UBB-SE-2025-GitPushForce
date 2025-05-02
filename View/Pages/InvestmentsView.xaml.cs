using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using src.Data;
using src.Repos;
using src.Services;
using src.Model;
using src.View.Components;
using System;

namespace src.View
{
    public sealed partial class InvestmentsView : Page
    {

        private DatabaseConnection _dbConnection;
        InvestmentsRepository _investmentsRepository;
        InvestmentsService _investmentsService;
        UserRepository _userRepository;
        public InvestmentsView()
        {
            _dbConnection = new DatabaseConnection();
            _investmentsRepository = new InvestmentsRepository(_dbConnection);
            _userRepository = new UserRepository(_dbConnection);
            _investmentsService = new InvestmentsService(_userRepository, _investmentsRepository);
            
            this.InitializeComponent();
            LoadInvestmentPortofolio();
        }

        private async void UpdateCreditScoreCommand(object sender, RoutedEventArgs e)
        {
            _investmentsService.CreditScoreUpdateInvestmentsBased();
        }

        private async void CalculateROICommand(object sender, RoutedEventArgs e)
        {
            _investmentsService.CalculateAndUpdateROI();
        }

        private async void CalculateRiskScoreCommand(object sender, RoutedEventArgs e)
        {
            _investmentsService.CalculateAndUpdateRiskScore();
            this.LoadInvestmentPortofolio();
        }
        private void LoadInvestmentPortofolio()
        {
            UsersPortofolioContainer.Items.Clear();
            try
            {
                List<InvestmentPortfolio> usersInvestmentPortofolioo = _investmentsService.GetPortfolioSummary();

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
