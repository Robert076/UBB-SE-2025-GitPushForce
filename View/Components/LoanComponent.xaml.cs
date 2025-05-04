using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Src.Services;

namespace Src.View.Components
{
    public sealed partial class LoanComponent : Page
    {
        private readonly ILoanService loanServices;
        public event EventHandler LoanUpdated;

        private int loanID;
        private string userCNP;
        private float loanAmount;
        private DateTime applicationDate;
        private DateTime repaymentDate;
        private float interestRate;
        private int numberOfMonths;
        private float monthlyPaymentAmount;
        private string status;
        private int monthlyPaymentsCompleted;
        private float repaidAmount;
        private float penalty;

        public LoanComponent(ILoanService loanServices)
        {
            this.loanServices = loanServices;
            this.InitializeComponent();
        }

        public void SetLoanData(int loanID, string userCNP, float loanAmount, DateTime applicationDate,
                                DateTime repaymentDate, float interestRate, int noMonths, float monthlyPaymentAmount,
                                string state, int monthlyPaymentsCompleted, float repaidAmount, float penalty)
        {
            this.loanID = loanID;
            this.userCNP = userCNP;
            this.loanAmount = loanAmount;
            this.applicationDate = applicationDate;
            this.repaymentDate = repaymentDate;
            this.interestRate = interestRate;
            this.numberOfMonths = noMonths;
            this.monthlyPaymentAmount = monthlyPaymentAmount;
            this.status = state;
            this.monthlyPaymentsCompleted = monthlyPaymentsCompleted;
            this.repaidAmount = repaidAmount;
            this.penalty = penalty;

            LoanIDTextBlock.Text = $"Loan ID: {loanID}";
            UserCNPTextBlock.Text = $"User CNP: {userCNP}";
            LoanAmountTextBlock.Text = $"Amount: {loanAmount}";
            ApplicationDateTextBlock.Text = $"Applied: {applicationDate:yyyy-MM-dd}";
            RepaymentDateTextBlock.Text = $"Repay By: {repaymentDate:yyyy-MM-dd}";
            InterestRateTextBlock.Text = $"Interest: {interestRate}%";
            NoMonthsTextBlock.Text = $"Duration: {noMonths} months";
            MonthlyPaymentAmountTextBlock.Text = $"Monthly Payment: {monthlyPaymentAmount}";
            StateTextBlock.Text = $"State: {state}";
            MonthlyPaymentsCompletedTextBlock.Text = $"Payments Done: {monthlyPaymentsCompleted}";
            RepaidAmountTextBlock.Text = $"Repaid: {repaidAmount}";
            PenaltyTextBlock.Text = $"Penalty: {penalty}";
        }

        private async void OnSolveClick(object sender, RoutedEventArgs e)
        {
            loanServices.IncrementMonthlyPaymentsCompleted(loanID, penalty);
            LoanUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
