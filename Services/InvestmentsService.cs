using src.Data;
using src.Model;
using src.Repos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace src.Services
{
    public class InvestmentsService
    {
        private readonly UserRepository _userRepository;
        private readonly InvestmentsRepository _investmentsRepository;

        public InvestmentsService(UserRepository userRepository, InvestmentsRepository investmentsRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _investmentsRepository = investmentsRepository ?? throw new ArgumentNullException(nameof(investmentsRepository));
        }

        public void CalculateAndUpdateRiskScore()
        {
            var allExistentUsers = _userRepository.GetUsers();

            foreach (var currentUser in allExistentUsers)
            {
                var recentInvestments = GetRecentInvestments(currentUser.CNP);
                if(recentInvestments != null) {
                    var riskScoreChange = CalculateRiskScoreChange(currentUser, recentInvestments);
                    UpdateUserRiskScore(currentUser, riskScoreChange);
                }
            }
        }

        private List<Investment> GetRecentInvestments(string cnp)
        {
            var allInvestments = _investmentsRepository.GetInvestmentsHistory();

            // Since the list is sorted in ascending order, the latest investment is the last one.
            var latestInvestment = allInvestments
                .Where(i => i.InvestorCNP == cnp)
                .OrderBy(i => i.InvestmentDate)
                .LastOrDefault();

            if (latestInvestment == null)
            {
                return null; // Return null if no investments are found for the user.
            }

            var latestInvestmentDate = latestInvestment.InvestmentDate;

            // Return all investments from last week, taking in consideration last transaction
            return allInvestments
                .Where(i => i.InvestorCNP == cnp)
                .Where(i => i.InvestmentDate >= latestInvestmentDate.AddDays(-7))
                .OrderByDescending(i => i.InvestmentDate)
                .ToList();
        }



        private int CalculateRiskScoreChange(User user, List<Investment> investments)
        {
            int riskScoreChange = 0;

            // Calculate trading performance impact
            var profitableTrades = investments.Where(i => i.AmountReturned > i.AmountInvested).Count();
            var totalTrades = investments.Count;
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

            var SafeInvestmentThreshold = 0.1f;
            var RiskyInvestmentThreshold = 0.3f;
            if (totalInvested / user.Income < SafeInvestmentThreshold)
            {
                riskScoreChange -= 5;
            }
            else if (totalInvested / user.Income > RiskyInvestmentThreshold)
            {
                riskScoreChange += 5;
            }

            return riskScoreChange;
        }

        private void UpdateUserRiskScore(User user, int riskScoreChange)
        {
            user.RiskScore += riskScoreChange;

            // Ensure risk score stays within the range (1 to 100)
            var MinRiskScore = 1;
            var MaxRiskScore = 100;
            user.RiskScore = Math.Max(MinRiskScore, Math.Min(user.RiskScore, MaxRiskScore));
        }



        public void CalculateAndUpdateROI()
        {
            var allExistentUsers = _userRepository.GetUsers();

            foreach (var currentUser in allExistentUsers)
                CalculateAndSetUserROI(currentUser);
        }

        private void CalculateAndSetUserROI(User user)
        {
            var investmentOpen = -1;

            var allInvestments = _investmentsRepository.GetInvestmentsHistory()
                .Where(i => i.InvestorCNP == user.CNP)
                .Where(i => i.AmountReturned != investmentOpen) // Exclude open transactions
                .ToList();

            if (!allInvestments.Any())
            {
                user.ROI = 1; // Set ROI to 1 if no closed transactions. This means no effect over credit score.
                return;
            }

            // Calculate ROI for each investment: [Net Profit / Amount Invested]
            var totalNetProfit = allInvestments.Sum(i => i.AmountReturned - i.AmountInvested);
            var totalInvested = allInvestments.Sum(i => i.AmountInvested);

            if (totalInvested == 0) // Avoid division by zero
            {
                user.ROI = 1; 
                return;
            }

            var newUserROI = (decimal)totalNetProfit / (decimal)totalInvested;

            user.ROI = newUserROI;
        }

        public void CreditScoreUpdateInvestmentsBased(){
            
            var allExistentUsers = _userRepository.GetUsers();

            foreach (var currentUser in allExistentUsers){

                var riskScorePercent = currentUser.RiskScore / 100;
                var creditScoreSubstracted = currentUser.CreditScore * riskScorePercent;
                currentUser.CreditScore -= creditScoreSubstracted;

                if(currentUser.ROI <= 0)
                    currentUser.CreditScore -= 100; // maximum amount possible to be substracted
                else if(currentUser.ROI < 1){
                    var decreaseAmount = 10 / currentUser.ROI;
                    currentUser.CreditScore -= (int) decreaseAmount;
                } else {
                    var increaseAmount = 10 * currentUser.ROI;
                    currentUser.CreditScore += (int) increaseAmount;
                }

                var minCreditScore = 100;
                var maxCreditScore = 700;

                currentUser.CreditScore = Math.Min(maxCreditScore, Math.Max(minCreditScore, currentUser.CreditScore));
            }
        }

        public Dictionary<string, decimal> GetPortfolioSummary(string userCNP)
        {
            UserRepository userRepository = new UserRepository(new DatabaseConnection());
            List<User> userList = userRepository.GetUsers();

            var investments = _investmentsRepository.GetInvestmentsHistory()
                .Where(i => i.InvestorCNP == userCNP)
                .ToList();

            var portfolioSummary = new Dictionary<string, decimal>();

            if (investments.Any())
            {
                var totalAmountInvested = (decimal)investments.Sum(i => i.AmountInvested);
                var totalAmountReturned = (decimal)investments.Sum(i => i.AmountReturned);
                var averageROI = totalAmountReturned / totalAmountInvested;

                portfolioSummary.Add("Total Invested", totalAmountInvested);
                portfolioSummary.Add("Total Returns", totalAmountReturned);
                portfolioSummary.Add("Average ROI", averageROI);
                portfolioSummary.Add("Number of Investments", investments.Count);
            }

            return portfolioSummary;
        }

    }
}
