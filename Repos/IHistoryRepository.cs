using System.Collections.Generic;
using Src.Model;

namespace Src.Repos
{
    public interface IHistoryRepository
    {
        public List<CreditScoreHistory> GetHistoryForUser(string userCNP);
    }
}
