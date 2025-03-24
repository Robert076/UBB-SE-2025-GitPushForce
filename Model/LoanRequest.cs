using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class LoanRequest
    {
        private int LoanID { get; set; }
        private string UserCNP { get; set; }
        private float LoanAmount { get; set; }
        private DateTime ApplicationDate { get; set; } = DateTime.Today;
        private DateTime RepaymentDate { get; set; }
        private float InterestRate { get; set; } = 0;
        private int NoMonths { get; set; }
        private float MonthlyPaymentAmount { get; set; }
        private string ApprovalStatus { get; set; } = "pending";
        private string State { get; set; } = "active";
        private int MonthlyPaymentsCompleted { get; set; } = 0;
        private float RepaidAmount { get; set; } = 0;
        private float Penalty { get; set; } = 0;
    }
}
