using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using src.Data;
using src.Repos;
using src.Services;
using src.Model;
using src.View.Components;

namespace src.Views
{
    public sealed partial class ChatReportView : Page
    {
        public ChatReportView()
        {
            this.InitializeComponent();
            LoadChatReports(); // Load the reports when the page is initialized
        }

        // This method will be similar to your ChatReportsButtonClick method in MainWindow.xaml.cs
        private void LoadChatReports()
        {
            // Initialize the database connection, repository, and service
            DatabaseConnection dbConn = new DatabaseConnection();
            ChatReportRepository repo = new ChatReportRepository(dbConn);
            ChatReportService service = new ChatReportService(repo);

            // Fetch the chat reports using the service
            List<ChatReport> chatReports = service.GetChatReports();

            // Loop through each report and create a ChatReportComponent for each one
            foreach (var report in chatReports)
            {
                // Create a new ChatReportComponent
                ChatReportComponent reportComponent = new ChatReportComponent();

                // Set the data for the component
                reportComponent.SetReportData(report.Id, report.ReportedUserCNP, report.ReportedMessage);

                // Add the component to the ItemsControl on the page
                ChatReportsContainer.Items.Add(reportComponent);
            }
        }
    }
}
