using src.Model;
using src.Repos;
using src.Services;
using src.Data;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System;

namespace src.ViewModel
{
    public class BillSplitReportViewModel
    {
        private readonly IBillSplitReportService _billSplitReportService;
        
        public ObservableCollection<BillSplitReport> BillSplitReports { get; set; }

        public BillSplitReportViewModel()
        {
            BillSplitReports = new ObservableCollection<BillSplitReport>(_billSplitReportService.GetBillSplitReports());
        }

        public async Task LoadBillSplitReports()
        {
            try
            {
                var reports = _billSplitReportService.GetBillSplitReports();
                foreach (var report in reports)
                {
                    BillSplitReports.Add(report);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error: {exception.Message}");
            }
        }
    }
}
