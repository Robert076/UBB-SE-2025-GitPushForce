using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using src.Services;
using src.Model;
using src.Data;
using src.Repos;
using System;

namespace src.View.Components
{
    public sealed partial class ChatReportComponent : Page
    {
        private readonly ChatReportService _chatReportService;
        public event EventHandler ReportSolved;

        public ChatReportComponent()
        {
            this.InitializeComponent();
            _chatReportService = new ChatReportService(new ChatReportRepository(new DatabaseConnection()));
        }

        public int ReportId { get; set; }
        public string ReportedUserCNP { get; set; }
        public string ReportedMessage { get; set; }

        private async void OnSolveClick(object sender, RoutedEventArgs e)
        {
            var chatReport = new ChatReport
            {
                Id = ReportId,
                ReportedUserCNP = ReportedUserCNP,
                ReportedMessage = ReportedMessage
            };

            bool isSolved = await _chatReportService.SolveChatReport(chatReport);

            ReportSolved?.Invoke(this, EventArgs.Empty);
        }

        public void SetReportData(int id, string reportedUserCnp, string reportedMessage)
        {
            ReportId = id;
            ReportedUserCNP = reportedUserCnp;
            ReportedMessage = reportedMessage;

            ReportedUserCNPTextBlock.Text = ReportedUserCNP;
            ReportedMessageTextBlock.Text = ReportedMessage;
        }
    }
}
