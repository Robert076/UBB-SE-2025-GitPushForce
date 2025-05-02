using src.Model;
using System.Collections.Generic;

namespace src.Services
{
    public interface IInvestmentsService
    {
        public void CalculateAndUpdateRiskScore();
        public void CalculateAndUpdateROI();
        public void CreditScoreUpdateInvestmentsBased();
        public List<InvestmentPortfolio> GetPortfolioSummary();
    }
}
