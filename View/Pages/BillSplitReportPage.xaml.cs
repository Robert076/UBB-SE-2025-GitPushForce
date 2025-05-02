using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;
using src.Data;
using src.Model;
using src.Repos;
using src.Services;
using src.View.Components;

namespace src.View
{
    public sealed partial class BillSplitReportPage : Page
    {
        private readonly Func<BillSplitReportComponent> _componentFactory;

        public BillSplitReportPage(Func<BillSplitReportComponent> componentFactory)
        {
            _componentFactory = componentFactory;
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
                    var reportComponent = _componentFactory();
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
