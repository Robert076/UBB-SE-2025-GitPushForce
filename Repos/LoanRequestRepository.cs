using src.Data;
using src.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.WebUI;

namespace src.Repos
{
    class LoanRequestRepository
    {
        private readonly DatabaseConnection dbConn;

        public LoanRequestRepository(DatabaseConnection dbConn)
        {
            this.dbConn = dbConn;
        }

        public List<LoanRequest> GetLoanRequests()
        {
            try
            {
                DataTable? dataTable = dbConn.ExecuteReader("GetLoanRequests", null, CommandType.StoredProcedure);

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    throw new Exception("Loan requests table is empty");
                }

                List<LoanRequest> loanRequests = new List<LoanRequest>();

                foreach (DataRow row in dataTable.Rows)
                {
                    LoanRequest loanRequest = new LoanRequest(
                        Convert.ToInt32(row["RequestID"]),
                        row["UserCNP"].ToString(),
                        Convert.ToSingle(row["Amount"]),
                        Convert.ToDateTime(row["ApplicationDate"]),
                        Convert.ToDateTime(row["RepaymentDate"])
                    );

                    loanRequests.Add(loanRequest);
                }

                return loanRequests;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error - GetLoanRequests: {exception.Message}");
            }
        }
    }
}
