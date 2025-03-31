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
        public string ReporterUserCNP{ get; set; }
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

        public void SetReportData(int id, string reportedUserCnp, string reporterUserCnp, DateTime dateTransaction, float billShare)
        {
            Id = id;
            ReportedUserCNP = reportedUserCnp;
            ReporterUserCNP = reporterUserCnp;
            DateTransaction = dateTransaction;
            BillShare = billShare;

            IdTextBlock.Text = $"Report ID: {id}";
            ReportedUserCNPTextBlock.Text = $"Reported user's CNP: {reportedUserCnp}";
            ReporterUserCNPTextBlock.Text = $"Reporter user's CNP: {reporterUserCnp}";
            DateTransactionTextBlock.Text = $"Date of transaction: {dateTransaction}";
            BillShareTextBlock.Text = $"Bill share: {billShare}";
        }
    }
}
