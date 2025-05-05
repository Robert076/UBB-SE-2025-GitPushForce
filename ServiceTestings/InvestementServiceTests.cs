using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Src.Model;
using Src.Repos;
using Src.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceTestings
{
    [TestClass]
    public class InvestmentsServiceTests
    {
        private Mock<IUserRepository> userRepoMock;
        private Mock<IInvestmentsRepository> investRepoMock;
        private InvestmentsService service;

        [TestInitialize]
        public void Setup()
        {
            userRepoMock = new Mock<IUserRepository>();
            investRepoMock = new Mock<IInvestmentsRepository>();
            service = new InvestmentsService(userRepoMock.Object, investRepoMock.Object);
        }

        [TestMethod]
        public void CalculateAndUpdateRiskScore_ShouldUpdateRiskCorrectly()
        {
            var user = new User { Cnp = "1", Income = 10000, RiskScore = 50 };
            var investments = new List<Investment>
            {
                new Investment { InvestorCnp = "1", AmountInvested = 1000, AmountReturned = 900, InvestmentDate = DateTime.Today },
                new Investment { InvestorCnp = "1", AmountInvested = 1000, AmountReturned = 1200, InvestmentDate = DateTime.Today.AddDays(-1) },
                new Investment { InvestorCnp = "1", AmountInvested = 1000, AmountReturned = 800, InvestmentDate = DateTime.Today.AddDays(-2) }
            };

            userRepoMock.Setup(x => x.GetUsers()).Returns(new List<User> { user });
            investRepoMock.Setup(x => x.GetInvestmentsHistory()).Returns(investments);

            service.CalculateAndUpdateRiskScore();

            userRepoMock.Verify(x => x.UpdateUserRiskScore("1", It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void CalculateAndUpdateROI_ShouldSetROI_WhenClosedInvestmentsExist()
        {
            var user = new User { Cnp = "1" };
            var investments = new List<Investment>
            {
                new Investment { InvestorCnp = "1", AmountInvested = 100, AmountReturned = 150 },
                new Investment { InvestorCnp = "1", AmountInvested = 200, AmountReturned = 100 }
            };

            userRepoMock.Setup(x => x.GetUsers()).Returns(new List<User> { user });
            investRepoMock.Setup(x => x.GetInvestmentsHistory()).Returns(investments);

            service.CalculateAndUpdateROI();

            userRepoMock.Verify(x => x.UpdateUserROI("1", It.Is<decimal>(v => v > 0)), Times.Once);
        }

        [TestMethod]
        public void CalculateAndUpdateROI_ShouldSetROIFallback_WhenNoClosedInvestments()
        {
            var user = new User { Cnp = "1" };
            var investments = new List<Investment>
            {
                new Investment { InvestorCnp = "1", AmountInvested = 100, AmountReturned = -1 }
            };

            userRepoMock.Setup(x => x.GetUsers()).Returns(new List<User> { user });
            investRepoMock.Setup(x => x.GetInvestmentsHistory()).Returns(investments);

            service.CalculateAndUpdateROI();

            userRepoMock.Verify(x => x.UpdateUserROI("1", 1), Times.Once);
        }

        [TestMethod]
        public void CreditScoreUpdateInvestmentsBased_ShouldUpdateCreditScore()
        {
            var user = new User { Cnp = "1", RiskScore = 20, CreditScore = 500, ROI = 1.5m };
            userRepoMock.Setup(x => x.GetUsers()).Returns(new List<User> { user });

            service.CreditScoreUpdateInvestmentsBased();

            userRepoMock.Verify(x => x.UpdateUserCreditScore("1", It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void GetPortfolioSummary_ShouldReturnCorrectSummaries()
        {
            var user = new User { Cnp = "1", FirstName = "John", LastName = "Doe", RiskScore = 50 };
            var investments = new List<Investment>
    {
        new Investment { InvestorCnp = "1", AmountInvested = 100, AmountReturned = 150 },
        new Investment { InvestorCnp = "1", AmountInvested = 200, AmountReturned = 300 }
    };

            var userRepo = new Mock<IUserRepository>();
            var investRepo = new Mock<IInvestmentsRepository>();

            userRepo.Setup(x => x.GetUsers()).Returns(new List<User> { user });
            investRepo.Setup(x => x.GetInvestmentsHistory()).Returns(investments);

            var service = new InvestmentsService(userRepo.Object, investRepo.Object);

            var portfolios = service.GetPortfolioSummary();

            Assert.AreEqual(1, portfolios.Count);
            Assert.AreEqual("John", portfolios[0].FirstName);
            Assert.AreEqual(2, portfolios[0].NumberOfInvestments);
            Assert.AreEqual(100m + 200m, portfolios[0].TotalAmountInvested);
            Assert.AreEqual(150m + 300m, portfolios[0].TotalAmountReturned);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldThrow_WhenUserRepoIsNull()
        {
            new InvestmentsService(null, investRepoMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldThrow_WhenInvestmentRepoIsNull()
        {
            new InvestmentsService(userRepoMock.Object, null);
        }
    }
}
