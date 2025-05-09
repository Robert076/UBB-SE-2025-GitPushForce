using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Src.Model;
using Src.Services;
using Src.ViewModel;

namespace ViewModelTestings
{
    [TestClass]
    public class BillSplitReportViewModelTests
    {
        private Mock<IBillSplitReportService> _mockService;

        [TestInitialize]
        public void SetUp()
        {
            _mockService = new Mock<IBillSplitReportService>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenServiceIsNull_ThrowsArgumentNullException()
        {
            // Act
            var vm = new BillSplitReportViewModel(null);
        }

        [TestMethod]
        public void Constructor_WithService_PopulatesInitialCollection()
        {
            // Arrange
            var reports = new List<BillSplitReport>
            {
                new BillSplitReport { Id = 1 },
                new BillSplitReport { Id = 2 }
            };
            _mockService.Setup(s => s.GetBillSplitReports()).Returns(reports);

            // Act
            var vm = new BillSplitReportViewModel(_mockService.Object);

            // Assert
            Assert.AreEqual(2, vm.BillSplitReports.Count);
        }

        [TestMethod]
        public async Task LoadBillSplitReports_WhenCalled_AddsReportsToCollection()
        {
            // Arrange
            var initialReports = new List<BillSplitReport>();
            var newReports = new List<BillSplitReport>
            {
                new BillSplitReport { Id = 3 },
                new BillSplitReport { Id = 4 }
            };

            _mockService.SetupSequence(s => s.GetBillSplitReports())
                        .Returns(initialReports)
                        .Returns(newReports); // for LoadBillSplitReports()

            var vm = new BillSplitReportViewModel(_mockService.Object);

            // Act
            await vm.LoadBillSplitReports();

            // Assert
            Assert.AreEqual(2, vm.BillSplitReports.Count);
            Assert.AreEqual(3, vm.BillSplitReports[0].Id);
            Assert.AreEqual(4, vm.BillSplitReports[1].Id);
        }

        [TestMethod]
        public async Task LoadBillSplitReports_WhenExceptionThrown_WritesToConsole()
        {
            // Arrange
            var initialReports = new List<BillSplitReport>();
            _mockService.SetupSequence(s => s.GetBillSplitReports())
                        .Returns(initialReports)
                        .Throws(new Exception("Test error"));

            var vm = new BillSplitReportViewModel(_mockService.Object);
            await vm.LoadBillSplitReports(); // First call is fine

            // Act
            await vm.LoadBillSplitReports(); // This will throw

            // Assert
            // No exception is propagated, so we just ensure it doesn't crash
            Assert.AreEqual(0, vm.BillSplitReports.Count);
        }
    }
}
