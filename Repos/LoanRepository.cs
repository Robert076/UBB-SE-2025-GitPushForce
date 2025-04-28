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
            string query = "SELECT * FROM Loans;";
            DataTable? dataTable = dbConn.ExecuteReader(query, null, CommandType.Text);
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
                string query = "SELECT LoanRequestID, UserCNP, Amount, ApplicationDate, RepaymentDate, InterestRate, NoMonths, MonthlyPaymentAmount FROM Loans WHERE UserCNP = @UserCNP;";
                DataTable? dataTable = dbConn.ExecuteReader(query, parameters, CommandType.Text);
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

                string query = "INSERT INTO Loans (LoanRequestID, UserCNP, Amount, ApplicationDate, RepaymentDate, InterestRate, NoMonths, State, MonthlyPaymentAmount, MonthlyPaymentsCompleted, RepaidAmount, Penalty) VALUES (@LoanRequestID, @UserCNP, @Amount, @ApplicationDate, @RepaymentDate, @InterestRate, @NoMonths, @State, @MonthlyPaymentAmount, @MonthlyPaymentsCompleted, @RepaidAmount, @Penalty);";
                dbConn.ExecuteNonQuery(query, parameters, CommandType.Text);
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
                string query = "UPDATE Loans SET UserCNP = @UserCNP, Amount = @Amount, ApplicationDate = @ApplicationDate, RepaymentDate = @RepaymentDate, InterestRate = @InterestRate, NoMonths = @NoMonths, State = @State, MonthlyPaymentAmount = @MonthlyPaymentAmount, MonthlyPaymentsCompleted = @MonthlyPaymentsCompleted, RepaidAmount = @RepaidAmount, Penalty = @Penalty WHERE LoanRequestID = @LoanRequestID;";
                dbConn.ExecuteNonQuery(query, parameters, CommandType.Text);
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
                string query = "DELETE FROM LoanRequest WHERE ID = @LoanRequestID;";
                dbConn.ExecuteNonQuery(query, parameters, CommandType.Text);
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
                string query = "SELECT * FROM Loans WHERE LoanRequestID = @LoanRequestID;";
                DataTable? dataTable = dbConn.ExecuteReader(query, parameters, CommandType.Text);
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

        public void UpdateHistoryForUser(string UserCNP, int NewScore)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserCNP", SqlDbType.VarChar, 16) { Value = UserCNP },
                new SqlParameter("@NewScore", SqlDbType.Int) { Value = NewScore }
            };
            string query = "IF EXISTS (SELECT 1 FROM CreditScoreHistory WHERE UserCNP = @UserCNP AND Date = CAST(GETDATE() AS DATE)) BEGIN UPDATE CreditScoreHistory SET Score = @NewScore WHERE UserCNP = @UserCNP AND Date = CAST(GETDATE() AS DATE); END ELSE BEGIN INSERT INTO CreditScoreHistory (UserCNP, Date, Score) VALUES (@UserCNP, CAST(GETDATE() AS DATE), @NewScore); END;";
            dbConn.ExecuteNonQuery(query, parameters, CommandType.Text);
        }
    }
}
