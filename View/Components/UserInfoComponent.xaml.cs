using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using src.ViewModel;
using src.Services;
using src.Repos;
using src.Data;
using src.Model;
using src.View.Pages;

namespace src.View.Components
{

    public sealed partial class UserInfoComponent : Page
    {

        private readonly UserViewModel _userViewModel;

        public User user;

        public UserInfoComponent()
        {
            this.InitializeComponent();
            _userViewModel = new UserViewModel(new UserService(new UserRepository(new DatabaseConnection())));
        }

        public void SetUserData(User userData)
        {
            user = userData;
            FirstNameTextBlock.Text = $"{user.FirstName}";
            LastNameTextBlock.Text = $"{user.LastName}";
            ScoreTextBlock.Text = $"Score: {user.CreditScore}";
        }

        private async void OnAnalysisClick(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
