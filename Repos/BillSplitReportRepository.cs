using src.Data;
using src.Model;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace src.Repos
{
    public class BillSplitReportRepository: IBillSplitReportRepository
    {
        private readonly DatabaseConnection _dbConnection;

        public BillSplitReportRepository(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<BillSplitReport> GetBillSplitReports()
        {
            try
            {
                const string SelectBillSplitReportsQuery = "SELECT Id, ReportedUserCnp, ReportingUserCnp, DateOfTransaction, BillShare FROM BillSplitReports";
                DataTable reportDataTable = _dbConnection.ExecuteReader(SelectBillSplitReportsQuery, null, CommandType.Text);

                if (reportDataTable == null || reportDataTable.Rows.Count == 0)
                {
                    throw new Exception("Bill split reports table is empty");
                }

                List<BillSplitReport> billSplitReports = new List<BillSplitReport>();

                foreach (DataRow row in reportDataTable.Rows)
                {
                    BillSplitReport billSplitReport = new BillSplitReport
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        ReportedUserCnp = row["ReportedUserCnp"].ToString() ?? string.Empty,
                        ReportingUserCnp = row["ReportingUserCnp"].ToString() ?? string.Empty,
                        DateOfTransaction = Convert.ToDateTime(row["DateOfTransaction"]),
                        BillShare = Convert.ToSingle(row["BillShare"])
                    };

                    billSplitReports.Add(billSplitReport);
                }

                return billSplitReports;
            }
            catch (Exception exception)
            {
                throw new Exception("Error retrieving bill split reports", exception);
            }
        }

        public void DeleteBillSplitReport(int id)
        {
            try
            {
                const string DeleteQuery = "DELETE FROM BillSplitReports WHERE Id = @Id";
                SqlParameter[] deleteParameters = new SqlParameter[]
                {
                    new SqlParameter("@Id", id)
                };

                int rowsAffected = _dbConnection.ExecuteNonQuery(DeleteQuery, deleteParameters, CommandType.Text);

                if (rowsAffected == 0)
                {
                    throw new Exception($"No bill split report found with Id {id}");
                }
            }
            catch (Exception exception)
            {
                throw new Exception($"Error deleting bill split report: {exception.Message}", exception);
            }
        }

        public void CreateBillSplitReport(BillSplitReport billSplitReport)
        {
            try
            {
                const string InsertQuery = @"
                    INSERT INTO BillSplitReports 
                        (ReportedUserCnp, ReportingUserCnp, DateOfTransaction, BillShare)
                    VALUES 
                        (@ReportedUserCnp, @ReportingUserCnp, @DateOfTransaction, @BillShare)";

                SqlParameter[] insertParameters = new SqlParameter[]
                {
                    new SqlParameter("@ReportedUserCnp", billSplitReport.ReportedUserCnp),
                    new SqlParameter("@ReportingUserCnp", billSplitReport.ReportingUserCnp),
                    new SqlParameter("@DateOfTransaction", billSplitReport.DateOfTransaction),
                    new SqlParameter("@BillShare", billSplitReport.BillShare)
                };

                _dbConnection.ExecuteNonQuery(InsertQuery, insertParameters, CommandType.Text);
            }
            catch (Exception exception)
            {
                throw new Exception($"Error creating bill split report: {exception.Message}", exception);
            }
        }

        public bool CheckLogsForSimilarPayments(BillSplitReport billSplitReport)
        {
            const string SelectQuery = @"
                SELECT COUNT(*)
                FROM TransactionLogs
                WHERE SenderCnp = @ReportedUserCnp
                  AND ReceiverCnp = @ReportingUserCnp
                  AND TransactionDate > @DateOfTransaction
                  AND Amount = @BillShare
                  AND (TransactionDescription LIKE '%bill%' 
                       OR TransactionDescription LIKE '%share%' 
                       OR TransactionDescription LIKE '%split%')
                  AND TransactionType != 'Bill Split'";

            SqlParameter[] selectParameters = new SqlParameter[]
            {
                new SqlParameter("@ReportedUserCnp", billSplitReport.ReportedUserCnp),
                new SqlParameter("@ReportingUserCnp", billSplitReport.ReportingUserCnp),
                new SqlParameter("@DateOfTransaction", billSplitReport.DateOfTransaction),
                new SqlParameter("@BillShare", billSplitReport.BillShare)
            };

            int count = _dbConnection.ExecuteScalar<int>(SelectQuery, selectParameters, CommandType.Text);
            return count > 0;
        }

        public int GetCurrentBalance(BillSplitReport billSplitReport)
        {
            try
            {
                const string SelectQuery = "SELECT Balance FROM Users WHERE Cnp = @ReportedUserCnp";
                SqlParameter[] selectParameter = new SqlParameter[]
                {
                    new SqlParameter("@ReportedUserCnp", billSplitReport.ReportedUserCnp)
                };

                return _dbConnection.ExecuteScalar<int>(SelectQuery, selectParameter, CommandType.Text);
            }
            catch (SqlException sqlException)
            {
                throw new Exception($"Error getting current balance: {sqlException.Message}", sqlException);
            }
            catch (Exception exception)
            {
                throw new Exception($"Error getting current balance: {exception.Message}", exception);
            }
        }

        public decimal SumTransactionsSinceReport(BillSplitReport billSplitReport)
        {
            if (string.IsNullOrWhiteSpace(billSplitReport.ReportedUserCnp))
            {
                throw new ArgumentException("Invalid CNP", nameof(billSplitReport.ReportedUserCnp));
            }

            try
            {
                const string SelectQuery = @"
                    SELECT SUM(Amount)
                    FROM TransactionLogs
                    WHERE SenderCnp = @ReportedUserCnp
                      AND TransactionDate > @DateOfTransaction";

                SqlParameter[] selectParameters = new SqlParameter[]
                {
                    new SqlParameter("@ReportedUserCnp", billSplitReport.ReportedUserCnp),
                    new SqlParameter("@DateOfTransaction", billSplitReport.DateOfTransaction)
                };

                decimal result = _dbConnection.ExecuteScalar<decimal>(SelectQuery, selectParameters, CommandType.Text);
                return result;
            }
            catch (SqlException sqlException)
            {
                throw new Exception("Error summing transactions since report date", sqlException);
            }
            catch (Exception exception)
            {
                throw new Exception("Error summing transactions since report date", exception);
            }
        }

        public bool CheckHistoryOfBillShares(BillSplitReport billSplitReport)
        {
            if (string.IsNullOrWhiteSpace(billSplitReport.ReportedUserCnp))
            {
                throw new ArgumentException("Invalid CNP", nameof(billSplitReport.ReportedUserCnp));
            }

            try
            {
                const string SelectQuery = "SELECT NumberOfBillSharesPaid FROM Users WHERE Cnp = @ReportedUserCnp";
                SqlParameter[] selectParameters = new SqlParameter[]
                {
                    new SqlParameter("@ReportedUserCnp", billSplitReport.ReportedUserCnp)
                };

                int count = _dbConnection.ExecuteScalar<int>(SelectQuery, selectParameters, CommandType.Text);
                return count >= 3;
            }
            catch (SqlException sqlException)
            {
                throw new Exception("Error checking bill share history", sqlException);
            }
            catch (Exception exception)
            {
                throw new Exception("Error checking bill share history", exception);
            }
        }

        public bool CheckFrequentTransfers(BillSplitReport billSplitReport)
        {
            if (string.IsNullOrWhiteSpace(billSplitReport.ReportedUserCnp) ||
                string.IsNullOrWhiteSpace(billSplitReport.ReportingUserCnp))
            {
                throw new ArgumentException("Invalid CNPs");
            }

            try
            {
                const string SelectQuery = @"
                    SELECT COUNT(*)
                    FROM TransactionLogs
                    WHERE SenderCnp = @ReportedUserCnp
                      AND ReceiverCnp = @ReportingUserCnp
                      AND TransactionDate >= DATEADD(month, -1, GETDATE())";

                SqlParameter[] selectParameters = new SqlParameter[]
                {
                    new SqlParameter("@ReportedUserCnp", billSplitReport.ReportedUserCnp),
                    new SqlParameter("@ReportingUserCnp", billSplitReport.ReportingUserCnp)
                };

                int count = _dbConnection.ExecuteScalar<int>(SelectQuery, selectParameters, CommandType.Text);
                return count >= 5;
            }
            catch (SqlException sqlException)
            {
                throw new Exception("Error checking frequent transfers", sqlException);
            }
            catch (Exception exception)
            {
                throw new Exception("Error checking frequent transfers", exception);
            }
        }

        public int GetNumberOfOffenses(BillSplitReport billSplitReport)
        {
            if (string.IsNullOrWhiteSpace(billSplitReport.ReportedUserCnp))
            {
                throw new ArgumentException("Invalid CNP", nameof(billSplitReport.ReportedUserCnp));
            }

            try
            {
                const string SelectQuery = "SELECT NumberOfOffenses FROM Users WHERE Cnp = @ReportedUserCnp";
                SqlParameter[] selectParameters = new SqlParameter[]
                {
                    new SqlParameter("@ReportedUserCnp", billSplitReport.ReportedUserCnp)
                };

                return _dbConnection.ExecuteScalar<int>(SelectQuery, selectParameters, CommandType.Text);
            }
            catch (SqlException sqlException)
            {
                throw new Exception("Error retrieving number of offenses", sqlException);
            }
            catch (Exception exception)
            {
                throw new Exception("Error retrieving number of offenses", exception);
            }
        }

        public int GetCurrentCreditScore(BillSplitReport billSplitReport)
        {
            if (string.IsNullOrWhiteSpace(billSplitReport.ReportedUserCnp))
            {
                throw new ArgumentException("Invalid CNP", nameof(billSplitReport.ReportedUserCnp));
            }

            try
            {
                string selectQuery = "SELECT CreditScore FROM Users WHERE Cnp = @ReportedUserCnp";
                SqlParameter[] selectParameters = new SqlParameter[]
                {
                    new SqlParameter("@ReportedUserCnp", billSplitReport.ReportedUserCnp)
                };

                return _dbConnection.ExecuteScalar<int>(selectQuery, selectParameters, CommandType.Text);
            }
            catch (SqlException sqlException)
            {
                throw new Exception("Error retrieving credit score", sqlException);
            }
            catch (Exception exception)
            {
                throw new Exception("Error retrieving credit score", exception);
            }
        }

        public void UpdateCreditScore(BillSplitReport billSplitReport, int newCreditScore)
        {
            if (string.IsNullOrWhiteSpace(billSplitReport.ReportedUserCnp))
            {
                throw new ArgumentException("Invalid CNP", nameof(billSplitReport.ReportedUserCnp));
            }

            try
            {
                string updateQuery = "UPDATE Users SET CreditScore = @NewCreditScore WHERE Cnp = @UserCnp";
                SqlParameter[] updateParameters = new SqlParameter[]
                {
                    new SqlParameter("@UserCnp", billSplitReport.ReportedUserCnp),
                    new SqlParameter("@NewCreditScore", newCreditScore)
                };

                int rowsAffected = _dbConnection.ExecuteNonQuery(updateQuery, updateParameters, CommandType.Text);

                if (rowsAffected == 0)
                {
                    throw new Exception($"No user found with CNP: {billSplitReport.ReportedUserCnp}");
                }
            }
            catch (SqlException sqlException)
            {
                throw new Exception("Error updating credit score", sqlException);
            }
            catch (Exception exception)
            {
                throw new Exception("Error updating credit score", exception);
            }
        }

        public void UpdateCreditScoreHistory(BillSplitReport billSplitReport, int newCreditScore)
        {
            if (string.IsNullOrWhiteSpace(billSplitReport.ReportedUserCnp))
            {
                throw new ArgumentException("Invalid CNP", nameof(billSplitReport.ReportedUserCnp));
            }

            try
            {
                string updateCreditScoreQuery = @"
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

                SqlParameter[] creditScoreParameters = new SqlParameter[]
                {
                    new SqlParameter("@UserCnp", billSplitReport.ReportedUserCnp),
                    new SqlParameter("@NewScore", newCreditScore)
                };

                int rowsAffected = _dbConnection.ExecuteNonQuery(updateCreditScoreQuery, creditScoreParameters, CommandType.Text);

                if (rowsAffected == 0)
                {
                    throw new Exception($"No changes made to credit score history for CNP: {billSplitReport.ReportedUserCnp}");
                }
            }
            catch (SqlException sqlException)
            {
                throw new Exception("Error updating credit score history", sqlException);
            }
            catch (Exception exception)
            {
                throw new Exception("Error updating credit score history", exception);
            }
        }

        public void IncrementNoOfBillSharesPaid(BillSplitReport billSplitReport)
        {
            if (string.IsNullOrWhiteSpace(billSplitReport.ReportedUserCnp))
            {
                throw new ArgumentException("Invalid CNP", nameof(billSplitReport.ReportedUserCnp));
            }

            try
            {
                string updateQuery = "UPDATE Users SET NumberOfBillSharesPaid = NumberOfBillSharesPaid + 1 WHERE Cnp = @UserCnp";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserCnp", billSplitReport.ReportedUserCnp)
                };

                int rowsAffected = _dbConnection.ExecuteNonQuery(updateQuery, parameters, CommandType.Text);

                if (rowsAffected == 0)
                {
                    throw new Exception($"No user found with CNP: {billSplitReport.ReportedUserCnp}");
                }
            }
            catch (SqlException sqlException)
            {
                throw new Exception("Error incrementing bill shares paid", sqlException);
            }
            catch (Exception exception)
            {
                throw new Exception("Error incrementing bill shares paid", exception);
            }
        }

        public int GetDaysOverdue(BillSplitReport billSplitReport)
        {
            return (DateTime.Now - billSplitReport.DateOfTransaction).Days;
        }
    }
}