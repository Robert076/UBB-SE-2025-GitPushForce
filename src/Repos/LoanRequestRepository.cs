using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Src.Data;
using Src.Model;

namespace Src.Repos
{
    public class LoanRequestRepository : ILoanRequestRepository
    {
        private readonly DatabaseConnection dbConnection;

        public LoanRequestRepository(DatabaseConnection databaseConnection)
        {
            dbConnection = databaseConnection;
        }

        public List<LoanRequest> GetLoanRequests()
        {
            try
            {
                const string SelectQuery = "SELECT Id, UserCnp, Amount, ApplicationDate, RepaymentDate, Status FROM LoanRequest";
                DataTable dataTable = dbConnection.ExecuteReader(SelectQuery, null, CommandType.Text);

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    return new List<LoanRequest>();
                }

                List<LoanRequest> loanRequests = new List<LoanRequest>();

                foreach (DataRow row in dataTable.Rows)
                {
                    loanRequests.Add(new LoanRequest(
                        requestId: Convert.ToInt32(row["Id"]),
                        userCnp: row["UserCnp"].ToString(),
                        amount: Convert.ToSingle(row["Amount"]),
                        applicationDate: Convert.ToDateTime(row["ApplicationDate"]),
                        repaymentDate: Convert.ToDateTime(row["RepaymentDate"]),
                        status: row["Status"].ToString()));
                }

                return loanRequests;
            }
            catch (Exception exception)
            {
                throw new Exception("Error retrieving loan requests", exception);
            }
        }

        public List<LoanRequest> GetUnsolvedLoanRequests()
        {
            try
            {
                const string SelectQuery = @"
                    SELECT Id, UserCnp, Amount, ApplicationDate, RepaymentDate, Status 
                    FROM LoanRequest 
                    WHERE Status <> 'Solved' OR Status IS NULL";

                DataTable dataTable = dbConnection.ExecuteReader(SelectQuery, null, CommandType.Text);

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    return new List<LoanRequest>();
                }

                List<LoanRequest> loanRequests = new List<LoanRequest>();

                foreach (DataRow row in dataTable.Rows)
                {
                    loanRequests.Add(new LoanRequest(
                        requestId: Convert.ToInt32(row["Id"]),
                        userCnp: row["UserCnp"].ToString(),
                        amount: Convert.ToSingle(row["Amount"]),
                        applicationDate: Convert.ToDateTime(row["ApplicationDate"]),
                        repaymentDate: Convert.ToDateTime(row["RepaymentDate"]),
                        status: row["Status"].ToString()));
                }

                return loanRequests;
            }
            catch (Exception exception)
            {
                throw new Exception("Error retrieving unsolved loan requests", exception);
            }
        }

        public void SolveLoanRequest(int loanRequestId)
        {
            if (loanRequestId <= 0)
            {
                throw new ArgumentException("Invalid loan request ID", nameof(loanRequestId));
            }

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@LoanRequestId", loanRequestId)
                };

                const string UpdateQuery = "UPDATE LoanRequest SET Status = 'Solved' WHERE Id = @LoanRequestId";
                int rowsAffected = dbConnection.ExecuteNonQuery(UpdateQuery, parameters, CommandType.Text);

                if (rowsAffected == 0)
                {
                    throw new Exception($"No loan request found with ID: {loanRequestId}");
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error solving loan request", exception);
            }
        }

        public void DeleteLoanRequest(int loanRequestId)
        {
            if (loanRequestId <= 0)
            {
                throw new ArgumentException("Invalid loan request ID", nameof(loanRequestId));
            }

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@LoanRequestId", loanRequestId)
                };

                const string DeleteQuery = "DELETE FROM LoanRequest WHERE Id = @LoanRequestId";
                int rowsAffected = dbConnection.ExecuteNonQuery(DeleteQuery, parameters, CommandType.Text);

                if (rowsAffected == 0)
                {
                    throw new Exception($"No loan request found with ID: {loanRequestId}");
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error deleting loan request", exception);
            }
        }
    }
}