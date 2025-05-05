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
    public class MessagesServiceTests
    {
        private Mock<IUserRepository> mockUserRepo;
        private Mock<IMessagesRepository> mockMessagesRepo;
        private MessagesService service;

        [TestInitialize]
        public void SetUp()
        {
            mockUserRepo = new Mock<IUserRepository>();
            mockMessagesRepo = new Mock<IMessagesRepository>();
            service = new MessagesService(mockMessagesRepo.Object, mockUserRepo.Object);
        }

        [TestMethod]
        public void GiveMessageToUser_ShouldCallGiveUserRandomMessage_WhenCreditScoreIsAbove550()
        {
            // Arrange
            var userCnp = "123456789";
            var user = new User { Cnp = userCnp, CreditScore = 600 }; // Credit score >= 550
            mockUserRepo.Setup(repo => repo.GetUserByCnp(userCnp)).Returns(user);

            // Act
            service.GiveMessageToUser(userCnp);

            // Assert
            mockMessagesRepo.Verify(repo => repo.GiveUserRandomMessage(userCnp), Times.Once);
        }

        [TestMethod]
        public void GiveMessageToUser_ShouldCallGiveUserRandomRoastMessage_WhenCreditScoreIsBelow550()
        {
            // Arrange
            var userCnp = "987654321";
            var user = new User { Cnp = userCnp, CreditScore = 400 }; // Credit score < 550
            mockUserRepo.Setup(repo => repo.GetUserByCnp(userCnp)).Returns(user);

            // Act
            service.GiveMessageToUser(userCnp);

            // Assert
            mockMessagesRepo.Verify(repo => repo.GiveUserRandomRoastMessage(userCnp), Times.Once);
        }

        [TestMethod]
        public void GiveMessageToUser_ShouldNotCallRepository_WhenUserNotFound()
        {
            // Arrange
            var userCnp = "111222333";
            mockUserRepo.Setup(repo => repo.GetUserByCnp(userCnp)).Returns((User)null); // User not found

            // Act
            service.GiveMessageToUser(userCnp);

            // Assert
            mockMessagesRepo.Verify(repo => repo.GiveUserRandomMessage(It.IsAny<string>()), Times.Never);
            mockMessagesRepo.Verify(repo => repo.GiveUserRandomRoastMessage(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void GiveMessageToUser_ShouldHandleExceptionInGiveUserRandomMessage()
        {
            // Arrange
            var userCnp = "123456789";
            var user = new User { Cnp = userCnp, CreditScore = 600 };
            mockUserRepo.Setup(repo => repo.GetUserByCnp(userCnp)).Returns(user);
            mockMessagesRepo.Setup(repo => repo.GiveUserRandomMessage(userCnp)).Throws(new Exception("Database error"));

            // Act & Assert
            try
            {
                service.GiveMessageToUser(userCnp);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Error giving user random message", ex.Message);
            }
        }

        [TestMethod]
        public void GiveMessageToUser_ShouldHandleExceptionInGiveUserRandomRoastMessage()
        {
            // Arrange
            var userCnp = "987654321";
            var user = new User { Cnp = userCnp, CreditScore = 400 };
            mockUserRepo.Setup(repo => repo.GetUserByCnp(userCnp)).Returns(user);
            mockMessagesRepo.Setup(repo => repo.GiveUserRandomRoastMessage(userCnp)).Throws(new Exception("Database error"));

            // Act & Assert
            try
            {
                service.GiveMessageToUser(userCnp);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Error giving user random roast message", ex.Message);
            }
        }

        [TestMethod]
        public void GetMessagesForGivenUser_ShouldReturnMessages_WhenUserHasMessages()
        {
            // Arrange
            var userCnp = "111222333";
            var messages = new List<Message>
            {
                new Message { Id = 1, Type = "Congrats-message", MessageText = "Congrats on your score!" }
            };
            mockMessagesRepo.Setup(repo => repo.GetMessagesForGivenUser(userCnp)).Returns(messages);

            // Act
            var result = service.GetMessagesForGivenUser(userCnp);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Congrats-message", result[0].Type);
        }

        [TestMethod]
        public void GetMessagesForGivenUser_ShouldReturnEmptyList_WhenNoMessagesFound()
        {
            // Arrange
            var userCnp = "111222333";
            var messages = new List<Message>();
            mockMessagesRepo.Setup(repo => repo.GetMessagesForGivenUser(userCnp)).Returns(messages);

            // Act
            var result = service.GetMessagesForGivenUser(userCnp);

            // Assert
            Assert.AreEqual(0, result.Count);
        }
    }
}
