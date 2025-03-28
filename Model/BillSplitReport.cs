using src.Data;
using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace src.Model
{
    public class BillSplitReport
    {
        private int _id;
        private string _reportedCNP;
        private string _reporterCNP;
        private DateTime _dateTransaction;
        private float _billShare;
        private float _gravityFactor;

        public BillSplitReport(int id, string reportedCNP, string reporterCNP, DateTime dateTransaction, float billShare, float gravityFactor)
        {
            _id = id;
            _reportedCNP = reportedCNP;
            _reporterCNP = reporterCNP;
            _dateTransaction = dateTransaction;
            _billShare = billShare;
            _gravityFactor = gravityFactor;
        }

        public BillSplitReport()
        {
            _id = 0;
            _reportedCNP = string.Empty;
            _reporterCNP = string.Empty;
            _dateTransaction = DateTime.Now;
            _billShare = 0;
            _gravityFactor = 0;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string ReportedCNP
        {
            get { return _reportedCNP; }
            set { _reportedCNP = value; }
        }

        public string ReporterCNP
        {
            get { return _reporterCNP; }
            set { _reporterCNP = value; }
        }

        public DateTime DateTransaction
        {
            get { return _dateTransaction; }
            set { _dateTransaction = value; }
        }

        public float BillShare
        {
            get { return _billShare; }
            set { _billShare = value; }
        }

        public float GravityFactor
        {
            get { return _gravityFactor; }
            set { _gravityFactor = value; }
        }

        // Check if the reported user made a separate mayment to re reporter user.
        public bool CheckLogsForSimilarPayments(string userCNP, DateTime date) {
            var databaseConnection = new DatabaseConnection();
            string query = @"
                SELECT COUNT(*)
                FROM TransactionLogs
                WHERE SenderCNP = @ReportedUserCNP
                  AND ReceiverCNP = @ReporterUserCNP
                  AND TransactionDate > @DateOfTransaction
                  AND Amount = @BillShare
                  AND (TransactionDescription LIKE '%bill%' OR TransactionDescription LIKE '%share%' OR TransactionDescription LIKE '%split%')
                  AND TransactionType != 'Bill Split';
                ";
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@ReportedUserCNP", _reportedCNP),
                new SqlParameter("@ReporterUserCNP", _reporterCNP),
                new SqlParameter("@DateOfTransaction", _dateTransaction),
                new SqlParameter("@BillShare", _billShare)
            };

            int count = databaseConnection.ExecuteScalar<int>(query, parameters, CommandType.Text);

            return count > 0;
        }

        // Get the current balance of the reported user.
        private decimal GetCurrentBalance()
        {
            var databaseConnection = new DatabaseConnection();
            string query = "SELECT Balance FROM Users WHERE CNP = @ReportedUserCNP";
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@ReportedUserCNP", _reportedCNP)
            };

            return databaseConnection.ExecuteScalar<decimal>(query, parameters, CommandType.Text);
        }

        // Sum the transactions since the report was initiated.
        private decimal SumTransactionsSinceReport()
        {
            var databaseConnection = new DatabaseConnection();
            string query = @"
                SELECT SUM(Amount)
                FROM TransactionLogs
                WHERE SenderCNP = @ReportedUserCNP
                  AND TransactionDate > @DateOfTransaction;
                ";
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@ReportedUserCNP", _reportedCNP),
                new SqlParameter("@DateOfTransaction", _dateTransaction)
            };

            return databaseConnection.ExecuteScalar<decimal>(query, parameters, CommandType.Text);
        }

        // Check if the user could have paid the bill share.
        public bool CouldHavePaidBillShare()
        {
            decimal currentBalance = GetCurrentBalance();
            decimal transactionsSum = SumTransactionsSinceReport();

            return currentBalance + transactionsSum >= (decimal)_billShare;
        }

        // Check if the user has a history of paying the bill share. (payed at least 3 other bill shares)
        public bool CheckHistoryOfBillShares()
        {
            var databaseConnection = new DatabaseConnection();
            string query = @"SELECT NoOfBillSharesPaid FROM Users WHERE CNP = @ReportedUserCNP;";
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@ReportedUserCNP", _reportedCNP) 
            };
            int count = databaseConnection.ExecuteScalar<int>(query, parameters, CommandType.Text);
            return count >= 3;
        }

        // Check if the reported user has sent money to the reporter at least 5 times in the last month.
        public bool CheckFrequentTransfers()
        {
            var databaseConnection = new DatabaseConnection();
            string query = @"
                SELECT COUNT(*)
                FROM TransactionLogs
                WHERE SenderCNP = @ReportedUserCNP
                  AND ReceiverCNP = @ReporterUserCNP
                  AND TransactionDate >= DATEADD(month, -1, GETDATE());
                ";
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@ReportedUserCNP", _reportedCNP),
                new SqlParameter("@ReporterUserCNP", _reporterCNP)
            };

            int count = databaseConnection.ExecuteScalar<int>(query, parameters, CommandType.Text);

            return count >= 5;
        }

        // Get the number of offenses the reported user has.
        public int GetNumberOfOffenses()
        {
            var databaseConnection = new DatabaseConnection();
            string query = @"SELECT NoOffenses FROM Users WHERE CNP = @ReportedUserCNP;";
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@ReportedUserCNP", _reportedCNP)
            };
            return databaseConnection.ExecuteScalar<int>(query, parameters, CommandType.Text);
        }

        // Get the current credit score of the reported user.
        public int GetCurrentCreditScore()
        {
            var databaseConnection = new DatabaseConnection();
            string query = @"SELECT CreditScore FROM Users WHERE CNP = @ReportedUserCNP;";
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@ReportedUserCNP", _reportedCNP)
            };
            return databaseConnection.ExecuteScalar<int>(query, parameters, CommandType.Text);
        }

        // Compute gravcity factor based on the rules.
        public float ComputeGravityFactor()
        {
            // Calculate the number of days past due
            int daysPastDue = (DateTime.Now - _dateTransaction).Days;

            // Calculate the time factor
            float timeFactor = Math.Min(50, (daysPastDue - 1) * 50 / 20.0f);

            // Calculate the amount factor
            float amountFactor = Math.Min(50, (_billShare - 1) * 50 / 999.0f);

            // Calculate the initial gravity factor
            float gravityFactor = timeFactor + amountFactor;

            // Adjust gravity factor based on checks
            if (CouldHavePaidBillShare())
            {
                gravityFactor += gravityFactor * 0.1f;
            }
            if (CheckHistoryOfBillShares())
            {
                gravityFactor -= gravityFactor * 0.05f;
            }
            if (CheckFrequentTransfers())
            {
                gravityFactor -= gravityFactor * 0.05f;
            }

            // Add floor(10% of the number of offenses)%
            int numberOfOffenses = GetNumberOfOffenses();
            gravityFactor += (float)Math.Floor(numberOfOffenses * 0.1f);

            return gravityFactor;
        }

        // Copute the new credit score based on the gravity factor.
        public int ComputeNewCreditScore()
        {
            return (int)Math.Floor(GetCurrentCreditScore() - 0.2f * ComputeGravityFactor());
        }

    }
}