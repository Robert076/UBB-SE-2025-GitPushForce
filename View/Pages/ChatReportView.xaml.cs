using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using src.Data;
using src.Repos;
using src.Services;
using src.Model;
using src.View.Components;
using System;

namespace src.Views
{
    public sealed partial class ChatReportView : Page
    {
        public ChatReportView()
        {
            this.InitializeComponent();
            LoadChatReports();
        }

        private void LoadChatReports()
        {
            ChatReportsContainer.Items.Clear(); // Clear previous items before reloading

            DatabaseConnection dbConn = new DatabaseConnection();
            ChatReportRepository repo = new ChatReportRepository(dbConn);
            ChatReportService service = new ChatReportService(repo);

            try
            {
                List<ChatReport> chatReports = service.GetChatReports();

                foreach (var report in chatReports)
                {
                    ChatReportComponent reportComponent = new ChatReportComponent();
                    reportComponent.SetReportData(report.Id, report.ReportedUserCNP, report.ReportedMessage);

                    // Subscribe to the event to refresh when a report is solved
                    reportComponent.ReportSolved += OnReportSolved;

                    ChatReportsContainer.Items.Add(reportComponent);
                }
            }
            catch (Exception)
            {
                ChatReportsContainer.Items.Add("There are no chat reports that need solving.");
            }
        }

        private void OnReportSolved(object sender, EventArgs e)
        {
            LoadChatReports(); // Refresh the list instantly when a report is solved
        }
    }
}
