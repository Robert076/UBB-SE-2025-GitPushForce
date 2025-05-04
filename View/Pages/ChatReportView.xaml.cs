using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;
using Src.Model;
using Src.Services;
using Src.View.Components;

namespace Src.Views
{
    public sealed partial class ChatReportView : Page
    {
        private readonly Func<ChatReportComponent> componentFactory;
        private readonly IChatReportService chatReportService;

        public ChatReportView(Func<ChatReportComponent> componentFactory, IChatReportService chatReportService)
        {
            this.componentFactory = componentFactory;
            this.chatReportService = chatReportService;
            this.InitializeComponent();
            LoadChatReports();
        }

        private void LoadChatReports()
        {
            ChatReportsContainer.Items.Clear();

            try
            {
                List<ChatReport> chatReports = chatReportService.GetChatReports();
                foreach (var report in chatReports)
                {
                    ChatReportComponent reportComponent = componentFactory();
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
