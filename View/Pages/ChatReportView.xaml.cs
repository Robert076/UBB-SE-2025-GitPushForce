using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;
using src.Services;
using src.Model;
using src.View.Components;
using System;

namespace src.Views
{
    public sealed partial class ChatReportView : Page
    {
        private readonly Func<ChatReportComponent> _componentFactory;
        private readonly IChatReportService _chatReportService;

        public ChatReportView(Func<ChatReportComponent> componentFactory, IChatReportService chatReportService)
        {
            _componentFactory = componentFactory;
            _chatReportService = chatReportService;
            this.InitializeComponent();
            LoadChatReports();
        }

        private void LoadChatReports()
        {
            ChatReportsContainer.Items.Clear(); 

            try
            {
                List<ChatReport> chatReports = _chatReportService.GetChatReports();
                foreach (var report in chatReports)
                {
                    ChatReportComponent reportComponent = _componentFactory();
                    reportComponent.SetReportData(report.Id, report.ReportedUserCnp, report.ReportedMessage);

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
            LoadChatReports();
        }
    }
}
