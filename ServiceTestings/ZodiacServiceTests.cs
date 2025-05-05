using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Src.Model;
using Src.Repos;
using Src.Services;

namespace ServiceTestings
{
    [TestClass]
    public class ZodiacServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private HttpClient _mockHttpClient;
        private ZodiacService _service;

        [TestInitialize]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mockHttpClient = CreateMockHttpClient("{\"value\":\"mock joke\"}");
            _service = new ZodiacService(_userRepositoryMock.Object, _mockHttpClient);
        }

        private HttpClient CreateMockHttpClient(string responseContent)
        {
            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                   "SendAsync",
                   ItExpr.IsAny<HttpRequestMessage>(),
                   ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(responseContent),
               });

            return new HttpClient(handlerMock.Object);
        }

        [TestMethod]
        public void ComputeJokeAsciiModulo10_ShouldReturnCorrectValue_WhenJokeIsGiven()
        {
            // Arrange
            string joke = "ab"; // ASCII: a=97, b=98, sum=195 -> 195 % 10 = 5

            // Act
            int result = _service.ComputeJokeAsciiModulo10(joke);

            // Assert
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ComputeJokeAsciiModulo10_ShouldThrowArgumentNullException_WhenJokeIsNull()
        {
            _service.ComputeJokeAsciiModulo10(null);
        }

        [TestMethod]
        public async Task CreditScoreModificationBasedOnJokeAndCoinFlipAsync_ShouldModifyUserCreditScores()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Cnp = "123", CreditScore = 100 },
                new User { Cnp = "456", CreditScore = 200 }
            };
            _userRepositoryMock.Setup(r => r.GetUsers()).Returns(users);
            _userRepositoryMock.Setup(r => r.UpdateUserCreditScore(It.IsAny<string>(), It.IsAny<int>()));

            // Act
            await _service.CreditScoreModificationBasedOnJokeAndCoinFlipAsync();

            // Assert
            _userRepositoryMock.Verify(r => r.UpdateUserCreditScore("123", It.IsAny<int>()), Times.Once);
            _userRepositoryMock.Verify(r => r.UpdateUserCreditScore("456", It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void CreditScoreModificationBasedOnAttributeAndGravity_ShouldUpdateScores()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Cnp = "123", CreditScore = 100 },
                new User { Cnp = "456", CreditScore = 200 }
            };
            _userRepositoryMock.Setup(r => r.GetUsers()).Returns(users);

            // Act
            _service.CreditScoreModificationBasedOnAttributeAndGravity();

            // Assert
            _userRepositoryMock.Verify(r => r.UpdateUserCreditScore("123", It.IsAny<int>()), Times.Once);
            _userRepositoryMock.Verify(r => r.UpdateUserCreditScore("456", It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "No users found.")]
        public void CreditScoreModificationBasedOnAttributeAndGravity_ShouldThrowException_WhenNoUsers()
        {
            _userRepositoryMock.Setup(r => r.GetUsers()).Returns(new List<User>());
            _service.CreditScoreModificationBasedOnAttributeAndGravity();
        }
    }
}
