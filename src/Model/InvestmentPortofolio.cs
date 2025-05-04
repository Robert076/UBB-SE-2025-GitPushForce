namespace Src.Model
{
    public class InvestmentPortfolio
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public decimal TotalAmountInvested { get; set; }
        public decimal TotalAmountReturned { get; set; }
        public decimal AverageROI { get; set; }
        public int NumberOfInvestments { get; set; }
        public int RiskFactor { get; set; }

        public InvestmentPortfolio(string firstName, string secondName, decimal totalAmountInvested, decimal totalAmountReturned, decimal averageROI, int numberOfInvestments, int riskFactor)
        {
            FirstName = firstName;
            SecondName = secondName;
            TotalAmountInvested = totalAmountInvested;
            TotalAmountReturned = totalAmountReturned;
            AverageROI = averageROI;
            NumberOfInvestments = numberOfInvestments;
            RiskFactor = riskFactor;
        }
    }
}
