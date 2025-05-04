using System.Collections.Generic;
using Src.Model;

namespace Src.Repos
{
    public interface ILoanRepository
    {
        public List<Loan> GetLoans();

        public List<Loan> GetUserLoans(string userCNP);

        public void AddLoan(Loan loan);

        public void UpdateLoan(Loan loan);

        public void DeleteLoan(int loanID);

        public Loan GetLoanById(int loanID);

        public void UpdateCreditScoreHistoryForUser(string userCNP, int newScore);
    }
}
