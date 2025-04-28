using Microsoft.Data.SqlClient;
using src.Data;
using src.Model;
using src.Repos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments;

namespace src.Services
{
    class LoanServices
    {
        LoanRepository _loanRepository;

        public LoanServices(LoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public List<Loan> GetLoans()
        {
            return _loanRepository.GetLoans();
        }

        public List<Loan> GetUserLoans(string userCNP)
        {
            return _loanRepository.GetUserLoans(userCNP);
        }

        public void AddLoan(LoanRequest loanRequest)
        {
            DatabaseConnection dbConn = new DatabaseConnection();
            UserRepository userRepo = new UserRepository(dbConn);

            User user = userRepo.GetUserByCNP(loanRequest.UserCNP);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            float interestRate = (float)user.RiskScore / user.CreditScore * 100;
            int noMonths = (loanRequest.RepaymentDate.Year - loanRequest.ApplicationDate.Year) * 12 + loanRequest.RepaymentDate.Month - loanRequest.ApplicationDate.Month;
            float monthlyPaymentAmount = (float)loanRequest.Amount * (1 + interestRate / 100) / noMonths;
            int monthlyPaymentsCompleted = 0;
            int repaidAmount = 0;
            float penalty = 0;

            Loan loan =  new Loan(
                loanRequest.RequestID,
                loanRequest.UserCNP,
                loanRequest.Amount,
                loanRequest.ApplicationDate,
                loanRequest.RepaymentDate,
                interestRate,
                noMonths,
                monthlyPaymentAmount,
                monthlyPaymentsCompleted,
                repaidAmount,
                penalty,
                "active"
            );

            _loanRepository.AddLoan(loan);
        }

        public void CheckLoans()
        {
            List<Loan> loans = _loanRepository.GetLoans();
            foreach (Loan loan in loans)
            {
                int numberOfMonthsPassed = (DateTime.Today.Year - loan.ApplicationDate.Year) * 12 + DateTime.Today.Month - loan.ApplicationDate.Month;
                User user = new UserRepository(new DatabaseConnection()).GetUserByCNP(loan.UserCNP);
                if (loan.MonthlyPaymentsCompleted >= loan.NoMonths)
                {
                    loan.State = "completed";
                    int newUserCreditScore = ComputeNewCreditScore(user, loan);

                    new UserRepository(new DatabaseConnection()).UpdateUserCreditScore(loan.UserCNP, newUserCreditScore);
                    UpdateHistoryForUser(loan.UserCNP, newUserCreditScore);

                }
                if (numberOfMonthsPassed > loan.MonthlyPaymentsCompleted)
                {
                    int numberOfOverdueDays = (DateTime.Today - loan.ApplicationDate.AddMonths(loan.MonthlyPaymentsCompleted)).Days;
                    float penalty = 0.1f * numberOfOverdueDays;
                    loan.Penalty = penalty;
                }
                else
                { 
                    loan.Penalty = 0;
                }
                if (DateTime.Today > loan.RepaymentDate && loan.State == "active")
                {
                    loan.State = "overdue";
                    int newUserCreditScore = ComputeNewCreditScore(user, loan);

                    new UserRepository(new DatabaseConnection()).UpdateUserCreditScore(loan.UserCNP, newUserCreditScore);
                    UpdateHistoryForUser(loan.UserCNP, newUserCreditScore);
                }
                else if (loan.State == "overdue")
                {
                    if (loan.MonthlyPaymentsCompleted >= loan.NoMonths)
                    {
                        loan.State = "completed";
                        int newUserCreditScore = ComputeNewCreditScore(user, loan);

                        new UserRepository(new DatabaseConnection()).UpdateUserCreditScore(loan.UserCNP, newUserCreditScore);
                        UpdateHistoryForUser(loan.UserCNP, newUserCreditScore);
                    }
                }
                if (loan.State == "completed")
                {
                    _loanRepository.DeleteLoan(loan.LoanID);
                }
                else
                {
                    _loanRepository.UpdateLoan(loan);
                }
            }
        }

        public int ComputeNewCreditScore(User user, Loan loan)
        {
            int totalDaysInAdvance = (loan.RepaymentDate - DateTime.Today).Days;
            if (totalDaysInAdvance > 30)    // 30 days in advance
            {
                totalDaysInAdvance = 30;
            }
            else if (totalDaysInAdvance < -100) // 100 days overdue
            {
                totalDaysInAdvance = -100;
            }
            int newUserCreditScore = user.CreditScore + ((int)loan.LoanAmount * 10 / user.Income) + totalDaysInAdvance;
            newUserCreditScore = Math.Min(newUserCreditScore, 700);     // to ensure the credit score does not exceed 700
            newUserCreditScore = Math.Max(newUserCreditScore, 100);     // to ensure the credit score does not go below 100

            return newUserCreditScore;
        }

        public void UpdateHistoryForUser(string UserCNP, int NewScore)
        {
            _loanRepository.UpdateHistoryForUser(UserCNP, NewScore);
        }

        public void incrementMonthlyPaymentsCompleted(int loanID, float penalty)
        {
            Loan loan = _loanRepository.GetLoanByID(loanID);
            loan.MonthlyPaymentsCompleted++;
            loan.RepaidAmount += loan.MonthlyPaymentAmount + penalty;
            _loanRepository.UpdateLoan(loan);
        }
    }
}
