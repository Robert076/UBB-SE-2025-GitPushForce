using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Src.Data;
using Src.Model;
using Src.Repos;

namespace Src.Services
{
    public class InvestmentsService : IInvestmentsService
    {
        private readonly IUserRepository userRepository;
        private readonly IInvestmentsRepository investmentsRepository;

        public InvestmentsService(IUserRepository userRepository, IInvestmentsRepository investmentsRepository)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.investmentsRepository = investmentsRepository ?? throw new ArgumentNullException(nameof(investmentsRepository));
        }

        public void CalculateAndUpdateRiskScore()
        {
            var allExistentUsers = userRepository.GetUsers();

            foreach (var currentUser in allExistentUsers)
            {
                var recentInvestments = GetRecentInvestments(currentUser.Cnp);
                if (recentInvestments != null)
                {
                    var riskScoreChange = CalculateRiskScoreChange(currentUser, recentInvestments);
                    UpdateUserRiskScore(currentUser, riskScoreChange);
                    userRepository.UpdateUserRiskScore(currentUser.Cnp, currentUser.RiskScore);
                }
            }
        }

        private List<Investment> GetRecentInvestments(string cnp)
        {
            var allInvestments = investmentsRepository.GetInvestmentsHistory();

            var latestInvestment = allInvestments
                .Where(i => i.InvestorCnp == cnp)
                .OrderBy(i => i.InvestmentDate)
                .LastOrDefault();

            if (latestInvestment == null)
            {
                return null;
            }

            var latestInvestmentDate = latestInvestment.InvestmentDate;

            return allInvestments
                .Where(i => i.InvestorCnp == cnp)
                .Where(i => i.InvestmentDate >= latestInvestmentDate.AddDays(-7))
                .OrderByDescending(i => i.InvestmentDate)
                .ToList();
        }
        private int CalculateRiskScoreChange(User user, List<Investment> investments)
        {
            int riskScoreChange = 0;

            var profitableTrades = investments.Where(i => i.AmountReturned > i.AmountInvested).Count();

            var totalTrades = investments.Where(i => i.AmountReturned >= 0).Count();
            var lossRate = totalTrades > 0 ? (totalTrades - profitableTrades) / (float)totalTrades : 0;

            var dangerousLossRate = 0.35f;
            if (lossRate > dangerousLossRate)
            {
                // Increase risk score for each new investment until the rate improves
                riskScoreChange += investments.Count * 5;
            }
            else
            {
                // Decrease risk score for each profitable trade
                riskScoreChange -= profitableTrades * 5;
            }

            // Calculate investment frequency impact
            var tradesPerDay = investments.GroupBy(i => i.InvestmentDate.Date).Count();
            var averageTradesPerDay = tradesPerDay / 7f; // Assuming a week

            var lowRiskRate = 2;
            var highRiskRate = 5;
            if (averageTradesPerDay < lowRiskRate)
            {
                riskScoreChange -= 5;
            }
            else if (averageTradesPerDay > highRiskRate)
            {
                riskScoreChange += 5;
            }

            var totalInvested = investments.Sum(i => i.AmountInvested);

            var safeInvestmentThreshold = 0.1f;
            var riskyInvestmentThreshold = 0.3f;
            if (totalInvested / user.Income < safeInvestmentThreshold)
            {
                riskScoreChange -= 5;
            }
            else if (totalInvested / user.Income > riskyInvestmentThreshold)
            {
                riskScoreChange += 5;
            }

            return riskScoreChange;
        }

        private void UpdateUserRiskScore(User user, int riskScoreChange)
        {
            user.RiskScore += riskScoreChange;

            // Ensure risk score stays within the range (1 to 100)
            var minRiskScore = 1;
            var maxRiskScore = 100;
            user.RiskScore = Math.Max(minRiskScore, Math.Min(user.RiskScore, maxRiskScore));
        }
        public void CalculateAndUpdateROI()
        {
            var allExistentUsers = userRepository.GetUsers();

            foreach (var currentUser in allExistentUsers)
            {
                CalculateAndSetUserROI(currentUser);
                userRepository.UpdateUserROI(currentUser.Cnp, currentUser.ROI);
            }
        }

        private void CalculateAndSetUserROI(User user)
        {
            var investmentOpen = -1;

            var allInvestments = investmentsRepository.GetInvestmentsHistory()
                .Where(i => i.InvestorCnp == user.Cnp)
                .Where(i => i.AmountReturned != investmentOpen)
                .ToList();

            if (!allInvestments.Any())
            {
                user.ROI = 1; // Set ROI to 1 if no closed transactions. This means no effect over credit score.
                return;
            }

            var totalProfit = allInvestments.Sum(i => i.AmountReturned);
            var totalInvested = allInvestments.Sum(i => i.AmountInvested);

            if (totalInvested == 0)
            {
                user.ROI = 1;
                return;
            }

            var newUserROI = (decimal)totalProfit / (decimal)totalInvested;
            user.ROI = newUserROI;
        }

        public void CreditScoreUpdateInvestmentsBased()
        {
            var allExistentUsers = userRepository.GetUsers();

            foreach (var currentUser in allExistentUsers)
            {
                var oldCreditScore = currentUser.CreditScore;
                var oldRiskScore = currentUser.RiskScore;
                var oldROI = currentUser.ROI;

                var riskScorePercent = currentUser.RiskScore / 100;
                var creditScoreSubstracted = currentUser.CreditScore * riskScorePercent;
                currentUser.CreditScore -= creditScoreSubstracted;

                if (currentUser.ROI <= 0)
                {
                    currentUser.CreditScore -= 100;
                }
                else if (currentUser.ROI < 1)
                {
                    var decreaseAmount = 10 / currentUser.ROI;
                    currentUser.CreditScore -= (int)decreaseAmount;
                }
                else
                {
                    var increaseAmount = 10 * currentUser.ROI;
                    currentUser.CreditScore += (int)increaseAmount;
                }

                var minCreditScore = 100;
                var maxCreditScore = 700;

                currentUser.CreditScore = Math.Min(maxCreditScore, Math.Max(minCreditScore, currentUser.CreditScore));

                userRepository.UpdateUserCreditScore(currentUser.Cnp, currentUser.CreditScore);
            }
        }
        public List<InvestmentPortfolio> GetPortfolioSummary()
        {
            // Use injected userRepository here
            List<User> userList = userRepository.GetUsers();

            var portfolios = new List<InvestmentPortfolio>();

            foreach (var user in userList)
            {
                // Use the mock investmentsRepository here
                var investments = investmentsRepository.GetInvestmentsHistory()
                    .Where(i => i.InvestorCnp == user.Cnp)
                    .ToList();

                if (investments.Any())
                {
                    var totalAmountInvested = (decimal)investments.Sum(i => i.AmountInvested);
                    var totalAmountReturned = (decimal)investments.Sum(i => i.AmountReturned);

                    var averageROI = totalAmountInvested == 0 ? 0 : totalAmountReturned / totalAmountInvested;

                    var portfolio = new InvestmentPortfolio(
                        user.FirstName,
                        user.LastName,
                        totalAmountInvested,
                        totalAmountReturned,
                        averageROI,
                        investments.Count,
                        user.RiskScore);

                    portfolios.Add(portfolio);
                }
            }

            return portfolios;
        }

    }
}