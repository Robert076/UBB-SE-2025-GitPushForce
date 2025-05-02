using System.Collections.ObjectModel;
using System.Threading.Tasks;
using src.Services;
using src.Model;
using src.Data;
using src.Repos;
using System;

namespace src.ViewModel
{
    class LoanRequestViewModel
    {
        private readonly LoanRequestService _loanRequestService;

        public ObservableCollection<LoanRequest> LoanRequests { get; set; }

        public LoanRequestViewModel()
        {
            _loanRequestService = new LoanRequestService(new LoanRequestRepository(new DatabaseConnection()));
            LoanRequests = new ObservableCollection<LoanRequest>();
        }

        public async Task LoadLoanRequests()
        {
            try
            {
                var requests = _loanRequestService.GetLoanRequests();
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
