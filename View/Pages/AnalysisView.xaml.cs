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
using src.Data;
using src.Repos;
using src.Services;
using src.Model;
using src.View.Components;

namespace src.View.Pages
{
    
    public sealed partial class AnalysisView : Page
    {
        public User user;

        public AnalysisView()
        {
            this.InitializeComponent();

        }

        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    base.OnNavigatedTo(e);

        //    if (e.Parameter is User receivedUser)
        //    {
        //        user = receivedUser;
        //        LoadUserAnalysis();
        //    }
        //}

        private void LoadUserAnalysis()
        {
            DatabaseConnection dbConn = new DatabaseConnection();
            UserRepository repo = new UserRepository(dbConn);
            UserService service = new UserService(repo);


            if (user != null)
            {
                FirstNameTextBlock.Text = $"First Name: {user.FirstName}";
                LastNameTextBlock.Text = $"Last Name: {user.LastName}";
                CNPTextBlock.Text = $"CNP: {user.CNP}";
                EmailTextBlock.Text = $"Email: {user.Email}";
                PhoneTextBlock.Text = $"Phone: {user.PhoneNumber}";
            }
        }
    }
}
