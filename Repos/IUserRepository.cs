using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Model;


namespace src.Repos
{
    public interface IUserRepository
    {
        public int CreateUser(User user);

        public User? GetUserByCnp(string CNP);

        public void PenalizeUser(string CNP, Int32 amountToBePenalizedWith);

        public void IncrementOffensesCount(string CNP);

        public void UpdateUserCreditScore(string CNP, int creditScore);

        public void UpdateUserROI(string CNP, decimal ROI);

        public void UpdateUserRiskScore(string CNP, int riskScore);

        public List<User> GetUsers();

    }
}
