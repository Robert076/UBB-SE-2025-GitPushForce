using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;
using Src.Data;
using Src.Model;
using Src.Repos;
using Src.Services;
using Src.View.Components;

namespace Src.View
{
    public sealed partial class BillSplitReportPage : Page
    {
        private readonly Func<BillSplitReportComponent> componentFactory;

        public BillSplitReportPage(Func<BillSplitReportComponent> componentFactory)
        {
            this.componentFactory = componentFactory;
            this.InitializeComponent();
            LoadReports();
        }

        private void LoadReports()
        {
            BillSplitReportsContainer.Items.Clear();

            DatabaseConnection dbConnection = new DatabaseConnection();
            BillSplitReportRepository billSplitReportRepository = new BillSplitReportRepository(dbConnection);
            BillSplitReportService billSplitReportService = new BillSplitReportService(billSplitReportRepository);

            try
            {
                List<BillSplitReport> reports = billSplitReportService.GetBillSplitReports();

                foreach (var report in reports)
                {
                    var reportComponent = componentFactory();
                    reportComponent.SetReportData(report);
                    reportComponent.ReportSolved += OnReportSolved;
                    BillSplitReportsContainer.Items.Add(reportComponent);
                }
            }
            catch (Exception)
            {
                BillSplitReportsContainer.Items.Add("There are no chat reports that need solving.");
            }
        }

        private void OnReportSolved(object sender, EventArgs e)
        {
            LoadReports();
        }
    }
}
