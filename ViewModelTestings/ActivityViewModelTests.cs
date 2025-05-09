using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Src.Services;
using Src.ViewModel;

namespace ViewModelTestings

{
    [TestClass]
    public class ActivityViewModelTests
    {
        private Mock<IActivityService> _mockActivityService;

        [TestInitialize]
        public void SetUp()
        {
            _mockActivityService = new Mock<IActivityService>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenServiceIsNull_ThrowsArgumentNullException()
        {
            // Act
            var viewModel = new ActivityViewModel(null);
        }

        [TestMethod]
        public void Constructor_WithValidService_DoesNotThrow()
        {
            // Act
            var viewModel = new ActivityViewModel(_mockActivityService.Object);

            // Assert
            Assert.IsNotNull(viewModel); // Sanity check
        }

        [TestMethod]
        public void OnPropertyChanged_WhenCalled_RaisesPropertyChangedEvent()
        {
            // Arrange
            var viewModel = new TestableActivityViewModel(_mockActivityService.Object);
            string actualPropertyName = null;

            viewModel.PropertyChanged += (sender, e) =>
            {
                actualPropertyName = e.PropertyName;
            };

            // Act
            viewModel.InvokeOnPropertyChanged("SampleProperty");

            // Assert
            Assert.AreEqual("SampleProperty", actualPropertyName);
        }

        // Test subclass to expose protected OnPropertyChanged for testing
        private class TestableActivityViewModel : ActivityViewModel
        {
            public TestableActivityViewModel(IActivityService activityService)
                : base(activityService) { }

            public void InvokeOnPropertyChanged(string propertyName)
            {
                base.OnPropertyChanged(propertyName);
            }
        }
    }
}
