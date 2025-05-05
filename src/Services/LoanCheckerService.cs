using Microsoft.UI.Xaml;
using System;
using Src.Helpers;  // Add this to bring in your custom ITimer interface

namespace Src.Services
{
    public class LoanCheckerService : ILoanCheckerService
    {
        private readonly ILoanService loanServices;
        private readonly IDispatchTimer timer;  // Use the custom ITimer from Src.Services

        public event EventHandler LoansUpdated;

        public LoanCheckerService(ILoanService loanServices, IDispatchTimer timer = null)
        {
            this.loanServices = loanServices;
            this.timer = timer ?? new DispatcherTimerWrapper();  // Default to real timer
            this.timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            loanServices.CheckLoans();
            LoansUpdated?.Invoke(this, EventArgs.Empty);
        }

        public void Start() => timer.Start();
        public void Stop() => timer.Stop();
    }
}
