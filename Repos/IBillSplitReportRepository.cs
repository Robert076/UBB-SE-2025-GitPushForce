using src.Data;
using src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Repos
{
    public interface IBillSplitReportRepository
    {
        public List<BillSplitReport> GetBillSplitReports();

        public void DeleteBillSplitReport(int id);

        public void CreateBillSplitReport(BillSplitReport billSplitReport);

        public bool CheckLogsForSimilarPayments(BillSplitReport billSplitReport);

        public int GetCurrentBalance(BillSplitReport billSplitReport);

        public decimal SumTransactionsSinceReport(BillSplitReport billSplitReport);

        public bool CheckHistoryOfBillShares(BillSplitReport billSplitReport);

        public bool CheckFrequentTransfers(BillSplitReport billSplitReport);

        public int GetNumberOfOffenses(BillSplitReport billSplitReport);

        public int GetCurrentCreditScore(BillSplitReport billSplitReport);

        public void UpdateCreditScore(BillSplitReport billSplitReport, int newCreditScore);

        public void UpdateCreditScoreHistory(BillSplitReport billSplitReport, int newCreditScore);

        public void IncrementNoOfBillSharesPaid(BillSplitReport billSplitReport);

        public int GetDaysOverdue(BillSplitReport billSplitReport);


    }
}
