using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Src.Data;
using Src.Model;
using Src.Repos;
using Src.Services;

namespace Src.ViewModel
{
    public class LoanRequestViewModel
    {
        private readonly LoanRequestService loanRequestService;

        public ObservableCollection<LoanRequest> LoanRequests { get; set; }

        public LoanRequestViewModel()
        {
            loanRequestService = new LoanRequestService(new LoanRequestRepository(new DatabaseConnection()));
            LoanRequests = new ObservableCollection<LoanRequest>();
        }

        public async Task LoadLoanRequests()
        {
            try
            {
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
