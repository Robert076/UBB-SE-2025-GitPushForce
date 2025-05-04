using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using Src.Model;
using Src.Repos;
using Src.Services;

namespace ServiceTestings
{
    [TestClass]
    public class BillSplitReportServiceTests
    {
        private Mock<IBillSplitReportRepository> mockRepo;
        private BillSplitReportService service;

        [TestInitialize]
        public void Setup()
        {
            mockRepo = new Mock<IBillSplitReportRepository>();
            service = new BillSplitReportService(mockRepo.Object);
        }

        [TestMethod]
        public void GetBillSplitReports_ReturnsList()
        {
            var mockList = new List<BillSplitReport> { new BillSplitReport(), new BillSplitReport() };
            mockRepo.Setup(r => r.GetBillSplitReports()).Returns(mockList);

            var result = service.GetBillSplitReports();

            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void CreateBillSplitReport_CallsRepository()
        {
            var report = new BillSplitReport();

            service.CreateBillSplitReport(report);

            mockRepo.Verify(r => r.CreateBillSplitReport(report), Times.Once);
        }

        [TestMethod]
        public void GetDaysOverdue_ReturnsCorrectValue()
        {
            var report = new BillSplitReport();
            mockRepo.Setup(r => r.GetDaysOverdue(report)).Returns(10);

            var result = service.GetDaysOverdue(report);

            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void SolveBillSplitReport_UpdatesCreditScore_And_DeletesReport()
        {
            var report = new BillSplitReport
            {
                Id = 1,
                BillShare = 500,
                DateOfTransaction = DateTime.Now.AddDays(-10)
            };

            mockRepo.Setup(r => r.GetDaysOverdue(report)).Returns(10);
            mockRepo.Setup(r => r.GetCurrentCreditScore(report)).Returns(700);
            mockRepo.Setup(r => r.SumTransactionsSinceReport(report)).Returns(600m);
            mockRepo.Setup(r => r.CheckHistoryOfBillShares(report)).Returns(true);
            mockRepo.Setup(r => r.CheckFrequentTransfers(report)).Returns(true);
            mockRepo.Setup(r => r.GetNumberOfOffenses(report)).Returns(3);

            service.SolveBillSplitReport(report);

            mockRepo.Verify(r => r.UpdateCreditScore(report, It.IsAny<int>()), Times.Once);
            mockRepo.Verify(r => r.UpdateCreditScoreHistory(report, It.IsAny<int>()), Times.Once);
            mockRepo.Verify(r => r.IncrementNoOfBillSharesPaid(report), Times.Once);
            mockRepo.Verify(r => r.DeleteBillSplitReport(report.Id), Times.Once);
        }

        [TestMethod]
        public void DeleteBillSplitReport_CallsDelete()
        {
            var report = new BillSplitReport { Id = 5 };

            service.DeleteBillSplitReport(report);

            mockRepo.Verify(r => r.DeleteBillSplitReport(5), Times.Once);
        }

        [TestMethod]
        public void GetUserByCNP_ReturnsUser()
        {
            var result = service.GetUserByCNP("1234567890123");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(User));
        }
    }
}
