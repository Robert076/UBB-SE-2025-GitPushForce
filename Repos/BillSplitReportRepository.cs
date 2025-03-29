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
                DataTable? dataTable = dbConn.ExecuteReader("GetBillSplitReports", null, CommandType.StoredProcedure);

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
                        GravityFactor = Convert.ToSingle(row["GravityFactor"])
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
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@BillSplitReportId", SqlDbType.Int) { Value = id }
                };

                int rowsAffected = dbConn.ExecuteNonQuery("DeleteBillSplitReportById", parameters, CommandType.StoredProcedure);

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
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ReportedUserCNP", SqlDbType.VarChar, 16) { Value = billSplitReport.ReportedCNP },
                    new SqlParameter("@ReporterUserCNP", SqlDbType.VarChar, 16) { Value = billSplitReport.ReporterCNP },
                    new SqlParameter("@DateOfTransaction", SqlDbType.DateTime) { Value = billSplitReport.DateTransaction },
                    new SqlParameter("@BillShare", SqlDbType.Float) { Value = billSplitReport.BillShare },
                    new SqlParameter("@GravityFactor", SqlDbType.Float) { Value = billSplitReport.GravityFactor }
                };

                dbConn.ExecuteNonQuery("CreateBillSplitReport", parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating bill split report: {ex.Message}", ex);
            }
        }

        // Check if the reported user made a separate payment to the reporter user.
        public bool CheckLogsForSimilarPayments(BillSplitReport billSplitReport)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ReportedUserCNP", billSplitReport.ReportedCNP),
                new SqlParameter("@ReporterUserCNP", billSplitReport.ReporterCNP),
                new SqlParameter("@DateOfTransaction", billSplitReport.DateTransaction),
                new SqlParameter("@BillShare", billSplitReport.BillShare)
            };

            int count = dbConn.ExecuteScalar<int>("CheckLogsForSimilarPayments", parameters, CommandType.StoredProcedure);
            return count > 0;
        }

        // Get the current balance of the reported user.
        public int GetCurrentBalance(BillSplitReport billSplitReport)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ReportedUserCNP", billSplitReport.ReportedCNP)
            };

            return dbConn.ExecuteScalar<int>("GetCurrentBalance", parameters, CommandType.StoredProcedure);
        }

        // Sum the transactions since the report was initiated.
        public decimal SumTransactionsSinceReport(BillSplitReport billSplitReport)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ReportedUserCNP", billSplitReport.ReportedCNP),
                new SqlParameter("@DateOfTransaction", billSplitReport.DateTransaction)
            };

            return dbConn.ExecuteScalar<decimal>("SumTransactionsSinceReport", parameters, CommandType.StoredProcedure);
        }

        // Check if the user has a history of paying the bill share. (paid at least 3 other bill shares)
        public bool CheckHistoryOfBillShares(BillSplitReport billSplitReport)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ReportedUserCNP", billSplitReport.ReportedCNP)
            };

            int count = dbConn.ExecuteScalar<int>("CheckHistoryOfBillShares", parameters, CommandType.StoredProcedure);
            return count >= 3;
        }

        // Check if the reported user has sent money to the reporter at least 5 times in the last month.
        public bool CheckFrequentTransfers(BillSplitReport billSplitReport)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ReportedUserCNP", billSplitReport.ReportedCNP),
                new SqlParameter("@ReporterUserCNP", billSplitReport.ReporterCNP)
            };

            int count = dbConn.ExecuteScalar<int>("CheckFrequentTransfers", parameters, CommandType.StoredProcedure);
            return count >= 5;
        }

        // Get the number of offenses the reported user has.
        public int GetNumberOfOffenses(BillSplitReport billSplitReport)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ReportedUserCNP", billSplitReport.ReportedCNP)
            };

            return dbConn.ExecuteScalar<int>("GetNumberOfOffenses", parameters, CommandType.StoredProcedure);
        }

        // Get the current credit score of the reported user.
        public int GetCurrentCreditScore(BillSplitReport billSplitReport)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ReportedUserCNP", billSplitReport.ReportedCNP)
            };

            return dbConn.ExecuteScalar<int>("GetCurrentCreditScore", parameters, CommandType.StoredProcedure);
        }

        // Upade the credit score of the reported user, and the credit score history of the reported user.
        public void UpdateCreditScore(BillSplitReport billSplitReport, int newCreditScore)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ReportedUserCNP", billSplitReport.ReportedCNP),
                new SqlParameter("@NewCreditScore", newCreditScore)
            };
            dbConn.ExecuteNonQuery("UpdateCreditScore", parameters, CommandType.StoredProcedure);
            dbConn.ExecuteNonQuery("UpdateCreditScoreHistory", parameters, CommandType.StoredProcedure);
        }
    }
}
