using src.Data;
using src.Model;
using src.Repos;
using System;
using System.Collections.Generic;

namespace src.Services
{
    class LoanService : ILoanService
    {
        ILoanRepository _loanRepository;

        public LoanService(ILoanRepository loanRepository)
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
            DatabaseConnection dbConnection = new DatabaseConnection();
            UserRepository userRepository = new UserRepository(dbConnection);

            User user = userRepository.GetUserByCnp(loanRequest.UserCnp);

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
                "active"
            );

            _loanRepository.AddLoan(loan);
        }

        public void CheckLoans()
        {
            List<Loan> loanList = _loanRepository.GetLoans();
            foreach (Loan loan in loanList)
            {
                int numberOfMonthsPassed = (DateTime.Today.Year - loan.ApplicationDate.Year) * 12 + DateTime.Today.Month - loan.ApplicationDate.Month;
                User user = new UserRepository(new DatabaseConnection()).GetUserByCnp(loan.UserCnp);
                if (loan.MonthlyPaymentsCompleted >= loan.NumberOfMonths)
                {
                    loan.Status = "completed";
                    int newUserCreditScore = ComputeNewCreditScore(user, loan);

                    new UserRepository(new DatabaseConnection()).UpdateUserCreditScore(loan.UserCnp, newUserCreditScore);


                    // (loan.UserCnp, newUserCreditScore); TODO: idk what is happenning here
                    //UpdateHistoryForUser(loan.UserCnp, newUserCreditScore); maybe this ???? 

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

                    new UserRepository(new DatabaseConnection()).UpdateUserCreditScore(loan.UserCnp, newUserCreditScore);
                    UpdateHistoryForUser(loan.UserCnp, newUserCreditScore);
                }
                else if (loan.Status == "overdue")
                {
                    if (loan.MonthlyPaymentsCompleted >= loan.NumberOfMonths)
                    {
                        loan.Status = "completed";
                        int newUserCreditScore = ComputeNewCreditScore(user, loan);

                        new UserRepository(new DatabaseConnection()).UpdateUserCreditScore(loan.UserCnp, newUserCreditScore);
                        UpdateHistoryForUser(loan.UserCnp, newUserCreditScore);
                    }
                }
                if (loan.Status == "completed")
                {
                    _loanRepository.DeleteLoan(loan.Id);
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
            _loanRepository.UpdateCreditScoreHistoryForUser(UserCNP, NewScore);
        }

        public void incrementMonthlyPaymentsCompleted(int loanID, float penalty)
        {
            Loan loan = _loanRepository.GetLoanById(loanID);
            loan.MonthlyPaymentsCompleted++;
            loan.RepaidAmount += loan.MonthlyPaymentAmount + penalty;
            _loanRepository.UpdateLoan(loan);
        }
    }
}
