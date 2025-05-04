using System.Collections.Generic;
using Src.Model;

namespace Src.Services
{
    public interface ILoanService
    {
        public List<Loan> GetLoans();
        public List<Loan> GetUserLoans(string userCNP);
        public void AddLoan(LoanRequest loanRequest);
        public void CheckLoans();
        public int ComputeNewCreditScore(User user, Loan loan);
        public void UpdateHistoryForUser(string userCNP, int newScore);
        public void IncrementMonthlyPaymentsCompleted(int loanID, float penalty);
    }
}
