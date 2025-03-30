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
                var history = _historyService.GetHistoryYearly(user.CNP);

                var plotModel = new PlotModel { Title = "User Credit Score by Month" };

                var barSeries = new BarSeries
                {
                    Title = "Credit Score",
                    StrokeThickness = 1
                };

                for (int i = 0; i < history.Count; i++)
                {
                    var record = history[i];
                    OxyColor barColor;

                    if (i == 0)
                    {
                        barColor = OxyColor.FromRgb(0, 255, 0); 
                    }
                    else
                    {
                        var previousRecord = history[i - 1];
                        if (record.CreditScore > previousRecord.CreditScore)
                        {
                            barColor = OxyColor.FromRgb(0, 255, 0); 
                        }
                        else if (record.CreditScore == previousRecord.CreditScore)
                        {
                            barColor = OxyColor.FromRgb(255, 255, 0); 
                        }
                        else
                        {
                            barColor = OxyColor.FromRgb(255, 0, 0);
                        }
                    }

                    
                    barSeries.Items.Add(new BarItem
                    {
                        Value = record.CreditScore,
                        Color = barColor
                    });
                }

                
                foreach (var record in history)
                {
                    barSeries.Items.Add(new BarItem { Value = record.CreditScore });
                }

                
                var categoryAxis = new CategoryAxis
                {
                    Position = AxisPosition.Left
                };


                foreach (var record in history)
                {
                    categoryAxis.Labels.Add(record.Date.ToString("MM/dd"));
                }

                plotModel.Axes.Add(categoryAxis);
                plotModel.Series.Add(barSeries);

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
