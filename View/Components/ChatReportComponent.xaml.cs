using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using src.Services;
using src.Model;
using System;
using src.Helpers;

namespace src.View.Components
{
    public sealed partial class ChatReportComponent : Page
    {
        private readonly IChatReportService _chatReportService;

        public event EventHandler ReportSolved;

        public string ReportedUserCNP { get; set; }
        public string ReportedMessage { get; set; }
        public int ReportId { get; set; }

        public ChatReportComponent(IChatReportService chatReportService)
        {
            this.InitializeComponent();
            _chatReportService = chatReportService;
        }

        private async void PunishReportedUser(object sender, RoutedEventArgs e)
        {
            ChatReport chatReport = new ChatReport
            {
                Id = ReportId,
                ReportedUserCnp = ReportedUserCNP, 
                ReportedMessage = ReportedMessage 
            };

            await _chatReportService.PunishUser(chatReport);
            ReportSolved?.Invoke(this, EventArgs.Empty);
        }

        private void DoNotPunishReportedUser(object sender, RoutedEventArgs e)
        {
            ChatReport chatReport = new ChatReport
            {
                Id = ReportId,
                ReportedUserCnp = ReportedUserCNP,
                ReportedMessage = ReportedMessage
            };
            _chatReportService.DoNotPunishUser(chatReport);
            ReportSolved?.Invoke(this, EventArgs.Empty);
        }

        public async void SetReportData(int id, string reportedUserCnp, string reportedMessage)
        {
            ReportId = id;
            ReportedUserCNP = reportedUserCnp; 
            ReportedMessage = reportedMessage;

            bool apiSuggestion = await ProfanityChecker.IsMessageOffensive(reportedMessage);

            IdTextBlock.Text = $"Report ID: {id}";
            ReportedUserCNPTextBlock.Text = $"Reported user's CNP: {reportedUserCnp}";
            ReportedMessageTextBlock.Text = $"Message: {reportedMessage}";
            ApiSuggestionTextBlock.Text = apiSuggestion ? "The software marked this message as offensive" : "The software marked this message as inoffensive";
        }
    }
}
