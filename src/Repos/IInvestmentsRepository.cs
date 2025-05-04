using System.Collections.Generic;
using Src.Model;

namespace Src.Repos
{
    public interface IInvestmentsRepository
    {
        public List<Investment> GetInvestmentsHistory();

        public void AddInvestment(Investment investment);

        public void UpdateInvestment(int investmentId, string investorCNP, float amountReturned);
    }
}
