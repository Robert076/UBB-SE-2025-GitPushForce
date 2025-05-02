using System;

namespace src.Model
{
    public class Investment
    {
        public int Id { get; set; }
        public string InvestorCnp { get; set; }
        public string Details { get; set; }
        public float AmountInvested { get; set; }
        public float AmountReturned { get; set; }
        public DateTime InvestmentDate { get; set; }

        public Investment(int id, string investorCnp, string details, float amountInvested, float amountReturned, DateTime investmentDate)
        {
            Id = id;
            InvestorCnp = investorCnp;
            Details = details;
            AmountInvested = amountInvested;
            AmountReturned = amountReturned;
            InvestmentDate = investmentDate;
        }

        public Investment()
        {
            Id = 0;
            InvestorCnp = string.Empty;
            Details = string.Empty;
            AmountInvested = 0;
            AmountReturned = 0;
            InvestmentDate = DateTime.Now;
        }
    }
}