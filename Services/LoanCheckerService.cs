using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Services
{
    class LoanCheckerService
    {
        private readonly LoanServices _loanServices;
        private readonly DispatcherTimer _timer;

        public event EventHandler LoansUpdated;

        public LoanCheckerService(LoanServices loanServices)
        {
            _loanServices = loanServices;
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1) // set to 1 second for testing purposes, should be higher since this checks for monthly payments
            };
            _timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, object e)
        {
            _loanServices.CheckLoans();
            LoansUpdated?.Invoke(this, EventArgs.Empty);
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
