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
using src.Model;
using src.Services;
using src.Data;
using src.Repos;
using System.Web.Http.Controllers;


namespace src.View.Pages
{
    public sealed partial class AnalysisWindow : Window
    {
        User user;
        private readonly ActivityService _activityService;

        public AnalysisWindow(User selectedUser)
        {
            this.InitializeComponent();
            user = selectedUser;
            _activityService = new ActivityService(new ActivityRepository(new DatabaseConnection(), new UserRepository(new DatabaseConnection())));
            LoadUserData();
            LoadUserActivities();
        }

        public void LoadUserData()
        {
            FirstNameTextBlock.Text = $"First name: {user.FirstName}";
            LastNameTextBlock.Text = $"Last name: {user.LastName}";
            CNPTextBlock.Text = $"CNP: {user.CNP}";
            EmailTextBlock.Text = $"Email: {user.Email}";
            PhoneNumberTextBlock.Text = $"Phone number: {user.PhoneNumber}";
        }

        public void LoadUserActivities()
        {
            try
            {
                // Fetch activities from the ActivityService
                var activities = _activityService.GetActivityForUser(user.CNP);

                // Bind the fetched activities to the ListView
                ActivityListView.ItemsSource = activities;
            }
            catch (Exception ex)
            {
                // Handle errors (optional)
                Console.WriteLine($"Error loading activities: {ex.Message}");
            }
        }
    }
}
