using System;

namespace src.Services
{
    public interface ILoanCheckerService
    {
        public event EventHandler LoansUpdated;

        public void Start();
        public void Stop();
    }
}
