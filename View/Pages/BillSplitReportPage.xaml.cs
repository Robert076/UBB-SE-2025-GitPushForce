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
using src.View.Components;

namespace src.View
{
    public sealed partial class BillSplitReportPage : Page
    {
        public BillSplitReportPage()
        {
            this.InitializeComponent();
            LoadReports();
        }

        private void LoadReports()
        {
            BillSplitReportsContainer.Items.Clear(); // Clear previous items before reloading

            DatabaseConnection dbConn = new DatabaseConnection();
            BillSplitReportRepository repo = new BillSplitReportRepository(dbConn);
            BillSplitReportService service = new BillSplitReportService(repo);

            try
            {
                List<BillSplitReport> reports = service.GetBillSplitReports();

                foreach (var report in reports)
                {
                    BillSplitReportComponent reportComponent = new BillSplitReportComponent();
                    reportComponent.SetReportData(report);

                    // Subscribe to the event to refresh when a report is solved
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
            LoadReports(); // Refresh the list instantly when a report is solved
        }
    }
}
