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

        public void AddLoan(Loan loan)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@LoanRequestID", loan.LoanID),
                    new SqlParameter("@UserCNP", loan.UserCNP),
                    new SqlParameter("@Amount", loan.LoanAmount),
                    new SqlParameter("@ApplicationDate", loan.ApplicationDate),
                    new SqlParameter("@RepaymentDate", loan.RepaymentDate),
                    new SqlParameter("@InterestRate", loan.InterestRate),
                    new SqlParameter("@NoMonths", loan.NoMonths),
                    new SqlParameter("@State", loan.State),
                    new SqlParameter("@MonthlyPaymentAmount", loan.MonthlyPaymentAmount),
                    new SqlParameter("@MonthlyPaymentsCompleted", loan.MonthlyPaymentsCompleted),
                    new SqlParameter("@RepaidAmount", loan.RepaidAmount),
                    new SqlParameter("@Penalty", loan.Penalty)
                };

                dbConn.ExecuteNonQuery("AddLoan", parameters, CommandType.StoredProcedure);
            }
            catch (Exception exception)
            {
                throw new Exception($"Error - AddLoan: {exception.Message}");
            }
        }
    }
}
