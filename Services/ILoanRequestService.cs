using src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Services
{
    public interface ILoanRequestService
    {
        public string GiveSuggestion(LoanRequest loanRequest);
        public void SolveLoanRequest(LoanRequest loanRequest);
        public void DenyLoanRequest(LoanRequest loanRequest);
        //public bool PastUnpaidLoans(User user, LoanServices loanService); TODO: after adding interfaces
        //public float ComputeMonthlyDebtAmount(User user, LoanServices loanServices); TODO: after adding interfaces
        public List<LoanRequest> GetLoanRequests();
        public List<LoanRequest> GetUnsolvedLoanRequests();
    }
}
