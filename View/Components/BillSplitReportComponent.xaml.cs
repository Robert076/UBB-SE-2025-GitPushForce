using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Src.Model;
using Src.Services;

namespace Src.View.Components
{
    public sealed partial class BillSplitReportComponent : Page
    {
        private readonly IBillSplitReportService billSplitReportService;
        public event EventHandler ReportSolved;

        public int Id { get; set; }
        public string ReportedUserCNP { get; set; }
        public string ReportedUserFirstName { get; set; }
        public string ReportedUserLastName { get; set; }
        public string ReporterUserCNP { get; set; }
        public string ReporterUserFirstName { get; set; }
        public string ReporterUserLastName { get; set; }
        public DateTime DateTransaction { get; set; }
        private float BillShare { get; set; }

        public BillSplitReportComponent(IBillSplitReportService billSplitReportService)
        {
            this.InitializeComponent();
            this.billSplitReportService = billSplitReportService;
        }

        private async void OnSolveClick(object sender, RoutedEventArgs e)
        {
            BillSplitReport billSplitReport = new BillSplitReport
            {
                Id = Id,
                ReportedUserCnp = ReportedUserCNP,
                ReportingUserCnp = ReporterUserCNP,
                DateOfTransaction = DateTransaction,
                BillShare = BillShare
            };

            billSplitReportService.SolveBillSplitReport(billSplitReport);
            ReportSolved?.Invoke(this, EventArgs.Empty);
        }

        private void OnDropReportClick(object sender, RoutedEventArgs e)
        {
            BillSplitReport billSplitReport = new BillSplitReport
            {
                Id = Id,
                ReportedUserCnp = ReportedUserCNP,
                ReportingUserCnp = ReporterUserCNP,
                DateOfTransaction = DateTransaction,
                BillShare = BillShare
            };

            billSplitReportService.DeleteBillSplitReport(billSplitReport);
            ReportSolved?.Invoke(this, EventArgs.Empty);
        }

        public void SetReportData(BillSplitReport billSplitReport)
        {
            User reportedUser = billSplitReportService.GetUserByCNP(billSplitReport.ReportedUserCnp);
            User reporterUser = billSplitReportService.GetUserByCNP(billSplitReport.ReportingUserCnp);

            Id = billSplitReport.Id;
            ReportedUserCNP = billSplitReport.ReportedUserCnp;
            ReportedUserFirstName = reportedUser.FirstName;
            ReportedUserLastName = reportedUser.LastName;
            ReporterUserCNP = billSplitReport.ReportingUserCnp;
            ReporterUserFirstName = reporterUser.FirstName;
            ReporterUserLastName = reporterUser.LastName;
            DateTransaction = billSplitReport.DateOfTransaction;
            BillShare = billSplitReport.BillShare;

            IdTextBlock.Text = $"Report ID: {Id}";
            ReportedUserCNPTextBlock.Text = $"CNP: {ReportedUserCNP}";
            ReportedUserNameTextBlock.Text = $"{reportedUser.FirstName} {reportedUser.LastName}";
            ReporterUserCNPTextBlock.Text = $"CNP: {ReporterUserCNP}";
            ReporterUserNameTextBlock.Text = $"{reporterUser.FirstName} {reporterUser.LastName}";
            DateTransactionTextBlock.Text = $"{DateTransaction}";
            DaysOverdueTextBlock.Text = $"{billSplitReportService.GetDaysOverdue(billSplitReport)} days overdue!";
            BillShareTextBlock.Text = $"Bill share: {BillShare}";
        }
    }
}