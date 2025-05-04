using System;
namespace Src.Model
{
    public class BillSplitReport
    {
        public int Id { get; set; }
        public string ReportedUserCnp { get; set; }
        public string ReportingUserCnp { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public float BillShare { get; set; }

        public BillSplitReport(int id, string reportedCNP, string reporterCNP, DateTime dateTransaction, float billShare)
        {
            Id = id;
            ReportedUserCnp = reportedCNP;
            ReportingUserCnp = reporterCNP;
            DateOfTransaction = dateTransaction;
            BillShare = billShare;
        }

        public BillSplitReport()
        {
            Id = 0;
            ReportedUserCnp = string.Empty;
            ReportingUserCnp = string.Empty;
            DateOfTransaction = DateTime.Now;
            BillShare = 0;
        }
    }
}