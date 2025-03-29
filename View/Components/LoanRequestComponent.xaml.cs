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
        public event EventHandler LoanSolved;

        public string RequestingUserCNP { get; set; }

        public float RequestedAmount { get; set; }
        public int RequestID { get; set; }

        public LoanRequestComponent()
        {
            this.InitializeComponent();
            _loanRequestService = new LoanRequestService(new LoanRequestRepository(new DatabaseConnection()));
        }

        private async void OnSolveClick(object sender, RoutedEventArgs e)
        {
            //var chatReport = new ChatReport
            //{
            //    Id = ReportId,
            //    ReportedUserCNP = ReportedUserCNP,
            //    ReportedMessage = ReportedMessage
            //};

            //await _chatReportService.SolveChatReport(chatReport);
            //ReportSolved?.Invoke(this, EventArgs.Empty);
        }

        public void SetRequestData(int id, string requestingUserCnp, float requestedAmount)
        {
            RequestID = id;
            RequestingUserCNP = requestingUserCnp; 
            RequestedAmount = requestedAmount; 

            IdTextBlock.Text = $"Loan Request ID: {id}";
            RequestingUserCNPTextBlock.Text = $"Requesting user's CNP: {requestingUserCnp}";
            RequestedAmountTextBlock.Text = $"RequestedAmount: {requestedAmount}";
        }
    }
}
