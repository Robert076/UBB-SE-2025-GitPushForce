using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Src.Services;

namespace ServiceTestings
{
    [TestClass]
    public class LoanCheckerServiceTests
    {
        [TestMethod]
        public async Task Start_ShouldTriggerLoanCheckAndEvent()
        {
            // Arrange
            var mockLoanService = new Mock<ILoanService>();
            var service = new LoanCheckerService(mockLoanService.Object);

            bool eventTriggered = false;
            service.LoansUpdated += (s, e) => eventTriggered = true;

            // Act
            service.Start();
            await Task.Delay(1500); // wait for timer to tick at least once
            service.Stop();

            // Assert
            mockLoanService.Verify(s => s.CheckLoans(), Times.AtLeastOnce());
            Assert.IsTrue(eventTriggered);
        }

        [TestMethod]
        public async Task Stop_ShouldPreventFurtherTimerTicks()
        {
            // Arrange
            var mockLoanService = new Mock<ILoanService>();
            var service = new LoanCheckerService(mockLoanService.Object);

            int callCount = 0;
            mockLoanService.Setup(s => s.CheckLoans()).Callback(() => callCount++);

            // Act
            service.Start();
            await Task.Delay(1100); // allow timer to tick once
            service.Stop();
            int countAfterStop = callCount;
            await Task.Delay(1100); // wait again, should not tick

            // Assert
            Assert.AreEqual(countAfterStop, callCount); // no more ticks after stop
        }

        [TestMethod]
        public void Start_ShouldNotThrow()
        {
            // Arrange
            var mockLoanService = new Mock<ILoanService>();
            var service = new LoanCheckerService(mockLoanService.Object);

            // Act & Assert
            try
            {
                service.Start();
                service.Stop(); // clean up
            }
            catch (Exception ex)
            {
                Assert.Fail($"Start threw an exception: {ex}");
            }
        }

        [TestMethod]
        public void Stop_ShouldNotThrow()
        {
            // Arrange
            var mockLoanService = new Mock<ILoanService>();
            var service = new LoanCheckerService(mockLoanService.Object);

            // Act & Assert
            try
            {
                service.Start();
                service.Stop();
            }
            catch (Exception ex)
            {
                Assert.Fail($"Stop threw an exception: {ex}");
            }
        }
    }
}
