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
    public sealed partial class LoanRequestView : Page
    {
        public LoanRequestView()
        {
            this.InitializeComponent();
            LoadLoanRequests();
        }

        private void LoadLoanRequests()
        {
            LoanRequestContainer.Items.Clear(); // Clear previous items before reloading

            DatabaseConnection dbConn = new DatabaseConnection();
            LoanRequestRepository repo = new LoanRequestRepository(dbConn);
            LoanRequestService service = new LoanRequestService(repo);

            try
            {
                List<LoanRequest> loanRequests = service.GetLoanRequests();

                foreach (var request in loanRequests)
                {
                    LoanRequestComponent requestComponent = new LoanRequestComponent();
                    requestComponent.SetRequestData(request.RequestID, request.UserCNP, request.Amount);

                    // Subscribe to the event to refresh when a request is solved
                    //requestComponent.LoanSolved += OnLoanSolved;

                    LoanRequestContainer.Items.Add(requestComponent);
                }
            }
            catch (Exception)
            {
                LoanRequestContainer.Items.Add("There are no loan requests that need solving.");
            }
        }
    }
}
