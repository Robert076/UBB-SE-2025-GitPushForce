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
            DataTable? dataTable = dbConn.ExecuteReader("GetLoans", null, CommandType.StoredProcedure);
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
                    Convert.ToSingle(row["MonthlyPaymentAmount"]),
                    Convert.ToInt32(row["MonthlyPaymentsCompleted"]),
                    Convert.ToSingle(row["RepaidAmount"]),
                    Convert.ToSingle(row["Penalty"]),
                    row["State"].ToString()
                );
                loans.Add(loan);
            }

            return loans;
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
                        Convert.ToSingle(row["MonthlyPaymentAmount"]),
                        Convert.ToInt32(row["MonthlyPaymentsCompleted"]),
                        Convert.ToSingle(row["RepaidAmount"]),
                        Convert.ToSingle(row["Penalty"]),
                        row["State"].ToString()
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

        public void UpdateLoan(Loan loan)
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
                dbConn.ExecuteNonQuery("UpdateLoan", parameters, CommandType.StoredProcedure);
            }
            catch (Exception exception)
            {
                throw new Exception($"Error - UpdateLoan: {exception.Message}");
            }
        }

        public void DeleteLoan(int loanID)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@LoanRequestID", loanID)
                };
                dbConn.ExecuteNonQuery("DeleteLoan", parameters, CommandType.StoredProcedure);
            }
            catch (Exception exception)
            {
                throw new Exception($"Error - DeleteLoan: {exception.Message}");
            }
        }

        public Loan GetLoanByID(int loanID)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@LoanRequestID", loanID)
                };
                DataTable? dataTable = dbConn.ExecuteReader("GetLoanByID", parameters, CommandType.StoredProcedure);
                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    throw new Exception("Loan not found");
                }
                DataRow row = dataTable.Rows[0];
                Loan loan = new Loan(
                    Convert.ToInt32(row["LoanRequestID"]),
                    row["UserCNP"].ToString(),
                    Convert.ToSingle(row["Amount"]),
                    Convert.ToDateTime(row["ApplicationDate"]),
                    Convert.ToDateTime(row["RepaymentDate"]),
                    Convert.ToSingle(row["InterestRate"]),
                    Convert.ToInt32(row["NoMonths"]),
                    Convert.ToSingle(row["MonthlyPaymentAmount"]),
                    Convert.ToInt32(row["MonthlyPaymentsCompleted"]),
                    Convert.ToSingle(row["RepaidAmount"]),
                    Convert.ToSingle(row["Penalty"]),
                    row["State"].ToString()
                );
                return loan;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error - GetLoanByID: {exception.Message}");
            }
        }
    }
}
