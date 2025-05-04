using System.Collections.Generic;
using Src.Model;

namespace Src.Services
{
    public interface IBillSplitReportService
    {
        public List<BillSplitReport> GetBillSplitReports();
        public void CreateBillSplitReport(BillSplitReport billSplitReport);
        public int GetDaysOverdue(BillSplitReport billSplitReport);
        public void SolveBillSplitReport(BillSplitReport billSplitReportToBeSolved);
        public void DeleteBillSplitReport(BillSplitReport billSplitReportToBeSolved);
        public User GetUserByCNP(string cNP);
    }
}
