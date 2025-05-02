using src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Repos
{
    public interface ILoanRepository
    {
        public List<Loan> GetLoans();

        public List<Loan> GetUserLoans(string userCNP);

        public void AddLoan(Loan loan);

        public void UpdateLoan(Loan loan);

        public void DeleteLoan(int loanID);

        public Loan GetLoanById(int loanID);

        public void UpdateCreditScoreHistoryForUser(string UserCNP, int NewScore);


    }
}
