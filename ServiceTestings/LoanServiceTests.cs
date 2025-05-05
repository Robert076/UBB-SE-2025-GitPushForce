using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Src.Model;
using Src.Repos;
using Src.Services;
using Src.Views;

namespace ServiceTestings
{
    [TestClass]
    public class LoanServiceTests
    {
        private Mock<ILoanRepository> _loanRepoMock;
        private Mock<IUserRepository> _userRepoMock;
        private LoanService _sut;

        [TestInitialize]
        public void Setup()
        {
            _loanRepoMock = new Mock<ILoanRepository>();
            _userRepoMock = new Mock<IUserRepository>();
            _sut = new LoanService(_loanRepoMock.Object, _userRepoMock.Object);
        }

        [TestMethod]
        public void GetLoans_ShouldReturnLoansFromRepository()
        {
            // Arrange
            var expected = new List<Loan> { new Loan(1, "123", 1000, DateTime.Now, DateTime.Now.AddMonths(6), 10, 6, 200, 0, 0, 0, "active") };
            _loanRepoMock.Setup(r => r.GetLoans()).Returns(expected);

            // Act
            var result = _sut.GetLoans();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetUserLoans_ShouldReturnLoansFromRepository()
        {
            var expected = new List<Loan>();
            _loanRepoMock.Setup(r => r.GetUserLoans("123")).Returns(expected);

            var result = _sut.GetUserLoans("123");

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "User not found")]
        public void AddLoan_WhenUserNotFound_ShouldThrow()
        {
            _userRepoMock.Setup(r => r.GetUserByCnp("123")).Returns((User)null);
            var request = new LoanRequest(1, "123", 1200f, new DateTime(2024, 1, 1), new DateTime(2024, 7, 1), "pending");


            _sut.AddLoan(request);
        }

        [TestMethod]
        public void AddLoan_ShouldAddLoanCorrectly()
        {
            var user = new User { Cnp = "123", RiskScore = 10, CreditScore = 100, Income = 5000 };
            _userRepoMock.Setup(r => r.GetUserByCnp("123")).Returns(user);

            var request = new LoanRequest(1, "123", 1200f, new DateTime(2024, 1, 1), new DateTime(2024, 7, 1), "pending");

            _sut.AddLoan(request);

            _loanRepoMock.Verify(r => r.AddLoan(It.Is<Loan>(l =>
                l.UserCnp == "123" &&
                l.Status == "active" &&
                l.NumberOfMonths == 6
            )), Times.Once);
        }

        [TestMethod]
        public void ComputeNewCreditScore_ShouldRespectBounds()
        {
            var user = new User { CreditScore = 600, Income = 2000 };
            var loan = new Loan(1, "123", 100, DateTime.Today, DateTime.Today.AddDays(35), 10, 6, 0, 0, 0, 0, "active");

            var score = _sut.ComputeNewCreditScore(user, loan);

            Assert.IsTrue(score <= 700 && score >= 100);
        }

        [TestMethod]
        public void ComputeNewCreditScore_ShouldClampLowScore()
        {
            var user = new User { CreditScore = 150, Income = 2000 };
            var loan = new Loan(1, "123", 100, DateTime.Today, DateTime.Today.AddDays(-150), 10, 6, 0, 0, 0, 0, "active");

            var score = _sut.ComputeNewCreditScore(user, loan);

            Assert.IsTrue(score >= 100);
        }

        [TestMethod]
        public void UpdateHistoryForUser_ShouldCallRepository()
        {
            _sut.UpdateHistoryForUser("123", 500);

            _loanRepoMock.Verify(r => r.UpdateCreditScoreHistoryForUser("123", 500), Times.Once);
        }

        [TestMethod]
        public void IncrementMonthlyPaymentsCompleted_ShouldUpdateLoan()
        {
            var loan = new Loan(1, "123", 1000, DateTime.Today, DateTime.Today.AddMonths(3), 10, 3, 100, 1, 200, 0, "active");
            _loanRepoMock.Setup(r => r.GetLoanById(1)).Returns(loan);

            _sut.IncrementMonthlyPaymentsCompleted(1, 50);

            Assert.AreEqual(2, loan.MonthlyPaymentsCompleted);
            Assert.AreEqual(350, loan.RepaidAmount);
            _loanRepoMock.Verify(r => r.UpdateLoan(loan), Times.Once);
        }

        [TestMethod]
        public void CheckLoans_ShouldHandleCompletedLoan()
        {
            var loan = new Loan(1, "123", 1000, DateTime.Today.AddMonths(-6), DateTime.Today, 10, 6, 200, 6, 1200, 0, "active");
            var user = new User { CreditScore = 300, Income = 1000, Cnp = "123" };

            _loanRepoMock.Setup(r => r.GetLoans()).Returns(new List<Loan> { loan });
            _userRepoMock.Setup(r => r.GetUserByCnp("123")).Returns(user);

            _sut.CheckLoans();

            _loanRepoMock.Verify(r => r.DeleteLoan(1), Times.Once);
        }

        [TestMethod]
        public void CheckLoans_ShouldApplyPenaltyIfOverdue()
        {
            var loan = new Loan(1, "123", 1000, DateTime.Today.AddMonths(-3), DateTime.Today.AddMonths(3), 10, 6, 200, 1, 200, 0, "active");
            var user = new User { CreditScore = 300, Income = 1000, Cnp = "123" };

            _loanRepoMock.Setup(r => r.GetLoans()).Returns(new List<Loan> { loan });
            _userRepoMock.Setup(r => r.GetUserByCnp("123")).Returns(user);

            _sut.CheckLoans();

            Assert.IsTrue(loan.Penalty > 0);
            _loanRepoMock.Verify(r => r.UpdateLoan(loan), Times.Once);
        }

        [TestMethod]
        public void CheckLoans_ShouldMarkAsOverduePastRepaymentDate()
        {
            var loan = new Loan(1, "123", 1000, DateTime.Today.AddMonths(-6), DateTime.Today.AddDays(-1), 10, 6, 200, 1, 200, 0, "active");
            var user = new User { CreditScore = 300, Income = 1000, Cnp = "123" };

            _loanRepoMock.Setup(r => r.GetLoans()).Returns(new List<Loan> { loan });
            _userRepoMock.Setup(r => r.GetUserByCnp("123")).Returns(user);

            _sut.CheckLoans();

            Assert.AreEqual("overdue", loan.Status);
        }

        [TestMethod]
        public void CheckLoans_OverdueLoanBecomesCompleted()
        {
            var loan = new Loan(1, "123", 1000, DateTime.Today.AddMonths(-6), DateTime.Today.AddDays(-1), 10, 6, 200, 6, 1200, 0, "overdue");
            var user = new User { CreditScore = 300, Income = 1000, Cnp = "123" };

            _loanRepoMock.Setup(r => r.GetLoans()).Returns(new List<Loan> { loan });
            _userRepoMock.Setup(r => r.GetUserByCnp("123")).Returns(user);

            _sut.CheckLoans();

            Assert.AreEqual("completed", loan.Status);
        }
    }
}
