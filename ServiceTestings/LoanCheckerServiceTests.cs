using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Src.Helpers; 
using System;
using Src.Services;

namespace ServiceTestings
{
    [TestClass]
    public class LoanCheckerServiceTests
    {
        private Mock<ILoanService> mockLoanService;
        private Mock<IDispatchTimer> mockTimer;
        private LoanCheckerService loanCheckerService;

        [TestInitialize]
        public void TestInitialize()
        {
            // Mock the ILoanService
            mockLoanService = new Mock<ILoanService>();

            // Mock the ITimer
            mockTimer = new Mock<IDispatchTimer>();

            // Initialize LoanCheckerService with mock timer
            loanCheckerService = new LoanCheckerService(mockLoanService.Object, mockTimer.Object);
        }

        [TestMethod]
        public void LoanCheckerService_Start_ShouldCallCheckLoansAtLeastOnce()
        {
            // Arrange
            loanCheckerService.Start();

            // Simulate the timer tick
            mockTimer.Raise(t => t.Tick += null, EventArgs.Empty);

            // Assert: Verify that CheckLoans was called at least once
            mockLoanService.Verify(service => service.CheckLoans(), Times.Once, "CheckLoans was not called after timer tick.");
        }

        [TestMethod]
        public void LoanCheckerService_Stop_ShouldStopTimer()
        {
            // Arrange: Start the service
            loanCheckerService.Start();

            // Act: Stop the service
            loanCheckerService.Stop();

            // Assert: Ensure that Stop was called on the mock timer
            mockTimer.Verify(t => t.Stop(), Times.Once, "Stop was not called on the timer.");
        }

        [TestMethod]
        public void LoanCheckerService_LoansUpdated_ShouldBeTriggeredOnTimerTick()
        {
            // Arrange
            var eventTriggered = false;
            loanCheckerService.LoansUpdated += (sender, args) => eventTriggered = true;

            // Act: Simulate the timer tick
            mockTimer.Raise(t => t.Tick += null, EventArgs.Empty);

            // Assert: Ensure that LoansUpdated event was triggered
            Assert.IsTrue(eventTriggered, "LoansUpdated event was not triggered after timer tick.");
        }
    }
}
