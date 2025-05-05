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
    public class UserServiceTests
    {
        private Mock<IUserRepository> mockUserRepo;
        private UserService userService;

        [TestInitialize]
        public void SetUp()
        {
            mockUserRepo = new Mock<IUserRepository>();
            userService = new UserService(mockUserRepo.Object); // Injecting the mock IUserRepository
        }

        [TestMethod]
        public void GetUserByCnp_ShouldThrowArgumentException_WhenCnpIsEmpty()
        {
            // Arrange
            string invalidCnp = string.Empty;

            // Act & Assert
            var ex = Assert.ThrowsException<ArgumentException>(() => userService.GetUserByCnp(invalidCnp));
            Assert.AreEqual("CNP cannot be empty", ex.Message);  // Validate exception message
        }

        [TestMethod]
        public void GetUserByCnp_ShouldThrowArgumentException_WhenCnpIsNull()
        {
            // Arrange
            string invalidCnp = null;

            // Act & Assert
            var ex = Assert.ThrowsException<ArgumentException>(() => userService.GetUserByCnp(invalidCnp));
            Assert.AreEqual("CNP cannot be empty", ex.Message);  // Validate exception message
        }

        [TestMethod]
        public void GetUserByCnp_ShouldReturnUser_WhenCnpIsValid()
        {
            // Arrange
            var validCnp = "123456789";
            var user = new User { Cnp = validCnp, FirstName = "John", LastName = "Doe" };
            mockUserRepo.Setup(repo => repo.GetUserByCnp(validCnp)).Returns(user);

            // Act
            var result = userService.GetUserByCnp(validCnp);

            // Assert
            Assert.AreEqual(validCnp, result.Cnp);
            Assert.AreEqual("John", result.FirstName);
            Assert.AreEqual("Doe", result.LastName);
        }

        [TestMethod]
        public void GetUsers_ShouldReturnListOfUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, FirstName = "Alice", LastName = "Smith" },
                new User { Id = 2, FirstName = "Bob", LastName = "Johnson" }
            };
            mockUserRepo.Setup(repo => repo.GetUsers()).Returns(users);

            // Act
            var result = userService.GetUsers();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Alice", result[0].FirstName);
            Assert.AreEqual("Bob", result[1].FirstName);
        }

        [TestMethod]
        public void GetUsers_ShouldReturnEmptyList_WhenNoUsersExist()
        {
            // Arrange
            var users = new List<User>(); // No users
            mockUserRepo.Setup(repo => repo.GetUsers()).Returns(users);

            // Act
            var result = userService.GetUsers();

            // Assert
            Assert.AreEqual(0, result.Count);  // Ensure the returned list is empty
        }
    }
}
