using System;
using System.Collections.Generic;
using Src.Data;
using Src.Model;
using Src.Repos;

namespace Src.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository loanRepository;
        private readonly IUserRepository userRepository;
        public LoanService(ILoanRepository loanRepository,IUserRepository userRepository)
        {
            this.loanRepository = loanRepository;
            this.userRepository = userRepository;
        }

        public List<Loan> GetLoans()
        {
            return loanRepository.GetLoans();
        }

        public List<Loan> GetUserLoans(string userCNP)
        {
            return loanRepository.GetUserLoans(userCNP);
        }

        public void AddLoan(LoanRequest loanRequest)
        {
           
           

            User user = userRepository.GetUserByCnp(loanRequest.UserCnp);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            float interestRate = (float)user.RiskScore / user.CreditScore * 100;
            int noMonths = ((loanRequest.RepaymentDate.Year - loanRequest.ApplicationDate.Year) * 12) + loanRequest.RepaymentDate.Month - loanRequest.ApplicationDate.Month;
            float monthlyPaymentAmount = (float)loanRequest.Amount * (1 + (interestRate / 100)) / noMonths;
            int monthlyPaymentsCompleted = 0;
            int repaidAmount = 0;
            float penalty = 0;

            Loan loan = new Loan(
                loanRequest.Id,
                loanRequest.UserCnp,
                loanRequest.Amount,
                loanRequest.ApplicationDate,
                loanRequest.RepaymentDate,
                interestRate,
                noMonths,
                monthlyPaymentAmount,
                monthlyPaymentsCompleted,
                repaidAmount,
                penalty,
                "active");

            loanRepository.AddLoan(loan);
        }

        public void CheckLoans()
        {
            List<Loan> loanList = loanRepository.GetLoans();
            foreach (Loan loan in loanList)
            {
                int numberOfMonthsPassed = ((DateTime.Today.Year - loan.ApplicationDate.Year) * 12) + DateTime.Today.Month - loan.ApplicationDate.Month;
                User user = this.userRepository.GetUserByCnp(loan.UserCnp);
                if (loan.MonthlyPaymentsCompleted >= loan.NumberOfMonths)
                {
                    loan.Status = "completed";
                    int newUserCreditScore = ComputeNewCreditScore(user, loan);
                    userRepository.UpdateUserCreditScore(loan.UserCnp, newUserCreditScore);
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
                if (DateTime.Today > loan.RepaymentDate && loan.Status == "active")
                {
                    loan.Status = "overdue";
                    int newUserCreditScore = ComputeNewCreditScore(user, loan);

                    userRepository.UpdateUserCreditScore(loan.UserCnp, newUserCreditScore);
                    UpdateHistoryForUser(loan.UserCnp, newUserCreditScore);
                }
                else if (loan.Status == "overdue")
                {
                    if (loan.MonthlyPaymentsCompleted >= loan.NumberOfMonths)
                    {
                        loan.Status = "completed";
                        int newUserCreditScore = ComputeNewCreditScore(user, loan);

                        userRepository.UpdateUserCreditScore(loan.UserCnp, newUserCreditScore);
                        UpdateHistoryForUser(loan.UserCnp, newUserCreditScore);
                    }
                }
                if (loan.Status == "completed")
                {
                    loanRepository.DeleteLoan(loan.Id);
                }
                else
                {
                    loanRepository.UpdateLoan(loan);
                }
            }
        }

        public int ComputeNewCreditScore(User user, Loan loan)
        {
            int totalDaysInAdvance = (loan.RepaymentDate - DateTime.Today).Days;
            if (totalDaysInAdvance > 30)
            {
                totalDaysInAdvance = 30;
            }
            else if (totalDaysInAdvance < -100)
            {
                totalDaysInAdvance = -100;
            }
            int newUserCreditScore = user.CreditScore + ((int)loan.LoanAmount * 10 / user.Income) + totalDaysInAdvance;
            newUserCreditScore = Math.Min(newUserCreditScore, 700);
            newUserCreditScore = Math.Max(newUserCreditScore, 100);

            return newUserCreditScore;
        }

        public void UpdateHistoryForUser(string userCNP, int newScore)
        {
            loanRepository.UpdateCreditScoreHistoryForUser(userCNP, newScore);
        }

        public void IncrementMonthlyPaymentsCompleted(int loanID, float penalty)
        {
            Loan loan = loanRepository.GetLoanById(loanID);
            loan.MonthlyPaymentsCompleted++;
            loan.RepaidAmount += loan.MonthlyPaymentAmount + penalty;
            loanRepository.UpdateLoan(loan);
        }
    }
}
