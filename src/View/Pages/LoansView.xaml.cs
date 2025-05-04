using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;
using Src.Model;
using Src.Services;
using Src.View.Components;

namespace Src.Views
{
    public sealed partial class LoansView : Page
    {
        private readonly ILoanService service;
        private readonly ILoanCheckerService loanCheckerService;
        private readonly Func<LoanComponent> componentFactory;

        public LoansView(ILoanService loanService, ILoanCheckerService loanCheckerService, Func<LoanComponent> componentFactory)
        {
            this.InitializeComponent();

            service = loanService;
            this.loanCheckerService = loanCheckerService;
            this.componentFactory = componentFactory;

            this.loanCheckerService = new LoanCheckerService(service);
            this.loanCheckerService.LoansUpdated += OnLoansUpdated;
            this.loanCheckerService.Start();

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
                List<Loan> loans = service.GetLoans();
                foreach (Loan loan in loans)
                {
                    LoanComponent loanComponent = componentFactory();
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
