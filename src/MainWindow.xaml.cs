using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Src.Views;
using Src.Services;
using Src.Repos;
using Src.Data;
using Src.View;
using Src.View.Components;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Src
{
    public sealed partial class MainWindow : Window
    {
        private readonly IServiceProvider serviceProvider;

        public MainWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;

            var usersView = this.serviceProvider.GetRequiredService<UsersView>();
            MainFrame.Content = usersView;
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItemContainer != null)
            {
               string invokedItemTag = args.SelectedItemContainer.Tag.ToString();
                switch (invokedItemTag)
                {
                    case "ChatReports":
                        var chatReportsPage = serviceProvider.GetRequiredService<ChatReportView>();
                        MainFrame.Content = chatReportsPage;
                        break;
                    case "LoanRequest":
                        var loanRequestPage = serviceProvider.GetRequiredService<LoanRequestView>();
                        MainFrame.Content = loanRequestPage;
                        break;
                    case "Loans":
                        var loansPage = serviceProvider.GetRequiredService<LoansView>();
                        MainFrame.Content = loansPage;
                        break;
                    case "UsersList":
                        var usersView = serviceProvider.GetRequiredService<UsersView>();
                        MainFrame.Content = usersView;
                        break;
                    case "BillSplitReports":
                        var billSplitPage = serviceProvider.GetRequiredService<BillSplitReportPage>();
                        MainFrame.Content = billSplitPage;
                        break;
                    case "Zodiac":
                        ZodiacFeature(sender, null);
                        break;
                    case "Investments":
                        MainFrame.Navigate(typeof(InvestmentsView));
                        break;
                }
            }
        }

        private void ZodiacFeature(object sender, RoutedEventArgs e)
        {
            DatabaseConnection dbConn = new DatabaseConnection();
            var httpClient = new HttpClient();
            UserRepository userRepository = new UserRepository(dbConn);
            ZodiacService zodiacService = new ZodiacService(userRepository,httpClient);

            zodiacService.CreditScoreModificationBasedOnJokeAndCoinFlipAsync();
            zodiacService.CreditScoreModificationBasedOnAttributeAndGravity();

            MainFrame.Navigate(typeof(ZodiacFeatureView));
        }
    }
}
