using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Src.Model;
using Src.Services;
using Src.ViewModel;

namespace ViewModelTestings
{
    [TestClass]
    public class InvestmentsViewModelTests
    {
        private Mock<IInvestmentsService> _mockService;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<IInvestmentsService>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullService_ThrowsException()
        {
            // Act
            var vm = new InvestmentsViewModel(null);
        }

        [TestMethod]
        public void Constructor_ValidService_InitializesEmptyPortfolio()
        {
            // Arrange & Act
            var vm = new InvestmentsViewModel(_mockService.Object);

            // Assert
            Assert.IsNotNull(vm.UsersPortofolio);
            Assert.AreEqual(0, vm.UsersPortofolio.Count);
        }

        [TestMethod]
        public void CalculateAndUpdateRiskScore_CallsServiceMethod()
        {
            // Arrange
            var vm = new InvestmentsViewModel(_mockService.Object);

            // Act
            vm.CalculateAndUpdateRiskScore();

            // Assert
            _mockService.Verify(s => s.CalculateAndUpdateRiskScore(), Times.Once);
        }

        [TestMethod]
        public void CalculateAndUpdateROI_CallsServiceMethod()
        {
            // Arrange
            var vm = new InvestmentsViewModel(_mockService.Object);

            // Act
            vm.CalculateAndUpdateROI();

            // Assert
            _mockService.Verify(s => s.CalculateAndUpdateROI(), Times.Once);
        }

        [TestMethod]
        public void CreditScoreUpdateInvestmentsBased_CallsServiceMethod()
        {
            // Arrange
            var vm = new InvestmentsViewModel(_mockService.Object);

            // Act
            vm.CreditScoreUpdateInvestmentsBased();

            // Assert
            _mockService.Verify(s => s.CreditScoreUpdateInvestmentsBased(), Times.Once);
        }

        [TestMethod]
        public void LoadPortfolioSummary_ServiceReturnsData_AddsToCollection()
        {
            // Arrange
            var portfolios = new List<InvestmentPortfolio>
            {
                new InvestmentPortfolio("Ana", "Popescu", 1000, 1200, 0.2m, 3, 5),
                new InvestmentPortfolio("Ion", "Ionescu", 2000, 2100, 0.05m, 5, 2)
            };
            _mockService.Setup(s => s.GetPortfolioSummary()).Returns(portfolios);
            var vm = new InvestmentsViewModel(_mockService.Object);

            // Act
            vm.LoadPortfolioSummary("123");

            // Assert
            Assert.AreEqual(2, vm.UsersPortofolio.Count);
            Assert.AreEqual("Ana", vm.UsersPortofolio[0].FirstName);
            Assert.AreEqual("Ion", vm.UsersPortofolio[1].FirstName);
        }

        [TestMethod]
        public void LoadPortfolioSummary_ServiceThrows_ExceptionHandledAndNoAdd()
        {
            // Arrange
            _mockService.Setup(s => s.GetPortfolioSummary()).Throws(new Exception("fail"));
            var vm = new InvestmentsViewModel(_mockService.Object);

            // Act
            vm.LoadPortfolioSummary("123");

            // Assert
            Assert.AreEqual(0, vm.UsersPortofolio.Count);
        }
    }
}
