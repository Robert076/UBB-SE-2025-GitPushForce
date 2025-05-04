using System.Collections.Generic;
using Src.Model;

namespace Src.Services
{
    public interface IInvestmentsService
    {
        public void CalculateAndUpdateRiskScore();
        public void CalculateAndUpdateROI();
        public void CreditScoreUpdateInvestmentsBased();
        public List<InvestmentPortfolio> GetPortfolioSummary();
    }
}
