using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;
using src.Services;
using src.Model;
using src.View.Components;

namespace src.Views
{
    public sealed partial class LoanRequestView : Page
    {
        private readonly ILoanRequestService _service;
        private readonly Func<LoanRequestComponent> _componentFactory;

        public LoanRequestView(ILoanRequestService loanRequestService, Func<LoanRequestComponent> componentFactory)
        {
            this.InitializeComponent();
            _service = loanRequestService;
            _componentFactory = componentFactory;
            LoadLoanRequests();
        }

        private void LoadLoanRequests()
        {
            LoanRequestContainer.Items.Clear();

            try
            {
                List<LoanRequest> loanRequests = _service.GetUnsolvedLoanRequests();

                if (loanRequests.Count == 0)
                {
                    LoanRequestContainer.Items.Add("There are no loan requests that need solving.");
                    return;
                }

                foreach (var request in loanRequests)
                {
                    LoanRequestComponent requestComponent = _componentFactory();
                    requestComponent.SetRequestData(
                        request.Id,
                        request.UserCnp,
                        request.Amount,
                        request.ApplicationDate,
                        request.RepaymentDate,
                        request.Status,
                        _service.GiveSuggestion(request)
                    );

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
