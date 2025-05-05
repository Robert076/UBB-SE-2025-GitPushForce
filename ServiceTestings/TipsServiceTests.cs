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
    public class TipsServiceTests
    {
        private Mock<ITipsRepository> mockTipsRepo;
        private Mock<IUserRepository> mockUserRepo;
        private TipsService service;

        [TestInitialize]
        public void SetUp()
        {
            mockTipsRepo = new Mock<ITipsRepository>();
            mockUserRepo = new Mock<IUserRepository>();
            service = new TipsService(mockTipsRepo.Object, mockUserRepo.Object);  // Injecting the mock IUserRepository
        }

        [TestMethod]
        public void GiveTipToUser_ShouldCallGiveUserTipForLowBracket_WhenCreditScoreIsBelow300()
        {
            // Arrange
            var userCnp = "123456789";
            var user = new User { Cnp = userCnp, CreditScore = 250 };
            mockUserRepo.Setup(repo => repo.GetUserByCnp(userCnp)).Returns(user);

            // Act
            service.GiveTipToUser(userCnp);

            // Assert
            mockTipsRepo.Verify(repo => repo.GiveUserTipForLowBracket(userCnp), Times.Once);
        }

        [TestMethod]
        public void GiveTipToUser_ShouldCallGiveUserTipForMediumBracket_WhenCreditScoreIsBelow550()
        {
            // Arrange
            var userCnp = "987654321";
            var user = new User { Cnp = userCnp, CreditScore = 400 };
            mockUserRepo.Setup(repo => repo.GetUserByCnp(userCnp)).Returns(user);

            // Act
            service.GiveTipToUser(userCnp);

            // Assert
            mockTipsRepo.Verify(repo => repo.GiveUserTipForMediumBracket(userCnp), Times.Once);
        }

        [TestMethod]
        public void GiveTipToUser_ShouldCallGiveUserTipForHighBracket_WhenCreditScoreIsAbove549()
        {
            // Arrange
            var userCnp = "112233445";
            var user = new User { Cnp = userCnp, CreditScore = 600 };
            mockUserRepo.Setup(repo => repo.GetUserByCnp(userCnp)).Returns(user);

            // Act
            service.GiveTipToUser(userCnp);

            // Assert
            mockTipsRepo.Verify(repo => repo.GiveUserTipForHighBracket(userCnp), Times.Once);  // Verify call to high bracket method
        }

        [TestMethod]
        public void GiveTipToUser_ShouldHandleException_WhenUserNotFound()
        {
            // Arrange
            var userCnp = "555666777";
            mockUserRepo.Setup(repo => repo.GetUserByCnp(userCnp)).Throws(new Exception("User not found"));

            // Act & Assert
            try
            {
                service.GiveTipToUser(userCnp);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("User not found", ex.Message);  // Assert the exception handling
            }
        }

        [TestMethod]
        public void GetTipsForGivenUser_ShouldReturnTips_WhenUserHasTips()
        {
            // Arrange
            var userCnp = "888999000";
            var tips = new List<Tip>
            {
                new Tip { Id = 1, CreditScoreBracket = "Medium-credit", TipText = "Save more money" }
            };
            mockTipsRepo.Setup(repo => repo.GetTipsForGivenUser(userCnp)).Returns(tips);

            // Act
            var result = service.GetTipsForGivenUser(userCnp);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Medium-credit", result[0].CreditScoreBracket);
        }

        [TestMethod]
        public void GetTipsForGivenUser_ShouldReturnEmptyList_WhenUserHasNoTips()
        {
            // Arrange
            var userCnp = "123123123";
            var tips = new List<Tip>(); // No tips
            mockTipsRepo.Setup(repo => repo.GetTipsForGivenUser(userCnp)).Returns(tips);

            // Act
            var result = service.GetTipsForGivenUser(userCnp);

            // Assert
            Assert.AreEqual(0, result.Count);  // Ensure empty list is returned
        }
    }
}
