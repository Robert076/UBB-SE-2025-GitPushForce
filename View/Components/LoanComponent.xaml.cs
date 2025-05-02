using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using src.Services;
using System;

namespace src.View.Components
{
    public sealed partial class LoanComponent : Page
    {
        private readonly ILoanService _loanServices;
        public event EventHandler LoanUpdated;

        private int _loanID;
        private string _userCNP;
        private float _loanAmount;
        private DateTime _applicationDate;
        private DateTime _repaymentDate;
        private float _interestRate;
        private int _noMonths;
        private float _monthlyPaymentAmount;
        private string _state;
        private int _monthlyPaymentsCompleted;
        private float _repaidAmount;
        private float _penalty;

        public LoanComponent(ILoanService loanServices)
        {
            _loanServices = loanServices;
            this.InitializeComponent();
        }

        public void SetLoanData(int loanID, string userCNP, float loanAmount, DateTime applicationDate,
                                DateTime repaymentDate, float interestRate, int noMonths, float monthlyPaymentAmount,
                                string state, int monthlyPaymentsCompleted, float repaidAmount, float penalty)
        {
            _loanID = loanID;
            _userCNP = userCNP;
            _loanAmount = loanAmount;
            _applicationDate = applicationDate;
            _repaymentDate = repaymentDate;
            _interestRate = interestRate;
            _noMonths = noMonths;
            _monthlyPaymentAmount = monthlyPaymentAmount;
            _state = state;
            _monthlyPaymentsCompleted = monthlyPaymentsCompleted;
            _repaidAmount = repaidAmount;
            _penalty = penalty;

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
            _loanServices.incrementMonthlyPaymentsCompleted(_loanID, _penalty);
            LoanUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
