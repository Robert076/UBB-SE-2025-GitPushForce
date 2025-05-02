using src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Repos
{
    public interface IInvestmentsRepository
    {
        public List<Investment> GetInvestmentsHistory();

        public void AddInvestment(Investment investment);

        public void UpdateInvestment(int investmentId, string investorCNP, float amountReturned);

    }
}
