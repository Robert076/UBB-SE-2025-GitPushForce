using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using src.Data;
using src.Repos;
using src.Services;
using src.Model;
using src.View.Components;

namespace src
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChatReportsButtonClick(object sender, RoutedEventArgs e)
        {
            DatabaseConnection dbConn = new DatabaseConnection();
            ChatReportRepository repo = new ChatReportRepository(dbConn);
            ChatReportService service = new ChatReportService(repo);
            List<ChatReport> chatReports = service.GetChatReports();

            foreach (var report in chatReports)
            {
                ChatReportComponent reportComponent = new ChatReportComponent();

                reportComponent.SetReportData(report.Id, report.ReportedUserCNP, report.ReportedMessage);

                ChatReportsContainer.Items.Add(reportComponent);
            }
        }
    }
}
