using Microsoft.Data.SqlClient;
using src.Data;
using src.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.WebUI;

namespace src.Repos
{
    public class InvestmentsRepository
    {
        private readonly DatabaseConnection dbConn;

        public InvestmentsRepository(DatabaseConnection dbConn)
        {
            this.dbConn = dbConn;
        }

        public List<Investment> GetInvestmentsHistory()
        {
            try
            {
                DataTable? dataTable = dbConn.ExecuteReader("GetInvestmentsHistory", null, CommandType.StoredProcedure);

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    throw new Exception("Investments history table is empty");
                }

                List<Investment> investmentsHistory = new List<Investment>();

                foreach (DataRow row in dataTable.Rows)
                {
                    Investment investment = new Investment(
                        Convert.ToInt32(row["ID"]),
                        row["InvestorCNP"].ToString(),
                        row["Details"].ToString(),
                        Convert.ToSingle(row["AmountInvested"]),
                        Convert.ToSingle(row["AmountReturned"]),
                        Convert.ToDateTime(row["InvestmentDate"])
                    );

                    investmentsHistory.Add(investment);
                }

                return investmentsHistory;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error - GetInvestments: {exception.Message}");
            }
        }

        public void AddInvestment(Investment investment)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@InvestorCNP", investment.InvestorCNP),
                    new SqlParameter("@Details", investment.Details),
                    new SqlParameter("@AmountInvested", investment.AmountInvested),
                    new SqlParameter("@AmountReturned", investment.AmountReturned),
                    new SqlParameter("@InvestmentDate", investment.InvestmentDate)
                };

                int rowsAffected = dbConn.ExecuteNonQuery("AddInvestment", parameters, CommandType.StoredProcedure);

                if (rowsAffected == 0)
                {
                    throw new Exception("Failed to add the investment to the database.");
                }
            }
            catch (Exception exception)
            {
                throw new Exception($"Error - AddInvestment: {exception.Message}");
            }
        }

        public void UpdateInvestment(int investmentId, string investorCNP, float amountReturned)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@InvestmentId", investmentId),
                    new SqlParameter("@InvestorCNP", investorCNP),
                    new SqlParameter("@AmountReturned", amountReturned)
                };

                // Check if the investment exists and meets the conditions
                DataTable? dataTable = dbConn.ExecuteReader("CheckInvestmentStatus", parameters, CommandType.StoredProcedure);

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    throw new Exception("Investment not found.");
                }

                DataRow row = dataTable.Rows[0];

                // Validate conditions
                if (Convert.ToSingle(row["AmountReturned"]) != -1)
                {
                    throw new Exception("This transaction was already finished.");
                }

                if (row["InvestorCNP"].ToString() != investorCNP)
                {
                    throw new Exception("Investor CNP mismatch.");
                }

                // Perform the update
                int rowsAffected = dbConn.ExecuteNonQuery("UpdateInvestment", parameters, CommandType.StoredProcedure);

                if (rowsAffected == 0)
                {
                    throw new Exception("Failed to update the investment in the database.");
                }
            }
            catch (Exception exception)
            {
                throw new Exception($"Error - UpdateInvestment: {exception.Message}");
            }
        }
    }
}