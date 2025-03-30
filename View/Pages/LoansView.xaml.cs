 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using src.Data;
using src.Repos;
using src.Services;
using src.Model;
using src.View.Components;

namespace src.Views
{
    public sealed partial class LoansView : Page
    {
        public LoansView()
        {
            this.InitializeComponent();
            LoadLoans();
        }

        private void LoadLoans()
        {
            LoansContainer.Items.Clear(); // Clear previous items before reloading

            DatabaseConnection dbConn = new DatabaseConnection();
            LoanRepository loanRepository = new LoanRepository(dbConn);
            LoanServices service = new LoanServices(loanRepository);

            try
            {
                List<Loan> loans = service.GetLoans();
                foreach (Loan loan in loans)
                {
                    LoanComponent loanComponent = new LoanComponent();
                    loanComponent.SetLoanData(loan.LoanID, loan.UserCNP, loan.LoanAmount, loan.ApplicationDate,
                                              loan.RepaymentDate, loan.InterestRate, loan.NoMonths, loan.MonthlyPaymentAmount,
                                              loan.State, loan.MonthlyPaymentsCompleted, loan.RepaidAmount, loan.Penalty);
                    

                    
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
