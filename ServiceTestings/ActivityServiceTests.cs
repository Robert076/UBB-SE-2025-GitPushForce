using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Src.Model;
using Src.Repos;
using Src.Services;
using System;
using System.Collections.Generic;

namespace ServiceTestings
{
    [TestClass]
    public class ActivityServiceTests
    {
        private Mock<IActivityRepository> _mockActivityRepository;
        private IActivityService _activityService;

        [TestInitialize]
        public void SetUp()
        {
            _mockActivityRepository = new Mock<IActivityRepository>();
            _activityService = new ActivityService(_mockActivityRepository.Object);
        }

        [TestMethod]
        public void GetActivityForUser_ShouldThrowArgumentException_WhenUserCnpIsNull()
        {
            // Act & Assert
            var ex = Assert.ThrowsException<ArgumentException>(() => _activityService.GetActivityForUser(null));
            Assert.AreEqual("user cannot be found", ex.Message);
        }

        [TestMethod]
        public void GetActivityForUser_ShouldThrowArgumentException_WhenUserCnpIsEmpty()
        {
            // Act & Assert
            var ex = Assert.ThrowsException<ArgumentException>(() => _activityService.GetActivityForUser(string.Empty));
            Assert.AreEqual("user cannot be found", ex.Message);
        }

        [TestMethod]
        public void GetActivityForUser_ShouldReturnEmptyList_WhenNoActivitiesFound()
        {
            // Arrange
            _mockActivityRepository.Setup(repo => repo.GetActivityForUser(It.IsAny<string>())).Returns(new List<ActivityLog>());

            // Act
            var result = _activityService.GetActivityForUser("12345");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetActivityForUser_ShouldReturnActivityList_WhenActivitiesFound()
        {
            // Arrange
            var activities = new List<ActivityLog>
            {
                new ActivityLog { Id = 1, UserCnp = "12345", ActivityName = "Activity 1", LastModifiedAmount = 10, ActivityDetails = "Test activity 1" },
                new ActivityLog { Id = 2, UserCnp = "12345", ActivityName = "Activity 2", LastModifiedAmount = 20, ActivityDetails = "Test activity 2" }
            };
            _mockActivityRepository.Setup(repo => repo.GetActivityForUser("12345")).Returns(activities);

            // Act
            var result = _activityService.GetActivityForUser("12345");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Activity 1", result[0].ActivityName);
            Assert.AreEqual(10, result[0].LastModifiedAmount);
            Assert.AreEqual("Activity 2", result[1].ActivityName);
            Assert.AreEqual(20, result[1].LastModifiedAmount);
        }

        [TestMethod]
        public void GetActivityForUser_ShouldCallRepositoryOnce_WhenValidCnpProvided()
        {
            // Arrange
            _mockActivityRepository.Setup(repo => repo.GetActivityForUser(It.IsAny<string>())).Returns(new List<ActivityLog>());

            // Act
            _activityService.GetActivityForUser("12345");

            // Assert
            _mockActivityRepository.Verify(repo => repo.GetActivityForUser("12345"), Times.Once);
        }

        [TestMethod]
        public void GetActivityForUser_ShouldThrowArgumentException_WhenCnpIsWhitespace()
        {
            // Act & Assert
            var ex = Assert.ThrowsException<ArgumentException>(() => _activityService.GetActivityForUser(" "));
            Assert.AreEqual("user cannot be found", ex.Message);
        }

        [TestMethod]
        public void Constructor_ShouldThrowArgumentNullException_WhenRepositoryIsNull()
        {
            // Act & Assert
            var ex = Assert.ThrowsException<ArgumentNullException>(() => new ActivityService(null));
            Assert.AreEqual("Value cannot be null. (Parameter 'activityRepository')", ex.Message);
        }
    }
}
