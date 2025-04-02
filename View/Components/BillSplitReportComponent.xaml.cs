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
using src.Data;
using src.Model;
using src.Repos;
using src.Services;

namespace src.View.Components
{
    public sealed partial class BillSplitReportComponent : Page
    {
        private readonly BillSplitReportService _billSplitReportService;
        public event EventHandler ReportSolved;

        public int Id { get; set; }

        public string ReportedUserCNP { get; set; }
        public string ReportedUserFirstName { get; set; }
        public string ReportedUserLastName { get; set; }

        public string ReporterUserCNP{ get; set; }
        public string ReporterUserFirstName { get; set; }
        public string ReporterUserLastName { get; set; }

        public DateTime DateTransaction { get; set; }
        private float BillShare { get; set; }

        public BillSplitReportComponent()
        {
            this.InitializeComponent();
            _billSplitReportService = new BillSplitReportService(new BillSplitReportRepository(new DatabaseConnection()));
        }

        private async void OnSolveClick(object sender, RoutedEventArgs e)
        {
            var billSplitReport = new BillSplitReport
            {
                Id = Id,
                ReportedCNP = ReportedUserCNP,
                ReporterCNP = ReporterUserCNP,
                DateTransaction = DateTransaction,
                BillShare = BillShare
            };

            _billSplitReportService.SolveBillSplitReport(billSplitReport);
            ReportSolved?.Invoke(this, EventArgs.Empty);
        }

        private void OnDropReportClick(object sender, RoutedEventArgs e)
        {
            var billSplitReport = new BillSplitReport
            {
                Id = Id,
                ReportedCNP = ReportedUserCNP,
                ReporterCNP = ReporterUserCNP,
                DateTransaction = DateTransaction,
                BillShare = BillShare
            };

            _billSplitReportService.DeleteBillSplitReport(billSplitReport);
            ReportSolved?.Invoke(this, EventArgs.Empty);
        }

        public void SetReportData(BillSplitReport billSplitReport)
        {
            User reportedUser = _billSplitReportService.GetUserByCNP(billSplitReport.ReportedCNP);
            User reporterUser = _billSplitReportService.GetUserByCNP(billSplitReport.ReporterCNP);

            Id = billSplitReport.Id;

            ReportedUserCNP = billSplitReport.ReportedCNP;
            ReportedUserFirstName = reportedUser.FirstName;
            ReportedUserLastName = reportedUser.LastName;

            
            ReporterUserCNP = billSplitReport.ReporterCNP;
            ReporterUserFirstName = reporterUser.FirstName;
            ReporterUserLastName = reporterUser.LastName;

            DateTransaction = billSplitReport.DateTransaction;
            BillShare = billSplitReport.BillShare;


            IdTextBlock.Text = $"Report ID: {Id}";

            ReportedUserCNPTextBlock.Text = $"CNP: {ReportedUserCNP}";
            ReportedUserNameTextBlock.Text = $"{reportedUser.FirstName} {reportedUser.LastName}";

            ReporterUserCNPTextBlock.Text = $"CNP: {ReporterUserCNP}";
            ReporterUserNameTextBlock.Text = $"{reporterUser.FirstName} {reporterUser.LastName}";

            DateTransactionTextBlock.Text = $"{DateTransaction}";
            DaysOverdueTextBlock.Text = $"{_billSplitReportService.GetDaysOverdue(billSplitReport)} days overdue!";

            BillShareTextBlock.Text = $"Bill share: {BillShare}";
        }
    }
}
