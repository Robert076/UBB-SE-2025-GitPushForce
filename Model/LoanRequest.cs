using System;

namespace src.Model
{
    public class LoanRequest
    {
        public int Id { get; set; }
        public string UserCnp { get; set; }
        public float Amount { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime RepaymentDate { get; set; }
        public string Status { get; set; }

        public LoanRequest(int requestId, string userCnp, float amount, DateTime applicationDate, DateTime repaymentDate, string status)
        {
            Id = requestId;
            UserCnp = userCnp;
            Amount = amount;
            ApplicationDate = applicationDate;
            RepaymentDate = repaymentDate;
            Status = status;
        }
    }
}
