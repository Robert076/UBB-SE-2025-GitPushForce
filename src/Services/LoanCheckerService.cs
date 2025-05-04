using System;
using Microsoft.UI.Xaml;

namespace Src.Services
{
    public class LoanCheckerService : ILoanCheckerService
    {
        private readonly ILoanService loanServices;
        private readonly DispatcherTimer timer;

        public event EventHandler LoansUpdated;

        public LoanCheckerService(ILoanService loanServices)
        {
            this.loanServices = loanServices;
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1) // set to 1 second for testing purposes, should be higher since this checks for monthly payments
            };
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, object e)
        {
            loanServices.CheckLoans();
            LoansUpdated?.Invoke(this, EventArgs.Empty);
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }
    }
}
