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
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;


namespace src.View.Pages
{
    public sealed partial class AnalysisWindow : Window
    {
        User user;
        private readonly ActivityService _activityService;
        private readonly HistoryService _historyService;

        public AnalysisWindow(User selectedUser)
        {
            this.InitializeComponent();
            user = selectedUser;
            _activityService = new ActivityService(new ActivityRepository(new DatabaseConnection(), new UserRepository(new DatabaseConnection())));
            _historyService = new HistoryService(new HistoryRepository(new DatabaseConnection()));
            LoadUserData();
            LoadHistory();
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

        public void LoadHistory()
        {
            try
            {
                // Fetch history from the HistoryService
                var history = _historyService.GetHistoryMonthly(user.CNP);

                // Create the plot model
                var plotModel = new PlotModel { Title = "User Credit Score by Month" };

                // Create a new bar series for the monthly credit score data
                var barSeries = new BarSeries
                {
                    Title = "Credit Score",
                    StrokeThickness = 1,
                    FillColor = OxyColor.FromRgb(56, 130, 255),
                };

                // Add the data to the series
                foreach (var record in history)
                {
                    barSeries.Items.Add(new BarItem { Value = record.CreditScore });
                }

                // Set the X-Axis labels (Months)
                var categoryAxis = new CategoryAxis
                {
                    Position = AxisPosition.Left
                };


                // Add months to the axis
                foreach (var record in history)
                {
                    categoryAxis.Labels.Add(record.Date.ToString("MM/dd"));
                }

                // Add the axis and series to the plot
                plotModel.Axes.Add(categoryAxis);
                plotModel.Series.Add(barSeries);

                // Set the plot model to the PlotView
                CreditScorePlotView.Model = plotModel;
                CreditScorePlotView.InvalidatePlot(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading credit score history: {ex.Message}");
            }
        }
    }
}
