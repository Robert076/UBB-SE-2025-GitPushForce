using src.Data;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using src.Model;

namespace src.Repos
{
    public class BillSplitReportRepository
    {
        private readonly DatabaseConnection dbConn;

        public BillSplitReportRepository(DatabaseConnection dbConn)
        {
            this.dbConn = dbConn;
        }
        public DatabaseConnection getDbConn()
        {
            return dbConn;
        }


        public List<BillSplitReport> GetBillSplitReports()
        {
            try
            {
                string sqlQuery = "SELECT * FROM BillSplitReports";

                DataTable? dataTable = dbConn.ExecuteReader(sqlQuery, null, CommandType.Text);

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    throw new Exception("Bill split reports table is empty");
                }

                List<BillSplitReport> billSplitReports = new List<BillSplitReport>();

                foreach (DataRow row in dataTable.Rows)
                {
                    BillSplitReport billSplitReport = new BillSplitReport
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        ReportedCNP = row["ReportedUserCNP"].ToString() ?? "",
                        ReporterCNP = row["ReporterUserCNP"].ToString() ?? "",
                        DateTransaction = Convert.ToDateTime(row["DateOfTransaction"]),
                        BillShare = Convert.ToSingle(row["BillShare"]),
                    };

                    billSplitReports.Add(billSplitReport);
                }

                return billSplitReports;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving bill split reports", ex);
            }
        }


        public void DeleteBillSplitReport(int id)
        {
            try
            {
                string sqlQuery = "DELETE FROM BillSplitReports WHERE Id = @BillSplitReportId";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@BillSplitReportId", SqlDbType.Int) { Value = id }
                };

                int rowsAffected = dbConn.ExecuteNonQuery(sqlQuery, parameters, CommandType.Text);

                if (rowsAffected == 0)
                {
                    throw new Exception($"No bill split report found with Id {id}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting bill split report: {ex.Message}", ex);
            }
        }


        public void CreateBillSplitReport(BillSplitReport billSplitReport)
        {
            try
            {
                string sqlQuery = @"
            INSERT INTO BillSplitReports (ReportedUserCNP, ReporterUserCNP, DateOfTransaction, BillShare)
            VALUES (@ReportedUserCNP, @ReporterUserCNP, @DateOfTransaction, @BillShare);
        ";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@ReportedUserCNP", SqlDbType.VarChar, 16) { Value = billSplitReport.ReportedCNP },
            new SqlParameter("@ReporterUserCNP", SqlDbType.VarChar, 16) { Value = billSplitReport.ReporterCNP },
            new SqlParameter("@DateOfTransaction", SqlDbType.DateTime) { Value = billSplitReport.DateTransaction },
            new SqlParameter("@BillShare", SqlDbType.Float) { Value = billSplitReport.BillShare },
                };

                dbConn.ExecuteNonQuery(sqlQuery, parameters, CommandType.Text);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating bill split report: {ex.Message}", ex);
            }
        }


        // Check if the reported user made a separate payment to the reporter user.
        public bool CheckLogsForSimilarPayments(BillSplitReport billSplitReport)
        {
            string query = @"
        SELECT COUNT(*)
        FROM TransactionLogs
        WHERE SenderCNP = @ReportedUserCNP
          AND ReceiverCNP = @ReporterUserCNP
          AND TransactionDate > @DateOfTransaction
          AND Amount = @BillShare
          AND (TransactionDescription LIKE '%bill%' OR TransactionDescription LIKE '%share%' OR TransactionDescription LIKE '%split%')
          AND TransactionType != 'Bill Split'";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@ReportedUserCNP", billSplitReport.ReportedCNP),
        new SqlParameter("@ReporterUserCNP", billSplitReport.ReporterCNP),
        new SqlParameter("@DateOfTransaction", billSplitReport.DateTransaction),
        new SqlParameter("@BillShare", billSplitReport.BillShare)
            };

            int count = dbConn.ExecuteScalar<int>(query, parameters, CommandType.Text);
            return count > 0;
        }


        // Get the current balance of the reported user.
        public int GetCurrentBalance(BillSplitReport billSplitReport)
        {
            try
            {
                string query = @"
            SELECT Balance 
            FROM Users 
            WHERE CNP = @ReportedUserCNP";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@ReportedUserCNP", billSplitReport.ReportedCNP)
                };

                return dbConn.ExecuteScalar<int>(query, parameters, CommandType.Text);
            }
            catch (SqlException sqlEx)
            {
                
                throw new Exception($"Error getting current balance: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {

                throw new Exception($"Error getting current balance: {ex.Message}", ex);
            }
        }


        // Sum the transactions since the report was initiated.
        public decimal SumTransactionsSinceReport(BillSplitReport billSplitReport)
        {
            if (string.IsNullOrWhiteSpace(billSplitReport.ReportedCNP))
            {
                throw new ArgumentException("Invalid CNP", nameof(billSplitReport.ReportedCNP));
            }

            try
            {
                string query = @"
            SELECT SUM(Amount)
            FROM TransactionLogs
            WHERE SenderCNP = @ReportedUserCNP
              AND TransactionDate > @DateOfTransaction";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@ReportedUserCNP", SqlDbType.VarChar, 16) { Value = billSplitReport.ReportedCNP },
            new SqlParameter("@DateOfTransaction", SqlDbType.Date) { Value = billSplitReport.DateTransaction }
                };

                decimal result = dbConn.ExecuteScalar<decimal>(query, parameters, CommandType.Text);

                return result;
            }
            catch (SqlException sqlEx)
            {
               
                throw new Exception("An error occurred while summing the transactions since the report date", sqlEx);
            }
            catch (Exception ex)
            {
                
                throw new Exception("An unexpected error occurred while summing the transactions since the report date", ex);
            }
        }


        // Check if the user has a history of paying the bill share. (paid at least 3 other bill shares)
        public bool CheckHistoryOfBillShares(BillSplitReport billSplitReport)
        {
            if (string.IsNullOrWhiteSpace(billSplitReport.ReportedCNP))
            {
                throw new ArgumentException("Invalid CNP", nameof(billSplitReport.ReportedCNP));
            }

            try
            {
                string query = "SELECT NoOfBillSharesPaid FROM Users WHERE CNP = @ReportedUserCNP";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@ReportedUserCNP", SqlDbType.VarChar, 16) { Value = billSplitReport.ReportedCNP }
                };

                int noOfBillSharesPaid = dbConn.ExecuteScalar<int>(query, parameters, CommandType.Text);

                return noOfBillSharesPaid >= 3;
            }
            catch (SqlException sqlEx)
            {
                
                throw new Exception("An error occurred while checking the user's bill share history", sqlEx);
            }
            catch (Exception ex)
            {
                
                throw new Exception("An unexpected error occurred while checking the user's bill share history", ex);
            }
        }


        // Check if the reported user has sent money to the reporter at least 5 times in the last month.
        public bool CheckFrequentTransfers(BillSplitReport billSplitReport)
        {
            if (string.IsNullOrWhiteSpace(billSplitReport.ReportedCNP) || string.IsNullOrWhiteSpace(billSplitReport.ReporterCNP))
            {
                throw new ArgumentException("Invalid CNPs", nameof(billSplitReport));
            }

            try
            {
                
                string query = @"
            SELECT COUNT(*)
            FROM TransactionLogs
            WHERE SenderCNP = @ReportedUserCNP
              AND ReceiverCNP = @ReporterUserCNP
              AND TransactionDate >= DATEADD(month, -1, GETDATE())";

                
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@ReportedUserCNP", SqlDbType.VarChar, 16) { Value = billSplitReport.ReportedCNP },
            new SqlParameter("@ReporterUserCNP", SqlDbType.VarChar, 16) { Value = billSplitReport.ReporterCNP }
                };

               
                int count = dbConn.ExecuteScalar<int>(query, parameters, CommandType.Text);

                return count >= 5;
            }
            catch (SqlException sqlEx)
            {
               
                throw new Exception("An error occurred while checking for frequent transfers", sqlEx);
            }
            catch (Exception ex)
            {
                
                throw new Exception("An unexpected error occurred while checking for frequent transfers", ex);
            }
        }

        // Get the number of offenses the reported user has.
        public int GetNumberOfOffenses(BillSplitReport billSplitReport)
        {
            if (string.IsNullOrWhiteSpace(billSplitReport.ReportedCNP))
            {
                throw new ArgumentException("Invalid CNP", nameof(billSplitReport.ReportedCNP));
            }

            try
            {
                string query = "SELECT NoOffenses FROM Users WHERE CNP = @ReportedUserCNP";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@ReportedUserCNP", SqlDbType.VarChar, 16) { Value = billSplitReport.ReportedCNP }
                };

                int numberOfOffenses = dbConn.ExecuteScalar<int>(query, parameters, CommandType.Text);

                return numberOfOffenses;
            }
            catch (SqlException sqlEx)
            { 
                throw new Exception("An error occurred while retrieving the number of offenses", sqlEx);
            }
            catch (Exception ex)
            {
                
                throw new Exception("An unexpected error occurred while retrieving the number of offenses", ex);
            }
        }

        // Get the current credit score of the reported user.
        public int GetCurrentCreditScore(BillSplitReport billSplitReport)
        {
            if (string.IsNullOrWhiteSpace(billSplitReport.ReportedCNP))
            {
                throw new ArgumentException("Invalid CNP", nameof(billSplitReport.ReportedCNP));
            }

            try
            {
                string query = "SELECT CreditScore FROM Users WHERE CNP = @ReportedUserCNP";

      
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@ReportedUserCNP", SqlDbType.VarChar, 16) { Value = billSplitReport.ReportedCNP }
                };

                int creditScore = dbConn.ExecuteScalar<int>(query, parameters, CommandType.Text);

                return creditScore;
            }
            catch (SqlException sqlEx)
            {
                
                throw new Exception("An error occurred while retrieving the current credit score", sqlEx);
            }
            catch (Exception ex)
            {
               
                throw new Exception("An unexpected error occurred while retrieving the current credit score", ex);
            }
        }


        // Upade the credit score of the reported user
        public void UpdateCreditScore(BillSplitReport billSplitReport, int newCreditScore)
        {
            if (string.IsNullOrWhiteSpace(billSplitReport.ReportedCNP))
            {
                throw new ArgumentException("Invalid CNP", nameof(billSplitReport.ReportedCNP));
            }

            try
            {
                string query = "UPDATE Users SET CreditScore = @NewCreditScore WHERE CNP = @UserCNP";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@UserCNP", SqlDbType.VarChar, 16) { Value = billSplitReport.ReportedCNP },
            new SqlParameter("@NewCreditScore", SqlDbType.Int) { Value = newCreditScore }
                };

                int rowsAffected = dbConn.ExecuteNonQuery(query, parameters, CommandType.Text);

                if (rowsAffected == 0)
                {
                    throw new Exception($"No user found with CNP: {billSplitReport.ReportedCNP}");
                }
            }
            catch (SqlException sqlEx)
            {
                
                throw new Exception("An error occurred while updating the user's credit score", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the user's credit score", ex);
            }
        }


        // Upade the credit score history of the reported user
        public void UpdateCreditScoreHistory(BillSplitReport billSplitReport, int newCreditScore)
        {
            if (string.IsNullOrWhiteSpace(billSplitReport.ReportedCNP))
            {
                throw new ArgumentException("Invalid CNP", nameof(billSplitReport.ReportedCNP));
            }

            try
            {
                string query = @"
            IF EXISTS (SELECT 1 FROM CreditScoreHistory WHERE UserCNP = @UserCNP AND Date = CAST(GETDATE() AS DATE))
            BEGIN
                UPDATE CreditScoreHistory
                SET Score = @NewScore
                WHERE UserCNP = @UserCNP AND Date = CAST(GETDATE() AS DATE);
            END
            ELSE
            BEGIN
                INSERT INTO CreditScoreHistory (UserCNP, Date, Score)
                VALUES (@UserCNP, CAST(GETDATE() AS DATE), @NewScore);
            END";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@UserCNP", SqlDbType.VarChar, 16) { Value = billSplitReport.ReportedCNP },
            new SqlParameter("@NewScore", SqlDbType.Int) { Value = newCreditScore }
                };

                int rowsAffected = dbConn.ExecuteNonQuery(query, parameters, CommandType.Text);

                if (rowsAffected == 0)
                {
                    throw new Exception($"No changes were made to the credit score history for user with CNP: {billSplitReport.ReportedCNP}");
                }
            }
            catch (SqlException sqlEx)
            {
                
                throw new Exception("An error occurred while updating the credit score history", sqlEx);
            }
            catch (Exception ex)
            {
                
                throw new Exception("An unexpected error occurred while updating the credit score history", ex);
            }
        }


        // Increment the number of bill shares paid by the reported user
        public void IncrementNoOfBillSharesPaid(BillSplitReport billSplitReport)
        {
            if (string.IsNullOrWhiteSpace(billSplitReport.ReportedCNP))
            {
                throw new ArgumentException("Invalid CNP", nameof(billSplitReport.ReportedCNP));
            }

            try
            {
                string query = "UPDATE Users SET NoOffenses = NoOffenses + 1 WHERE CNP = @UserCNP";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@UserCNP", SqlDbType.VarChar, 16) { Value = billSplitReport.ReportedCNP }
                };

                int rowsAffected = dbConn.ExecuteNonQuery(query, parameters, CommandType.Text);

                if (rowsAffected == 0)
                {
                    throw new Exception($"No user found with CNP: {billSplitReport.ReportedCNP}");
                }
            }
            catch (SqlException sqlEx)
            {
                
                throw new Exception("An error occurred while updating the number of offenses", sqlEx);
            }
            catch (Exception ex)
            {
               
                throw new Exception("An unexpected error occurred while updating the number of offenses", ex);
            }
        }


        public int GetDaysOverdue(BillSplitReport billSplitReport)
        {
            return (DateTime.Now - billSplitReport.DateTransaction).Days;
        }
    }
}
