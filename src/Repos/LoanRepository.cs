using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Src.Data;
using Src.Model;

namespace Src.Repos
{
    public class LoanRepository : ILoanRepository
    {
        private readonly DatabaseConnection dbConnection;

        public LoanRepository(DatabaseConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public List<Loan> GetLoans()
        {
          
                const string SelectQuery = "SELECT * FROM Loans";
                DataTable dataTable = dbConnection.ExecuteReader(SelectQuery, null, CommandType.Text);

                List<Loan> loans = new List<Loan>();

                foreach (DataRow row in dataTable.Rows)
                {
                    loans.Add(CreateLoanFromDataRow(row));
                }

                return loans;
           
        }

        public List<Loan> GetUserLoans(string userCnp)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserCnp", userCnp)
                };

                const string SelectQuery = @"
                    SELECT LoanRequestId, UserCnp, Amount, ApplicationDate, RepaymentDate, 
                           InterestRate, NumberOfMonths, MonthlyPaymentAmount, 
                           MonthlyPaymentsCompleted, RepaidAmount, Penalty, Status 
                    FROM Loans 
                    WHERE UserCnp = @UserCnp";

                DataTable dataTable = dbConnection.ExecuteReader(SelectQuery, parameters, CommandType.Text);

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    return new List<Loan>();
                }

                List<Loan> loans = new List<Loan>();

                foreach (DataRow row in dataTable.Rows)
                {
                    loans.Add(CreateLoanFromDataRow(row));
                }

                return loans;
            }
            catch (Exception exception)
            {
                throw new Exception("Error retrieving user loans", exception);
            }
        }

        public void AddLoan(Loan loan)
        {
            if (loan == null)
            {
                throw new ArgumentNullException(nameof(loan));
            }

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@LoanRequestId", loan.Id),
                    new SqlParameter("@UserCnp", loan.UserCnp),
                    new SqlParameter("@Amount", loan.LoanAmount),
                    new SqlParameter("@ApplicationDate", loan.ApplicationDate),
                    new SqlParameter("@RepaymentDate", loan.RepaymentDate),
                    new SqlParameter("@InterestRate", loan.InterestRate),
                    new SqlParameter("@NumberOfMonths", loan.NumberOfMonths),
                    new SqlParameter("@Status", loan.Status),
                    new SqlParameter("@MonthlyPaymentAmount", loan.MonthlyPaymentAmount),
                    new SqlParameter("@MonthlyPaymentsCompleted", loan.MonthlyPaymentsCompleted),
                    new SqlParameter("@RepaidAmount", loan.RepaidAmount),
                    new SqlParameter("@Penalty", loan.Penalty)
                };

                const string InsertQuery = @"
                    INSERT INTO Loans 
                        (LoanRequestId, UserCnp, Amount, ApplicationDate, RepaymentDate, 
                         InterestRate, NumberOfMonths, Status, MonthlyPaymentAmount, 
                         MonthlyPaymentsCompleted, RepaidAmount, Penalty) 
                    VALUES 
                        (@LoanRequestId, @UserCnp, @Amount, @ApplicationDate, @RepaymentDate, 
                         @InterestRate, @NumberOfMonths, @Status, @MonthlyPaymentAmount, 
                         @MonthlyPaymentsCompleted, @RepaidAmount, @Penalty)";

                int rowsAffected = dbConnection.ExecuteNonQuery(InsertQuery, parameters, CommandType.Text);

                if (rowsAffected == 0)
                {
                    throw new Exception("No rows were inserted");
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error adding loan", exception);
            }
        }

        public void UpdateLoan(Loan loan)
        {
            if (loan == null)
            {
                throw new ArgumentNullException(nameof(loan));
            }

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@LoanRequestId", loan.Id),
                    new SqlParameter("@UserCnp", loan.UserCnp),
                    new SqlParameter("@Amount", loan.LoanAmount),
                    new SqlParameter("@ApplicationDate", loan.ApplicationDate),
                    new SqlParameter("@RepaymentDate", loan.RepaymentDate),
                    new SqlParameter("@InterestRate", loan.InterestRate),
                    new SqlParameter("@NumberOfMonths", loan.NumberOfMonths),
                    new SqlParameter("@Status", loan.Status),
                    new SqlParameter("@MonthlyPaymentAmount", loan.MonthlyPaymentAmount),
                    new SqlParameter("@MonthlyPaymentsCompleted", loan.MonthlyPaymentsCompleted),
                    new SqlParameter("@RepaidAmount", loan.RepaidAmount),
                    new SqlParameter("@Penalty", loan.Penalty)
                };

                const string UpdateQuery = @"
                    UPDATE Loans 
                    SET UserCnp = @UserCnp, 
                        Amount = @Amount, 
                        ApplicationDate = @ApplicationDate, 
                        RepaymentDate = @RepaymentDate, 
                        InterestRate = @InterestRate, 
                        NumberOfMonths = @NumberOfMonths, 
                        Status = @Status, 
                        MonthlyPaymentAmount = @MonthlyPaymentAmount, 
                        MonthlyPaymentsCompleted = @MonthlyPaymentsCompleted, 
                        RepaidAmount = @RepaidAmount, 
                        Penalty = @Penalty 
                    WHERE LoanRequestId = @LoanRequestId";

                int rowsAffected = dbConnection.ExecuteNonQuery(UpdateQuery, parameters, CommandType.Text);

                if (rowsAffected == 0)
                {
                    throw new Exception("No rows were updated");
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error updating loan", exception);
            }
        }

        public void DeleteLoan(int loanId)
        {
            if (loanId <= 0)
            {
                throw new ArgumentException("Invalid loan ID", nameof(loanId));
            }

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@LoanRequestId", loanId)
                };

                const string DeleteQuery = "DELETE FROM Loans WHERE LoanRequestId = @LoanRequestId";
                int rowsAffected = dbConnection.ExecuteNonQuery(DeleteQuery, parameters, CommandType.Text);

                if (rowsAffected == 0)
                {
                    throw new Exception($"No loan found with ID: {loanId}");
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error deleting loan", exception);
            }
        }

        public Loan GetLoanById(int loanId)
        {
            if (loanId <= 0)
            {
                throw new ArgumentException("Invalid loan ID", nameof(loanId));
            }

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@LoanRequestId", loanId)
                };

                const string SelectQuery = "SELECT * FROM Loans WHERE LoanRequestId = @LoanRequestId";
                DataTable dataTable = dbConnection.ExecuteReader(SelectQuery, parameters, CommandType.Text);

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    throw new Exception($"Loan with ID {loanId} not found");
                }

                return CreateLoanFromDataRow(dataTable.Rows[0]);
            }
            catch (Exception exception)
            {
                throw new Exception("Error retrieving loan by ID", exception);
            }
        }

        public void UpdateCreditScoreHistoryForUser(string userCnp, int newScore)
        {
            if (string.IsNullOrWhiteSpace(userCnp))
            {
                throw new ArgumentException("User CNP cannot be empty", nameof(userCnp));
            }

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserCnp", userCnp),
                    new SqlParameter("@NewScore", newScore)
                };

                const string updateOrInsertQuery = @"
                    IF EXISTS (SELECT 1 FROM CreditScoreHistory WHERE UserCnp = @UserCnp AND Date = CAST(GETDATE() AS DATE))
                    BEGIN
                        UPDATE CreditScoreHistory
                        SET Score = @NewScore
                        WHERE UserCnp = @UserCnp AND Date = CAST(GETDATE() AS DATE);
                    END
                    ELSE
                    BEGIN
                        INSERT INTO CreditScoreHistory (UserCnp, Date, Score)
                        VALUES (@UserCnp, CAST(GETDATE() AS DATE), @NewScore);
                    END";

                int rowsAffected = dbConnection.ExecuteNonQuery(updateOrInsertQuery, parameters, CommandType.Text);

                if (rowsAffected == 0)
                {
                    throw new Exception("No changes were made to credit score history");
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error updating credit score history", exception);
            }
        }

        private Loan CreateLoanFromDataRow(DataRow row)
        {
            return new Loan(
                loanID: Convert.ToInt32(row["LoanRequestId"]),
                userCnp: row["UserCnp"].ToString(),
                loanAmount: Convert.ToSingle(row["Amount"]),
                applicationDate: Convert.ToDateTime(row["ApplicationDate"]),
                repaymentDate: Convert.ToDateTime(row["RepaymentDate"]),
                interestRate: Convert.ToSingle(row["InterestRate"]),
                numberOfMonths: Convert.ToInt32(row["NumberOfMonths"]),
                monthlyPaymentAmount: Convert.ToSingle(row["MonthlyPaymentAmount"]),
                monthlyPaymentsCompleted: Convert.ToInt32(row["MonthlyPaymentsCompleted"]),
                repaidAmount: Convert.ToSingle(row["RepaidAmount"]),
                penalty: Convert.ToSingle(row["Penalty"]),
                status: row["Status"].ToString());
        }
    }
}