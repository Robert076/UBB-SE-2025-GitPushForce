using src.Model;
using System.Collections.Generic;

namespace src.Services
{
    public interface ILoanService
    {
        public List<Loan> GetLoans();
        public List<Loan> GetUserLoans(string userCNP);
        public void AddLoan(LoanRequest loanRequest);
        public void CheckLoans();
        public int ComputeNewCreditScore(User user, Loan loan);
        public void UpdateHistoryForUser(string UserCNP, int NewScore);
        public void incrementMonthlyPaymentsCompleted(int loanID, float penalty);
    }
}
