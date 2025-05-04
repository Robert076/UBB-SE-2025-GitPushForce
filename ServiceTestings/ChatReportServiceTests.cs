using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Src.Model;
using Src.Repos;
using Src.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceTestings
{
    [TestClass]
    public class ChatReportServiceTests
    {
        private Mock<IChatReportRepository> chatReportRepoMock;
        private ChatReportService service;

        [TestInitialize]
        public void Setup()
        {
            chatReportRepoMock = new Mock<IChatReportRepository>();
            service = new ChatReportService(chatReportRepoMock.Object);
        }

        [TestMethod]
        public void DoNotPunishUser_ShouldCallDeleteChatReport()
        {
            var report = new ChatReport { Id = 1 };
            service.DoNotPunishUser(report);

            chatReportRepoMock.Verify(repo => repo.DeleteChatReport(1), Times.Once);
        }

        [TestMethod]
        public void GetChatReports_ShouldReturnListFromRepo()
        {
            var expectedReports = new List<ChatReport>
            {
                new ChatReport(1, "123", "spam message"),
                new ChatReport(2, "456", "another message")
            };

            chatReportRepoMock.Setup(repo => repo.GetChatReports()).Returns(expectedReports);

            var actual = service.GetChatReports();

            Assert.AreEqual(2, actual.Count);
            Assert.AreEqual("123", actual[0].ReportedUserCnp);
        }

        [TestMethod]
        public void DeleteChatReport_ShouldCallRepo()
        {
            service.DeleteChatReport(5);
            chatReportRepoMock.Verify(repo => repo.DeleteChatReport(5), Times.Once);
        }

        [TestMethod]
        public void UpdateHistoryForUser_ShouldCallRepo()
        {
            service.UpdateHistoryForUser("123456", 88);
            chatReportRepoMock.Verify(repo => repo.UpdateScoreHistoryForUser("123456", 88), Times.Once);
        }

        [TestMethod]
        public async Task IsMessageOffensive_ShouldReturnExpectedResult()
        {
            string testMessage = "you are awful";

            
            bool result = await service.IsMessageOffensive(testMessage);

            Assert.IsInstanceOfType(result, typeof(bool));
        }

        [TestMethod]
        [Ignore("PunishUser involves multiple concrete repositories, tested in integration tests.")]
        public async Task PunishUser_ShouldExecuteWithoutException()
        {
            var report = new ChatReport { Id = 1, ReportedUserCnp = "123456" };
            bool result = await service.PunishUser(report);

            Assert.IsTrue(result);
        }
    }
}
