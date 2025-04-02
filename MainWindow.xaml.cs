using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using src.Views; // Import Views namespace
using src.Services;
using src.Repos; // Import Services namespace
using src.Data;
using System.Collections.Generic;
using src.Model;
using src.View;
using src.View.Components;

namespace src
{
    public sealed partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            this.InitializeComponent();
            MainFrame.Navigate(typeof(UsersView));
        }


        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItemContainer != null)
            {
               string invokedItemTag = args.SelectedItemContainer.Tag.ToString();
                switch (invokedItemTag)
                {
                    case "ChatReports":
                        MainFrame.Navigate(typeof(ChatReportView));
                        break;
                    case "LoanRequest":
                        MainFrame.Navigate(typeof(LoanRequestView));
                        break;
                    case "Loans":
                        MainFrame.Navigate(typeof(LoansView));
                        break;
                    case "UsersList":
                        MainFrame.Navigate(typeof(UsersView));
                        break;
                    case "BillSplitReports":
                        MainFrame.Navigate(typeof(BillSplitReportPage));
                        break;
                    case "Zodiac":
                        ZodiacFeature(sender, null);
                        break;
                }
            }
        }

        private void ZodiacFeature(object sender, RoutedEventArgs e)
        {
            DatabaseConnection dbConn = new DatabaseConnection();
            UserRepository userRepository = new UserRepository(dbConn);
            ZodiacService zodiacService = new ZodiacService(userRepository);
            zodiacService.CreditScoreModificationBasedOnJokeAndCoinFlipAsync();
            zodiacService.CreditScoreModificationBasedOnAttributeAndGravity();

            MainFrame.Navigate(typeof(ZodiacFeatureView));
        }
    }
}
