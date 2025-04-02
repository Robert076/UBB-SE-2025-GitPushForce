using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Model
{
    public class Investment
    {
        private int _id;
        private string _investorCNP;
        private string _details;
        private float _amountInvested;
        private float _amountReturned;
        private DateTime _investmentDate;

        public Investment(int id, string investorCNP, string details, float amountInvested, float amountReturned, DateTime investmentDate)
        {
            _id = id;
            _investorCNP = investorCNP;
            _details = details;
            _amountInvested = amountInvested;
            _amountReturned = amountReturned;
            _investmentDate = investmentDate;
        }

        public Investment()
        {
            _id = 0;
            _investorCNP = string.Empty;
            _details = string.Empty;
            _amountInvested = 0;
            _amountReturned = 0;
            _investmentDate = DateTime.Now;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string InvestorCNP
        {
            get { return _investorCNP; }
            set { _investorCNP = value; }
        }

        public string Details
        {
            get { return _details; }
            set { _details = value; }
        }

        public float AmountInvested
        {
            get { return _amountInvested; }
            set { _amountInvested = value; }
        }

        public float AmountReturned
        {
            get { return _amountReturned; }
            set { _amountReturned = value; }
        }

        public DateTime InvestmentDate
        {
            get { return _investmentDate; }
            set { _investmentDate = value; }
        }
    }

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