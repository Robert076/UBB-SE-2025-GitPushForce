using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using src.Repos;
using src.Data;
using src.Model;
using src.View.Pages;

namespace src.View.Components
{
    public sealed partial class UserInfoComponent : Page
    {
        public User user;

        public UserInfoComponent()
        {
            this.InitializeComponent();
        }

        public void SetUserData(User userData)
        {
            user = userData;
            NameTextBlock.Text = $"{user.FirstName}  {user.LastName}";
            CNPTextBlock.Text = $"{user.Cnp}";
            ScoreTextBlock.Text = $"Score: {user.CreditScore}";
        }

        private async void OnAnalysisClick(object sender, RoutedEventArgs e)
        {
            if (user != null)
            {
                AnalysisWindow analysisWindow = new AnalysisWindow(user);
                analysisWindow.Activate();
            }
        }

        private async void OnTipHistoryClick(object seder, RoutedEventArgs e)
        {
            if (user != null)
            {
                DatabaseConnection _dbConnection = new DatabaseConnection();
                TipHistoryWindow tipHistoryWindow = new TipHistoryWindow(user, new MessagesRepository(_dbConnection), new TipsRepository(_dbConnection));
                tipHistoryWindow.Activate();
            }
        }
    }
}
