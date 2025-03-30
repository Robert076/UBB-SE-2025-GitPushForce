using src.Data;
using src.Model;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Repos
{
    class LoanRepository
    {
        private readonly DatabaseConnection dbConn;

        public LoanRepository(DatabaseConnection dbConn)
        {
            this.dbConn = dbConn;
        }

        public List<Loan> GetLoans()
        {
            try
            {
                DataTable? dataTable = dbConn.ExecuteReader("GetLoans", null, CommandType.StoredProcedure);
                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    throw new Exception("Loans table is empty");
                }

                List<Loan> loans = new List<Loan>();
                foreach (DataRow row in dataTable.Rows)
                {
                    Loan loan = new Loan(
                        Convert.ToInt32(row["LoanRequestID"]),
                        row["UserCNP"].ToString(),
                        Convert.ToSingle(row["Amount"]),
                        Convert.ToDateTime(row["ApplicationDate"]),
                        Convert.ToDateTime(row["RepaymentDate"]),
                        Convert.ToSingle(row["InterestRate"]),
                        Convert.ToInt32(row["NoMonths"]),
                        Convert.ToSingle(row["MonthlyPaymentAmount"])
                    );
                    loans.Add(loan);
                }

                return loans;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error - GetLoans: {exception.Message}");
            }
        }

        public List<Loan> GetUserLoans(string userCNP)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserCNP", userCNP)
                };
                DataTable? dataTable = dbConn.ExecuteReader("GetLoansByUserCNP", parameters, CommandType.StoredProcedure);
                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    throw new Exception("Loans table is empty");
                }
                List<Loan> loans = new List<Loan>();
                foreach (DataRow row in dataTable.Rows)
                {
                    Loan loan = new Loan(
                        Convert.ToInt32(row["LoanRequestID"]),
                        row["UserCNP"].ToString(),
                        Convert.ToSingle(row["Amount"]),
                        Convert.ToDateTime(row["ApplicationDate"]),
                        Convert.ToDateTime(row["RepaymentDate"]),
                        Convert.ToSingle(row["InterestRate"]),
                        Convert.ToInt32(row["NoMonths"]),
                        Convert.ToSingle(row["MonthlyPaymentAmount"])
                    );
                    loans.Add(loan);
                }
                return loans;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error - GetLoansByUserCNP: {exception.Message}");
            }
        }
    }
}
