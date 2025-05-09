using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Src.Model;
using Src.Services;
using Src.ViewModel;

namespace ViewModelTestings
{
    [TestClass]
    public class ChatReportsViewModelTests
    {
        private Mock<IChatReportService> _mockService;

        [TestInitialize]
        public void SetUp()
        {
            _mockService = new Mock<IChatReportService>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenServiceIsNull_ThrowsArgumentNullException()
        {
            // Act
            var vm = new ChatReportsViewModel(null);
        }

        [TestMethod]
        public void Constructor_WithService_InitializesEmptyCollection()
        {
            // Act
            var vm = new ChatReportsViewModel(_mockService.Object);

            // Assert
            Assert.IsNotNull(vm.ChatReports);
            Assert.AreEqual(0, vm.ChatReports.Count);
        }

        [TestMethod]
        public async Task LoadChatReports_WhenCalled_AddsReportsToCollection()
        {
            // Arrange
            var reports = new List<ChatReport>
            {
                new ChatReport { Id = 1 },
                new ChatReport { Id = 2 }
            };
            _mockService.Setup(s => s.GetChatReports()).Returns(reports);

            var vm = new ChatReportsViewModel(_mockService.Object);

            // Act
            await vm.LoadChatReports();

            // Assert
            Assert.AreEqual(2, vm.ChatReports.Count);
            Assert.AreEqual(1, vm.ChatReports[0].Id);
            Assert.AreEqual(2, vm.ChatReports[1].Id);
        }

        [TestMethod]
        public async Task LoadChatReports_WhenExceptionThrown_DoesNotThrow()
        {
            // Arrange
            _mockService.Setup(s => s.GetChatReports()).Throws(new Exception("Mock failure"));
            var vm = new ChatReportsViewModel(_mockService.Object);

            // Act & Assert (no throw)
            await vm.LoadChatReports();
            Assert.AreEqual(0, vm.ChatReports.Count); // Still empty
        }
    }
}
