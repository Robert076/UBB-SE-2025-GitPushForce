using src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Repos
{
    public interface IHistoryRepository
    {
        public List<CreditScoreHistory> GetHistoryForUser(string userCNP);

    }
}
