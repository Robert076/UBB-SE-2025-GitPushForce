using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Src.Model;
using Src.Services;

namespace Src.ViewModel
{
    public class LoanRequestViewModel
    {
        private readonly ILoanRequestService loanRequestService;

        public ObservableCollection<LoanRequest> LoanRequests { get; set; }

        public LoanRequestViewModel(ILoanRequestService loanRequestService)
        {
            this.loanRequestService = loanRequestService ?? throw new ArgumentNullException(nameof(loanRequestService));
            LoanRequests = new ObservableCollection<LoanRequest>();
        }

        public async Task LoadLoanRequests()
        {
            try
            {
                LoanRequests.Clear(); 
                var requests = loanRequestService.GetLoanRequests();
                foreach (var request in requests)
                {
                    LoanRequests.Add(request);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error: {exception.Message}");
            }
        }

    }
}
