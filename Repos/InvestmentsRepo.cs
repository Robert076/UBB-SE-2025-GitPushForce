using Microsoft.Data.SqlClient;
using src.Data;
using src.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace src.Repos
{
    public class InvestmentsRepository: IInvestmentsRepository
    {
        private readonly DatabaseConnection _dbConnection;

        public InvestmentsRepository(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<Investment> GetInvestmentsHistory()
        {
            try
            {
                const string SelectQuery = "SELECT Id, InvestorCnp, Details, AmountInvested, AmountReturned, InvestmentDate FROM Investments";
                DataTable investmentsDataTable = _dbConnection.ExecuteReader(SelectQuery, null, CommandType.Text);

                if (investmentsDataTable == null || investmentsDataTable.Rows.Count == 0)
                {
                    throw new Exception("Investments history table is empty");
                }

                List<Investment> investmentsHistory = new List<Investment>();

                foreach (DataRow row in investmentsDataTable.Rows)
                {
                    Investment investment = new Investment(
                        id: Convert.ToInt32(row["Id"]),
                        investorCnp: row["InvestorCnp"].ToString(),
                        details: row["Details"].ToString(),
                        amountInvested: Convert.ToSingle(row["AmountInvested"]),
                        amountReturned: Convert.ToSingle(row["AmountReturned"]),
                        investmentDate: Convert.ToDateTime(row["InvestmentDate"])
                    );

                    investmentsHistory.Add(investment);
                }

                return investmentsHistory;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error retrieving investments: {exception.Message}");
            }
        }

        public void AddInvestment(Investment investment)
        {
            if (investment == null)
            {
                throw new ArgumentNullException(nameof(investment));
            }

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@InvestorCnp", investment.InvestorCnp),
                    new SqlParameter("@Details", investment.Details),
                    new SqlParameter("@AmountInvested", investment.AmountInvested),
                    new SqlParameter("@AmountReturned", investment.AmountReturned),
                    new SqlParameter("@InvestmentDate", investment.InvestmentDate)
                };

                const string InsertInvestmentQuery = @"INSERT INTO Investments 
                              (InvestorCnp, Details, AmountInvested, AmountReturned, InvestmentDate) 
                              VALUES 
                              (@InvestorCnp, @Details, @AmountInvested, @AmountReturned, @InvestmentDate)";

                int rowsAffected = _dbConnection.ExecuteNonQuery(InsertInvestmentQuery, parameters, CommandType.Text);

                if (rowsAffected == 0)
                {
                    throw new Exception("Failed to add investment to database");
                }
            }
            catch (Exception exception)
            {
                throw new Exception($"Error adding investment: {exception.Message}");
            }
        }

        public void UpdateInvestment(int investmentId, string investorCnp, float amountReturned)
        {
            if (investmentId <= 0)
            {
                throw new ArgumentException("Invalid investment ID", nameof(investmentId));
            }

            if (string.IsNullOrWhiteSpace(investorCnp))
            {
                throw new ArgumentException("Investor CNP cannot be empty", nameof(investorCnp));
            }

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@InvestmentId", investmentId),
                    new SqlParameter("@InvestorCnp", investorCnp),
                    new SqlParameter("@AmountReturned", amountReturned)
                };

                const string SelectQuery = "SELECT Id, InvestorCnp, AmountReturned FROM Investments WHERE Id = @InvestmentId AND InvestorCnp = @InvestorCnp";
                DataTable dataTable = _dbConnection.ExecuteReader(SelectQuery, parameters, CommandType.Text);

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    throw new Exception("Specified investment not found");
                }

                DataRow row = dataTable.Rows[0];

                if (Convert.ToSingle(row["AmountReturned"]) != -1)
                {
                    throw new Exception("Investment return has already been processed");
                }

                if (row["InvestorCnp"].ToString() != investorCnp)
                {
                    throw new Exception("Investor CNP does not match investment record");
                }

                const string UpdateQuery = "UPDATE Investments SET AmountReturned = @AmountReturned WHERE Id = @InvestmentId AND AmountReturned = -1";
                int rowsAffected = _dbConnection.ExecuteNonQuery(UpdateQuery, parameters, CommandType.Text);

                if (rowsAffected == 0)
                {
                    throw new Exception("Failed to update investment return in database");
                }
            }
            catch (Exception exception)
            {
                throw new Exception($"Error updating investment: {exception.Message}");
            }
        }
    }
}