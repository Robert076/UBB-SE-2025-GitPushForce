using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using src.Services;
using src.Model;
using src.Data;
using src.Repos;
using System;

namespace src.View.Components
{
    public sealed partial class LoanRequestComponent : Page
    {
        private readonly LoanRequestService _loanRequestService;
        private readonly LoanServices _loanServices;
        public event EventHandler LoanRequestSolved;

        // Properties
        public int RequestID { get; set; }
        public string RequestingUserCNP { get; set; }
        public float RequestedAmount { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime RepaymentDate { get; set; }
        public string State { get; set; }

        public LoanRequestComponent()
        {
            this.InitializeComponent();
            _loanRequestService = new LoanRequestService(new LoanRequestRepository(new DatabaseConnection()));
            _loanServices = new LoanServices(new LoanRepository(new DatabaseConnection()));
        }

        private async void OnSolveClick(object sender, RoutedEventArgs e)
        {
            var loanRequest = new LoanRequest(RequestID, RequestingUserCNP, RequestedAmount, ApplicationDate, RepaymentDate, State);

            if (_loanRequestService.SolveLoanRequest(loanRequest))
            {
                _loanServices.AddLoan(loanRequest);
            }
            else
            {
                // Create and show the ContentDialog
                ContentDialog failureDialog = new ContentDialog
                {
                    Title = "Loan Request Failed",
                    Content = "User does not meet loan approval requirements. Deleting request...",
                    CloseButtonText = "OK",
                    XamlRoot = this.Content.XamlRoot
                };

                // Show the dialog asynchronously
                failureDialog.ShowAsync();
            }
            LoanRequestSolved?.Invoke(this, EventArgs.Empty);
        }


        public void SetRequestData(int id, string requestingUserCnp, float requestedAmount, DateTime applicationDate, DateTime repaymentDate, string state)
        {
            RequestID = id;
            RequestingUserCNP = requestingUserCnp;
            RequestedAmount = requestedAmount;
            ApplicationDate = applicationDate;
            RepaymentDate = repaymentDate;
            State = state;


            // Update UI elements
            IdTextBlock.Text = $"ID: {id}";
            RequestingUserCNPTextBlock.Text = $"User CNP: {requestingUserCnp}";
            RequestedAmountTextBlock.Text = $"Amount: {requestedAmount}";
            ApplicationDateTextBlock.Text = $"Application Date: {applicationDate:yyyy-MM-dd}";
            RepaymentDateTextBlock.Text = $"Repayment Date: {repaymentDate:yyyy-MM-dd}";
        }
    }
}
