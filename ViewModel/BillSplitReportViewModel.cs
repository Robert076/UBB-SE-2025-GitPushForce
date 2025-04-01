using src.Model;
using src.Repos;
using src.Services;
using src.Data;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using System;

namespace src.ViewModel
{
    public class BillSplitReportViewModel
    {
        private readonly BillSplitReportService _billSplitReportService;
        
        public ObservableCollection<BillSplitReport> BillSplitReports { get; set; }

        public BillSplitReportViewModel()
        {
            _billSplitReportService = new BillSplitReportService(new BillSplitReportRepository(new DatabaseConnection()));
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
