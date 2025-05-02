using src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Services
{
    public interface IBillSplitReportService
    {
        public List<BillSplitReport> GetBillSplitReports();
        public void CreateBillSplitReport(BillSplitReport billSplitReport);
        public int GetDaysOverdue(BillSplitReport billSplitReport);
        public void SolveBillSplitReport(BillSplitReport billSplitReportToBeSolved);
        public void DeleteBillSplitReport(BillSplitReport billSplitReportToBeSolved);
        public User GetUserByCNP(string CNP);
    }
}
