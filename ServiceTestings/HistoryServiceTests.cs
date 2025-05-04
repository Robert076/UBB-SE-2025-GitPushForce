using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Src.Model;
using Src.Repos;
using Src.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceTestings
{
    [TestClass]
    public class HistoryServiceTests
    {
        private Mock<IHistoryRepository> mockRepo;
        private HistoryService service;

        [TestInitialize]
        public void Setup()
        {
            mockRepo = new Mock<IHistoryRepository>();
            service = new HistoryService(mockRepo.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetHistoryByUserCNP_ShouldThrowIfCnpIsNullOrEmpty()
        {
            service.GetHistoryByUserCNP(null);
        }

        [TestMethod]
        public void GetHistoryByUserCNP_ShouldReturnHistory()
        {
            var cnp = "1234567890123";
            var expected = new List<CreditScoreHistory>
            {
                new CreditScoreHistory(1, cnp, DateOnly.FromDateTime(DateTime.Now), 700)
            };

            mockRepo.Setup(r => r.GetHistoryForUser(cnp)).Returns(expected);

            var actual = service.GetHistoryByUserCNP(cnp);

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(700, actual[0].Score);
        }

        [TestMethod]
        public void GetHistoryWeekly_ShouldFilterLast7Days()
        {
            var cnp = "123";
            var history = new List<CreditScoreHistory>
            {
                new CreditScoreHistory(1, cnp, DateOnly.FromDateTime(DateTime.Now.AddDays(-2)), 710),
                new CreditScoreHistory(2, cnp, DateOnly.FromDateTime(DateTime.Now.AddDays(-10)), 680)
            };

            mockRepo.Setup(r => r.GetHistoryForUser(cnp)).Returns(history);

            var result = service.GetHistoryWeekly(cnp);

            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result.All(h => h.Date >= DateOnly.FromDateTime(DateTime.Now.AddDays(-7))));
        }

        [TestMethod]
        public void GetHistoryMonthly_ShouldFilterLast30Days()
        {
            var cnp = "123";
            var history = new List<CreditScoreHistory>
            {
                new CreditScoreHistory(1, cnp, DateOnly.FromDateTime(DateTime.Now.AddDays(-5)), 700),
                new CreditScoreHistory(2, cnp, DateOnly.FromDateTime(DateTime.Now.AddMonths(-2)), 690)
            };

            mockRepo.Setup(r => r.GetHistoryForUser(cnp)).Returns(history);

            var result = service.GetHistoryMonthly(cnp);

            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result.All(h => h.Date >= DateOnly.FromDateTime(DateTime.Now.AddMonths(-1))));
        }

        [TestMethod]
        public void GetHistoryYearly_ShouldFilterLast365Days()
        {
            var cnp = "123";
            var history = new List<CreditScoreHistory>
            {
                new CreditScoreHistory(1, cnp, DateOnly.FromDateTime(DateTime.Now.AddMonths(-3)), 650),
                new CreditScoreHistory(2, cnp, DateOnly.FromDateTime(DateTime.Now.AddYears(-2)), 600)
            };

            mockRepo.Setup(r => r.GetHistoryForUser(cnp)).Returns(history);

            var result = service.GetHistoryYearly(cnp);

            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result.All(h => h.Date >= DateOnly.FromDateTime(DateTime.Now.AddYears(-1))));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetHistoryByUserCNP_ShouldPropagateRepoExceptions()
        {
            var cnp = "123";
            mockRepo.Setup(r => r.GetHistoryForUser(cnp)).Throws(new Exception("DB error"));

            service.GetHistoryByUserCNP(cnp);
        }
    }
}
