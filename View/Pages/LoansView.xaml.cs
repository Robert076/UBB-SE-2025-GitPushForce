using Microsoft.UI.Xaml.Controls;
using src.Services;
using src.Model;
using src.View.Components;
using System.Collections.Generic;
using System;

namespace src.Views
{
    public sealed partial class LoansView : Page
    {
        private readonly ILoanService _service;
        private readonly ILoanCheckerService _loanCheckerService;
        private readonly Func<LoanComponent> _componentFactory;

        public LoansView(ILoanService loanService, ILoanCheckerService loanCheckerService, Func<LoanComponent> componentFactory)
        {
            this.InitializeComponent();

            _service = loanService;
            _loanCheckerService = loanCheckerService;
            _componentFactory = componentFactory;

            _loanCheckerService = new LoanCheckerService(_service);
            _loanCheckerService.LoansUpdated += OnLoansUpdated;
            _loanCheckerService.Start();

            LoadLoans();
        }

        private void OnLoansUpdated(object sender, EventArgs e)
        {
            LoadLoans();
        }

        private void LoadLoans()
        {
            LoansContainer.Items.Clear();

            try
            {
                List<Loan> loans = _service.GetLoans();
                foreach (Loan loan in loans)
                {
                    LoanComponent loanComponent = _componentFactory();
                    loanComponent.SetLoanData(loan.Id, loan.UserCnp, loan.LoanAmount, loan.ApplicationDate,
                                              loan.RepaymentDate, loan.InterestRate, loan.NumberOfMonths, loan.MonthlyPaymentAmount,
                                              loan.Status, loan.MonthlyPaymentsCompleted, loan.RepaidAmount, loan.Penalty);

                    loanComponent.LoanUpdated += OnLoansUpdated;

                    LoansContainer.Items.Add(loanComponent);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error - LoadLoans: {exception.Message}");
            }
        }
    }
}
