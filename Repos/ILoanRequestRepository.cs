using src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Repos
{
    public interface ILoanRequestRepository
    {
        public List<LoanRequest> GetLoanRequests();

        public List<LoanRequest> GetUnsolvedLoanRequests();

        public void SolveLoanRequest(Int32 loanRequestID);

        public void DeleteLoanRequest(Int32 loanRequestID);


    }
}
