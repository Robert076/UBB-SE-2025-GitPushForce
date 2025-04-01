using Microsoft.TeamFoundation.Build.WebApi;
using src.Data;
using src.Model;
using src.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Services
{
    class LoanRequestService
    {
        LoanRequestRepository _loanRequestRepository;

        public LoanRequestService(LoanRequestRepository loanRequestRepository)
        {
            _loanRequestRepository = loanRequestRepository;
        }

        public string GiveSuggestion(LoanRequest loanRequest)
        {
            DatabaseConnection dbConn = new DatabaseConnection();
            UserRepository userRepo = new UserRepository(dbConn);
            LoanServices loanService = new LoanServices(new LoanRepository(dbConn));

            User user = userRepo.GetUserByCNP(loanRequest.UserCNP);

            string suggestion = string.Empty;

            if (loanRequest.Amount > user.Income * 10)
            {
                suggestion = "Amount requested is too high for user income";
            }

            if (user.CreditScore < 300)
            {
                if (suggestion.Length > 0)
                {
                    suggestion += ", ";
                }
                suggestion += "Credit score is too low";
            }

            if (user.Income <= 0)
            {
                if (suggestion.Length > 0)
                {
                    suggestion += ", ";
                }
                suggestion += "User has no income";
            }

            if (user.RiskScore > 70)
            {
                if (suggestion.Length > 0)
                {
                    suggestion += ", ";
                }
                suggestion += "User risk score is too high";
            }

            if (suggestion.Length > 0)
            {
                suggestion = "User does not qualify for loan: " + suggestion;
            }

            return suggestion;
        }

        public void SolveLoanRequest(LoanRequest loanRequest)
        {
            _loanRequestRepository.SolveLoanRequest(loanRequest.RequestID);
        }

        public void DenyLoanRequest(LoanRequest loanRequest)
        {
            _loanRequestRepository.DeleteLoanRequest(loanRequest.RequestID);
        }

        public bool PastUnpaidLoans(User user, LoanServices loanService)
        {
            List<Loan> loans;
            try
            {
                loans = loanService.GetUserLoans(user.CNP);
            }
            catch (Exception)
            {
                loans = new List<Loan>();
            }
            foreach (Loan loan in loans)
            {
                if (loan.State == "Active" && loan.RepaymentDate < DateTime.Today)
                {
                    return true;
                }
            }

            return false;
        }

        public float ComputeMonthlyDebtAmount(User user, LoanServices loanServices)
        {
            List<Loan> loans;
            try
            {
                loans = loanServices.GetUserLoans(user.CNP);
            }
            catch (Exception)
            {
                loans = new List<Loan>();
            }
            float monthlyDebtAmount = 0;

            foreach (Loan loan in loans)
            {
                if (loan.State == "Active")
                {
                    monthlyDebtAmount += loan.MonthlyPaymentAmount;
                }
            }

            return monthlyDebtAmount;
        }

        public List<LoanRequest> GetLoanRequests()
        {
            return _loanRequestRepository.GetLoanRequests();
        }

        public List<LoanRequest> GetUnsolvedLoanRequests()
        {
            return _loanRequestRepository.GetUnsolvedLoanRequests();
        }
    }
}
