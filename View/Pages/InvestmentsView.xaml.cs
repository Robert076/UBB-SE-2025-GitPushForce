using System;
using Azure;
using src.Data;
using src.Repos;
using src.Services;
using src.ViewModel;

namespace src.Views
{
    public sealed partial class InvestmentsView : Page
    {
        private InvestmentsViewModel _viewModel;

        public InvestmentsViewModel DataContext { get; private set; }

        public InvestmentsView()
        {
            this.InitializeComponent();
            InitializeViewModel();
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        private void InitializeViewModel()
        {
            DatabaseConnection dbConn = new DatabaseConnection();
            InvestmentsRepository repo = new InvestmentsRepository(dbConn);
            InvestmentsService service = new InvestmentsService(new UserRepository(dbConn), repo);
            _viewModel = new InvestmentsViewModel(service);
            this.DataContext = _viewModel;

            // Load portfolio summary for a specific user
            _viewModel.LoadPortfolioSummary("UserCNP");
        }
    }
}
