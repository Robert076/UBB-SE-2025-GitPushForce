using System;
using System.Collections.Generic;
using Src.Model;
using Src.Repos;

namespace Src.Services
{
    public class BillSplitReportService : IBillSplitReportService
    {
        private readonly IBillSplitReportRepository billSplitReportRepository;

        public BillSplitReportService(IBillSplitReportRepository billSplitReportRepository)
        {
            this.billSplitReportRepository = billSplitReportRepository;
        }

        public List<BillSplitReport> GetBillSplitReports()
        {
            return billSplitReportRepository.GetBillSplitReports();
        }

        public void CreateBillSplitReport(BillSplitReport billSplitReport)
        {
            billSplitReportRepository.CreateBillSplitReport(billSplitReport);
        }

        public int GetDaysOverdue(BillSplitReport billSplitReport)
        {
            return billSplitReportRepository.GetDaysOverdue(billSplitReport);
        }

        public void SolveBillSplitReport(BillSplitReport billSplitReportToBeSolved)
        {
            int daysPastDue = this.GetDaysOverdue(billSplitReportToBeSolved);

            float timeFactor = Math.Min(50, (daysPastDue - 1) * 50 / 20.0f);

            float amountFactor = Math.Min(50, (billSplitReportToBeSolved.BillShare - 1) * 50 / 999.0f);

            float gravityFactor = timeFactor + amountFactor;

            int currentBalance = billSplitReportRepository.GetCurrentCreditScore(billSplitReportToBeSolved);
            decimal transactionsSum = billSplitReportRepository.SumTransactionsSinceReport(billSplitReportToBeSolved);

            bool couldHavePaidBillShare = currentBalance + transactionsSum >= (decimal)billSplitReportToBeSolved.BillShare;

            if (couldHavePaidBillShare)
            {
                gravityFactor += gravityFactor * 0.1f;
            }
            if (billSplitReportRepository.CheckHistoryOfBillShares(billSplitReportToBeSolved))
            {
                gravityFactor -= gravityFactor * 0.05f;
            }
            if (billSplitReportRepository.CheckFrequentTransfers(billSplitReportToBeSolved))
            {
                gravityFactor -= gravityFactor * 0.05f;
            }

            int numberOfOffenses = billSplitReportRepository.GetNumberOfOffenses(billSplitReportToBeSolved);
            gravityFactor += (float)Math.Floor(numberOfOffenses * 0.1f);

            int newCreditScore = (int)Math.Floor(currentBalance - (0.2f * gravityFactor));

            billSplitReportRepository.UpdateCreditScore(billSplitReportToBeSolved, newCreditScore);
            billSplitReportRepository.UpdateCreditScoreHistory(billSplitReportToBeSolved, newCreditScore);

            billSplitReportRepository.IncrementNoOfBillSharesPaid(billSplitReportToBeSolved);

            billSplitReportRepository.DeleteBillSplitReport(billSplitReportToBeSolved.Id);
        }

        public void DeleteBillSplitReport(BillSplitReport billSplitReportToBeSolved)
        {
            billSplitReportRepository.DeleteBillSplitReport(billSplitReportToBeSolved.Id);
        }

        public User GetUserByCNP(string cNP)
        {
            return new User();
        }
    }
}
