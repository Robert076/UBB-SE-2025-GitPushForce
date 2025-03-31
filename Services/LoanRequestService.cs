﻿using Microsoft.TeamFoundation.Build.WebApi;
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

        // this will be called inside the loans service 
        public bool ValidLoanRequest(LoanRequest loanRequest)
        {
            DatabaseConnection dbConn = new DatabaseConnection();
            UserRepository userRepo = new UserRepository(dbConn);

            User user = userRepo.GetUserByCNP(loanRequest.UserCNP);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (loanRequest.Amount > user.Income * 10)
            {
                return false;
            }

            if (user.CreditScore < 300)
            {
                return false;
            }

            if (PastUnpaidLoans(user))
            {
                return false;
            }

            if (ComputeMonthlyDebtAmount(user) / user.Income * 100 > 60)
            {
                return false;
            }

            if (user.RiskScore > 70)
            {
                return false;
            }

            return true;    // the user passed the checks
        }

        public bool SolveLoanRequest(LoanRequest loanRequest)
        {
            if (ValidLoanRequest(loanRequest))
            {
                _loanRequestRepository.SolveLoanRequest(loanRequest.RequestID);
                return true;
            }
            _loanRequestRepository.DeleteLoanRequest(loanRequest.RequestID);
            return false;
        }

        public bool PastUnpaidLoans(User user)
        {
            DatabaseConnection dbConn = new DatabaseConnection();
            LoanServices loanService = new LoanServices(new LoanRepository(dbConn));

            List<Loan> loans;
            try
            {
                loans = loanService.GetUserLoans(user.CNP);
            }
            catch (Exception exception)
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

        public float ComputeMonthlyDebtAmount(User user)
        {
            DatabaseConnection dbConn = new DatabaseConnection();
            LoanServices loanServices = new LoanServices(new LoanRepository(dbConn));

            List<Loan> loans;
            try
            {
                loans = loanServices.GetUserLoans(user.CNP);
            }
            catch (Exception exception)
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
