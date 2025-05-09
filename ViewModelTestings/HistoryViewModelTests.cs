using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Src.Services;
using Src.ViewModel;

namespace ViewModelTestings
{
    [TestClass]
    public class HistoryViewModelTests
    {
        private Mock<IHistoryService> _mockHistoryService;

        [TestInitialize]
        public void SetUp()
        {
            _mockHistoryService = new Mock<IHistoryService>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenServiceIsNull_ThrowsArgumentNullException()
        {
            // Act
            var viewModel = new HistoryViewModel(null);
        }

        [TestMethod]
        public void Constructor_WithService_DoesNotThrow()
        {
            // Act
            var viewModel = new HistoryViewModel(_mockHistoryService.Object);

            // Assert
            Assert.IsNotNull(viewModel);
        }

        [TestMethod]
        public void OnPropertyChanged_WhenCalled_RaisesPropertyChangedEvent()
        {
            // Arrange
            var viewModel = new TestableHistoryViewModel(_mockHistoryService.Object);
            string changedProperty = null;
            viewModel.PropertyChanged += (sender, e) => changedProperty = e.PropertyName;

            // Act
            viewModel.InvokeOnPropertyChanged("SomeProperty");

            // Assert
            Assert.AreEqual("SomeProperty", changedProperty);
        }

        // Subclass to expose protected OnPropertyChanged for testing
        private class TestableHistoryViewModel : HistoryViewModel
        {
            public TestableHistoryViewModel(IHistoryService historyService)
                : base(historyService) { }

            public void InvokeOnPropertyChanged(string propertyName)
            {
                base.OnPropertyChanged(propertyName);
            }
        }
    }
}
