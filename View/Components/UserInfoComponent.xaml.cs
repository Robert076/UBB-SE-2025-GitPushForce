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

namespace src.View.Components
{

    public sealed partial class UserInfoComponent : Page
    {

        private readonly UserViewModel _userViewModel;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Score { get; set; }

        public UserInfoComponent()
        {
            this.InitializeComponent();
            _userViewModel = new UserViewModel(new UserService(new UserRepository(new DatabaseConnection())));
        }

        public void SetUserData(string firstName, string lastName, int score)
        {
            FirstName = firstName;
            LastName = lastName;
            Score = score;
            FirstNameTextBlock.Text = $"First name: {firstName}";
            LastNameTextBlock.Text = $"Last name: {lastName}";
            ScoreTextBlock.Text = $"Score: {score}";
        }


    }
}
