using Microsoft.Data.SqlClient;
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
                        Convert.ToInt32(row["ID"]),
                        row["UserCNP"].ToString(),
                        Convert.ToSingle(row["Amount"]),
                        Convert.ToDateTime(row["ApplicationDate"]),
                        Convert.ToDateTime(row["RepaymentDate"]),
                        row["State"].ToString()
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

        public List<LoanRequest> GetUnsolvedLoanRequests()
        {
            try
            {
                DataTable? dataTable = dbConn.ExecuteReader("GetUnsolvedLoanRequests", null, CommandType.StoredProcedure);

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    throw new Exception("Loan requests table is empty");
                }

                List<LoanRequest> loanRequests = new List<LoanRequest>();

                foreach (DataRow row in dataTable.Rows)
                {
                    LoanRequest loanRequest = new LoanRequest(
                        Convert.ToInt32(row["ID"]),
                        row["UserCNP"].ToString(),
                        Convert.ToSingle(row["Amount"]),
                        Convert.ToDateTime(row["ApplicationDate"]),
                        Convert.ToDateTime(row["RepaymentDate"]),
                        row["State"].ToString()
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

        public void SolveLoanRequest(Int32 loanRequestID)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@LoanRequestID", loanRequestID)
                };

                int rowsAffected = dbConn.ExecuteNonQuery("MarkRequestAsSolved", parameters, CommandType.StoredProcedure);

                if (rowsAffected == 0)
                {
                    throw new Exception($"No loan request was found with Id {loanRequestID}");
                }
            }
            catch (Exception exception)
            {
                throw new Exception($"Error - MarkRequestAsSolved: {exception.Message}");
            }
        }

        public void DeleteLoanRequest(Int32 loanRequestID)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@LoanRequestID", loanRequestID)
                };
                int rowsAffected = dbConn.ExecuteNonQuery("DeleteLoanRequest", parameters, CommandType.StoredProcedure);
                if (rowsAffected == 0)
                {
                    throw new Exception($"No loan request was found with Id {loanRequestID}");
                }
            }
            catch (Exception exception)
            {
                throw new Exception($"Error - DeleteLoanRequest: {exception.Message}");
            }
        }
    }
}
