using Microsoft.UI.Xaml.Controls;
using src.Data;
using src.Repos;
using src.Services;
using src.Model;
using src.View.Components;
using System.Collections.Generic;
using System;

namespace src.Views
{
    public sealed partial class LoansView : Page
    {
        private readonly LoanServices _service;
        private readonly LoanCheckerService _loanCheckerService;

        public LoansView()
        {
            this.InitializeComponent();

            DatabaseConnection dbConn = new DatabaseConnection();
            LoanRepository loanRepository = new LoanRepository(dbConn);
            _service = new LoanServices(loanRepository);

            _loanCheckerService = new LoanCheckerService(_service);
            _loanCheckerService.LoansUpdated += OnLoansUpdated;
            _loanCheckerService.Start(); // Start periodic checking

            LoadLoans();
        }

        private void OnLoansUpdated(object sender, EventArgs e)
        {
            LoadLoans(); // Refresh the list instantly when a request is solved
        }

        private void LoadLoans()
        {
            LoansContainer.Items.Clear(); // Clear previous items before reloading

            try
            {
                List<Loan> loans = _service.GetLoans();
                foreach (Loan loan in loans)
                {
                    LoanComponent loanComponent = new LoanComponent();
                    loanComponent.SetLoanData(loan.LoanID, loan.UserCNP, loan.LoanAmount, loan.ApplicationDate,
                                              loan.RepaymentDate, loan.InterestRate, loan.NoMonths, loan.MonthlyPaymentAmount,
                                              loan.State, loan.MonthlyPaymentsCompleted, loan.RepaidAmount, loan.Penalty);

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
