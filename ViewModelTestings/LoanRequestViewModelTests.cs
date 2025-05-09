using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Src.Model;
using Src.Services;
using Src.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ViewModelTestings

{
    [TestClass]
    public class LoanRequestViewModelTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenServiceIsNull_ThrowsArgumentNullException()
        {
            // Act
            var viewModel = new LoanRequestViewModel(null);
        }

        [TestMethod]
        public async Task LoadLoanRequests_AddsReturnedRequestsToCollection()
        {
            // Arrange
            var mockService = new Mock<ILoanRequestService>();
            var now = DateTime.Now;

            var testRequests = new List<LoanRequest>
            {
                new LoanRequest(1, "1234567890123", 1000.5f, now, now.AddMonths(6), "Pending")
            };

            mockService.Setup(s => s.GetLoanRequests()).Returns(testRequests);

            var viewModel = new LoanRequestViewModel(mockService.Object);

            // Act
            await viewModel.LoadLoanRequests();

            // Assert
            Assert.AreEqual(1, viewModel.LoanRequests.Count);
            var request = viewModel.LoanRequests[0];
            Assert.AreEqual(1, request.Id);
            Assert.AreEqual("1234567890123", request.UserCnp);
            Assert.AreEqual(1000.5f, request.Amount);
            Assert.AreEqual("Pending", request.Status);
        }

        [TestMethod]
        public async Task LoadLoanRequests_DoesNotThrow_WhenServiceThrowsException()
        {
            // Arrange
            var mockService = new Mock<ILoanRequestService>();
            mockService.Setup(s => s.GetLoanRequests()).Throws(new Exception("Service error"));

            var viewModel = new LoanRequestViewModel(mockService.Object);

            // Act
            await viewModel.LoadLoanRequests();

            // Assert
            Assert.AreEqual(0, viewModel.LoanRequests.Count);
        }

        [TestMethod]
        public async Task LoadLoanRequests_ClearsPreviousData_BeforeAddingNew()
        {
            // Arrange
            var mockService = new Mock<ILoanRequestService>();
            mockService.SetupSequence(s => s.GetLoanRequests())
                .Returns(new List<LoanRequest> { new LoanRequest(1, "111", 1000, DateTime.Now, DateTime.Now.AddDays(30), "Pending") })
                .Returns(new List<LoanRequest> { new LoanRequest(2, "222", 2000, DateTime.Now, DateTime.Now.AddDays(60), "Approved") });

            var viewModel = new LoanRequestViewModel(mockService.Object);

            // Act
            await viewModel.LoadLoanRequests();
            await viewModel.LoadLoanRequests(); // second call

            // Assert
            Assert.AreEqual(1, viewModel.LoanRequests.Count);
            Assert.AreEqual(2, viewModel.LoanRequests[0].Id);
        }
    }
}
