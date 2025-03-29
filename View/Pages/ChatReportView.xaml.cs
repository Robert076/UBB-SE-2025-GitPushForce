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

                    ChatReportsContainer.Items.Add(reportComponent);
                }
            }
            catch(Exception exception)
            {
                ChatReportsContainer.Items.Add("There are no chat reports that need solving.");
            }
            
        }
    }
}
