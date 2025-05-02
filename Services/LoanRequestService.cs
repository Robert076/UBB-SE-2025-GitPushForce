using src.Data;
using src.Model;
using src.Repos;
using System;
using System.Collections.Generic;

namespace src.Services
{
    class LoanRequestService : ILoanRequestService
    {
        ILoanRequestRepository _loanRequestRepository;

        public LoanRequestService(ILoanRequestRepository loanRequestRepository)
        {
            _loanRequestRepository = loanRequestRepository;
        }

        public string GiveSuggestion(LoanRequest loanRequest)
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            UserRepository userRepository = new UserRepository(dbConnection);
            LoanService loanService = new LoanService(new LoanRepository(dbConnection));

            User user = userRepository.GetUserByCnp(loanRequest.UserCnp);

            string givenSuggestion = string.Empty;

            if (loanRequest.Amount > user.Income * 10)
            {
                givenSuggestion = "Amount requested is too high for user income";
            }

            if (user.CreditScore < 300)
            {
                if (givenSuggestion.Length > 0)
                {
                    givenSuggestion += ", ";
                }
                givenSuggestion += "Credit score is too low";
            }

            //if (PastUnpaidLoans(user, loanService))
            //if (ComputeMonthlyDebtAmount(user, loanService) / user.Income * 100 > 60)

            if (user.RiskScore > 70)
            {
                if (givenSuggestion.Length > 0)
                {
                    givenSuggestion += ", ";
                }
                givenSuggestion += "User risk score is too high";
            }

            if (givenSuggestion.Length > 0)
            {
                givenSuggestion = "User does not qualify for loan: " + givenSuggestion;
            }

            return givenSuggestion;
        }

        public void SolveLoanRequest(LoanRequest loanRequest)
        {
            _loanRequestRepository.SolveLoanRequest(loanRequest.Id);
        }

        public void DenyLoanRequest(LoanRequest loanRequest)
        {
            _loanRequestRepository.DeleteLoanRequest(loanRequest.Id); //TODO: double check, it was requestId
        }

        public bool PastUnpaidLoans(User user, LoanService loanService)
        {
            List<Loan> userLoanList;
            try
            {
                userLoanList = loanService.GetUserLoans(user.Cnp);
            }
            catch (Exception)
            {
                userLoanList = new List<Loan>();
            }
            foreach (Loan loan in userLoanList)
            {
                if (loan.Status == "Active" && loan.RepaymentDate < DateTime.Today)
                {
                    return true;
                }
            }

            return false;
        }

        public float ComputeMonthlyDebtAmount(User user, LoanService loanServices)
        {
            List<Loan> loanList;
            try
            {
                loanList = loanServices.GetUserLoans(user.Cnp);
            }
            catch (Exception)
            {
                loanList = new List<Loan>();
            }
            float monthlyDebtAmount = 0;

            foreach (Loan loan in loanList)
            {
                if (loan.Status == "Active")
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
