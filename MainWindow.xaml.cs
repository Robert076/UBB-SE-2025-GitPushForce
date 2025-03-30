using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using src.Views; // Import Views namespace
using src.Services;
using src.Repos; // Import Services namespace
using src.Data;
using System.Collections.Generic;
using src.Model;
using src.View;

namespace src
{
    public sealed partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            this.InitializeComponent();
            MainFrame.Navigate(typeof(ChatReportView));
        }

        private void ChatReportsButtonClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(ChatReportView)); 
        }

        private void LoanRequestsButtonClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(LoanRequestView));
        }

        private void ZodiacFeature(object sender, RoutedEventArgs e)
        {
            DatabaseConnection dbConn = new DatabaseConnection();
            UserRepository userRepository = new UserRepository(dbConn);
            ZodiacService zodiacService = new ZodiacService(userRepository);
            zodiacService.CreditScoreModificationBaseOnJokeAndCoinFlipAsync();
            zodiacService.CreditScoreModificationBadeOnAttributeAndGravity();
        }
        
        private void UsersButtonClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(UsersView));
        }

        private void BillSplitReportsButtonClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(BillSplitReportPage));
        }
    }
}
