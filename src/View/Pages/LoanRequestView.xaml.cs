using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;
using Src.Services;
using Src.Model;
using Src.View.Components;

namespace Src.Views
{
    public sealed partial class LoanRequestView : Page
    {
        private readonly ILoanRequestService service;
        private readonly Func<LoanRequestComponent> componentFactory;

        public LoanRequestView(ILoanRequestService loanRequestService, Func<LoanRequestComponent> componentFactory)
        {
            this.InitializeComponent();
            service = loanRequestService;
            this.componentFactory = componentFactory;
            LoadLoanRequests();
        }

        private void LoadLoanRequests()
        {
            LoanRequestContainer.Items.Clear();

            try
            {
                List<LoanRequest> loanRequests = service.GetUnsolvedLoanRequests();

                if (loanRequests.Count == 0)
                {
                    LoanRequestContainer.Items.Add("There are no loan requests that need solving.");
                    return;
                }

                foreach (var request in loanRequests)
                {
                    LoanRequestComponent requestComponent = componentFactory();
                    requestComponent.SetRequestData(
                        request.Id,
                        request.UserCnp,
                        request.Amount,
                        request.ApplicationDate,
                        request.RepaymentDate,
                        request.Status,
                        service.GiveSuggestion(request));

                    requestComponent.LoanRequestSolved += OnLoanRequestSolved;

                    LoanRequestContainer.Items.Add(requestComponent);
                }
            }
            catch (Exception ex)
            {
                LoanRequestContainer.Items.Add($"Error loading loan requests: {ex.Message}");
            }
        }

        private void OnLoanRequestSolved(object sender, EventArgs e)
        {
            LoadLoanRequests();
        }
    }
}
