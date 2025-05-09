using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Src.Model;
using Src.Services;
using Src.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ViewModelTestings
{
    [TestClass]
    public class UserViewModelTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenServiceIsNull_ThrowsArgumentNullException()
        {
            var viewModel = new UserViewModel(null);
        }

        [TestMethod]
        public async Task LoadUsers_AddsReturnedUsersToCollection()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var testUsers = new List<User>
            {
                new User(
                    id: 1,
                    cnp: "1234567890123",
                    firstName: "Alice",
                    lastName: "Smith",
                    email: "alice@example.com",
                    phoneNumber: "1234567890",
                    hashedPassword: "hashedpwd",
                    numberOfOffenses: 0,
                    riskScore: 10,
                    roi: 0.05m,
                    creditScore: 700,
                    birthday: new DateOnly(1990, 5, 1),
                    zodiacSign: "Taurus",
                    zodiacAttribute: "Earth",
                    numberOfBillSharesPaid: 5,
                    income: 50000,
                    balance: 1234.56m
                )
            };

            mockService.Setup(s => s.GetUsers()).Returns(testUsers);

            var viewModel = new UserViewModel(mockService.Object);

            // Act
            await viewModel.LoadUsers();

            // Assert
            Assert.AreEqual(1, viewModel.Users.Count);
            var user = viewModel.Users[0];
            Assert.AreEqual("Alice", user.FirstName);
            Assert.AreEqual("Taurus", user.ZodiacSign);
            Assert.AreEqual(1234.56m, user.Balance);
        }

        [TestMethod]
        public async Task LoadUsers_DoesNotThrow_WhenServiceThrows()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            mockService.Setup(s => s.GetUsers()).Throws(new Exception("Data fetch error"));

            var viewModel = new UserViewModel(mockService.Object);

            // Act
            await viewModel.LoadUsers();

            // Assert
            Assert.AreEqual(0, viewModel.Users.Count);
        }

        [TestMethod]
        public async Task LoadUsers_ClearsOldUsersBeforeAddingNew_OnSubsequentCall()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            mockService.SetupSequence(s => s.GetUsers())
                .Returns(new List<User>
                {
                    new User { Id = 1, FirstName = "John" }
                })
                .Returns(new List<User>
                {
                    new User { Id = 2, FirstName = "Jane" }
                });

            var viewModel = new UserViewModel(mockService.Object);

            // Act
            await viewModel.LoadUsers(); // John
      

            Assert.AreEqual(1, viewModel.Users.Count);
            Assert.AreEqual(1, viewModel.Users[0].Id);
            Assert.AreEqual("John", viewModel.Users[0].FirstName);
        }
    }
}
