using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Src.Model;
using Src.Services;

namespace Src.View.Components
{
    public sealed partial class LoanRequestComponent : Page
    {
        private readonly ILoanRequestService loanRequestService;
        private readonly ILoanService loanServices;

        public event EventHandler LoanRequestSolved;

        public int RequestID { get; set; }
        public string RequestingUserCNP { get; set; }
        public float RequestedAmount { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime RepaymentDate { get; set; }
        public string State { get; set; }
        public string Suggestion { get; set; }

        public LoanRequestComponent(ILoanRequestService loanRequestService, ILoanService loanService)
        {
            this.loanRequestService = loanRequestService;
            loanServices = loanService;
            this.InitializeComponent();
        }

        private async void OnDenyClick(object sender, RoutedEventArgs e)
        {
            LoanRequest loanRequest = new LoanRequest(RequestID, RequestingUserCNP, RequestedAmount, ApplicationDate, RepaymentDate, State);
            loanRequestService.DenyLoanRequest(loanRequest);
            LoanRequestSolved?.Invoke(this, EventArgs.Empty);
        }

        private async void OnApproveClick(object sender, RoutedEventArgs e)
        {
            LoanRequest loanRequest = new LoanRequest(RequestID, RequestingUserCNP, RequestedAmount, ApplicationDate, RepaymentDate, State);
            loanServices.AddLoan(loanRequest);
            loanRequestService.SolveLoanRequest(loanRequest);
            LoanRequestSolved?.Invoke(this, EventArgs.Empty);
        }

        public void SetRequestData(int id, string requestingUserCnp, float requestedAmount, DateTime applicationDate, DateTime repaymentDate, string state, string suggestion)
        {
            RequestID = id;
            RequestingUserCNP = requestingUserCnp;
            RequestedAmount = requestedAmount;
            ApplicationDate = applicationDate;
            RepaymentDate = repaymentDate;
            State = state;
            Suggestion = suggestion;

            IdTextBlock.Text = $"ID: {id}";
            RequestingUserCNPTextBlock.Text = $"User CNP: {requestingUserCnp}";
            RequestedAmountTextBlock.Text = $"Amount: {requestedAmount}";
            ApplicationDateTextBlock.Text = $"Application Date: {applicationDate:yyyy-MM-dd}";
            RepaymentDateTextBlock.Text = $"Repayment Date: {repaymentDate:yyyy-MM-dd}";
            SuggestionTextBlock.Text = $"{suggestion}";
        }
    }
}
