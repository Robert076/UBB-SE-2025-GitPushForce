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
            float monthlyPaymentAmount = (float)loanRequest.Amount * interestRate / noMonths;

            Loan loan =  new Loan(
                loanRequest.RequestID,
                loanRequest.UserCNP,
                loanRequest.Amount,
                loanRequest.ApplicationDate,
                loanRequest.RepaymentDate,
                interestRate,
                noMonths,
                monthlyPaymentAmount
            );

            _loanRepository.AddLoan(loan);
        }
    }
}
