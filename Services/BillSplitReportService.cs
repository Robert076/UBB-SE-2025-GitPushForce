using src.Repos;
using System;
using System.Collections.Generic;
using src.Model;

namespace src.Services
{
    class BillSplitReportService : IBillSplitReportService
    {
        IBillSplitReportRepository _billSplitReportRepository;

        public BillSplitReportService(IBillSplitReportRepository billSplitReportRepository)
        {
            this._billSplitReportRepository = billSplitReportRepository;
        }

        public List<BillSplitReport> GetBillSplitReports()
        {
            return _billSplitReportRepository.GetBillSplitReports();
        }

        public void CreateBillSplitReport(BillSplitReport billSplitReport)
        {
            _billSplitReportRepository.CreateBillSplitReport(billSplitReport);
        }

        public int GetDaysOverdue(BillSplitReport billSplitReport)
        {
            return _billSplitReportRepository.GetDaysOverdue(billSplitReport);
        }

        public void SolveBillSplitReport(BillSplitReport billSplitReportToBeSolved)
        {

            // Calculate the number of days past due
            int daysPastDue = this.GetDaysOverdue(billSplitReportToBeSolved);

            // Calculate the time factor
            float timeFactor = Math.Min(50, (daysPastDue - 1) * 50 / 20.0f);

            // Calculate the amount factor
            float amountFactor = Math.Min(50, (billSplitReportToBeSolved.BillShare - 1) * 50 / 999.0f);

            // Calculate the initial gravity factor
            float gravityFactor = timeFactor + amountFactor;

            int currentBalance = _billSplitReportRepository.GetCurrentCreditScore(billSplitReportToBeSolved);
            decimal transactionsSum = _billSplitReportRepository.SumTransactionsSinceReport(billSplitReportToBeSolved);

            bool couldHavePaidBillShare = currentBalance + transactionsSum >= (decimal)billSplitReportToBeSolved.BillShare;

            // Adjust gravity factor based on checks
            if (couldHavePaidBillShare)
            {
                gravityFactor += gravityFactor * 0.1f;
            }
            if (_billSplitReportRepository.CheckHistoryOfBillShares(billSplitReportToBeSolved))
            {
                gravityFactor -= gravityFactor * 0.05f;
            }
            if (_billSplitReportRepository.CheckFrequentTransfers(billSplitReportToBeSolved))
            {
                gravityFactor -= gravityFactor * 0.05f;
            }

            // Add floor(10% of the number of offenses)%
            int numberOfOffenses = _billSplitReportRepository.GetNumberOfOffenses(billSplitReportToBeSolved);
            gravityFactor += (float)Math.Floor(numberOfOffenses * 0.1f);

            int newCreditScore = (int)Math.Floor(currentBalance - 0.2f * gravityFactor);

            // update the credit score
            _billSplitReportRepository.UpdateCreditScore(billSplitReportToBeSolved, newCreditScore);
            _billSplitReportRepository.UpdateCreditScoreHistory(billSplitReportToBeSolved, newCreditScore);

            // increment the number of bill shares paid by the reported user
            _billSplitReportRepository.IncrementNoOfBillSharesPaid(billSplitReportToBeSolved);

            // increase the number of offenses
            //UserRepository userRepo = new UserRepository(this._billSplitReportRepository.getDbConn()); TODO whyyyyyy
            //userRepo.IncrementOffenesesCountByOne(billSplitReportToBeSolved.ReportedUserCnp);

            _billSplitReportRepository.DeleteBillSplitReport(billSplitReportToBeSolved.Id);
        }

        public void DeleteBillSplitReport(BillSplitReport billSplitReportToBeSolved)
        {
            _billSplitReportRepository.DeleteBillSplitReport(billSplitReportToBeSolved.Id);
        }

        public User GetUserByCNP(string CNP)
        {
            //UserRepository userRepo = new UserRepository(this._billSplitReportRepository.getDbConn()); TODO whyyyyyy
            //return userRepo.GetUserByCNP(CNP);
            return new User();
        }
    }
}
