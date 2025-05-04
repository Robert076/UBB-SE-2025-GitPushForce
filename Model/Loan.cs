using System;

namespace Src.Model
{
    public class Loan
    {
        public int Id { get; set; }
        public string UserCnp { get; set; }
        public float LoanAmount { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime RepaymentDate { get; set; }
        public float InterestRate { get; set; }
        public int NumberOfMonths { get; set; }
        public float MonthlyPaymentAmount { get; set; }
        public string Status { get; set; }
        public int MonthlyPaymentsCompleted { get; set; }
        public float RepaidAmount { get; set; }
        public float Penalty { get; set; }

        public Loan(int loanID, string userCnp, float loanAmount, DateTime applicationDate, DateTime repaymentDate, float interestRate, int numberOfMonths, float monthlyPaymentAmount, int monthlyPaymentsCompleted, float repaidAmount, float penalty, string status)
        {
            Id = loanID;
            UserCnp = userCnp;
            LoanAmount = loanAmount;
            ApplicationDate = applicationDate;
            RepaymentDate = repaymentDate;
            InterestRate = interestRate;
            NumberOfMonths = numberOfMonths;
            MonthlyPaymentAmount = monthlyPaymentAmount;
            Status = status;
            MonthlyPaymentsCompleted = monthlyPaymentsCompleted;
            RepaidAmount = repaidAmount;
            Penalty = penalty;
        }
    }
}

