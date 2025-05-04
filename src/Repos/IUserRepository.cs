using System;
using System.Collections.Generic;
using Src.Model;

namespace Src.Repos
{
    public interface IUserRepository
    {
        public int CreateUser(User user);

        public User? GetUserByCnp(string cNP);

        public void PenalizeUser(string cNP, int amountToBePenalizedWith);

        public void IncrementOffensesCount(string cNP);

        public void UpdateUserCreditScore(string cNP, int creditScore);

        public void UpdateUserROI(string cNP, decimal rOI);

        public void UpdateUserRiskScore(string cNP, int riskScore);

        public List<User> GetUsers();
    }
}
