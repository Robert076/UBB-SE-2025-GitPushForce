using src.Repos;
using src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Helpers;
using src.Data;
using System.Data;
using Microsoft.Data.SqlClient;
using src.Services;

namespace src.Services
{
    public class ChatReportService
    {
        ChatReportRepository _chatReportRepository;

        public ChatReportService(ChatReportRepository chatReportRepository)
        {
            _chatReportRepository = chatReportRepository;
        }

        public async Task<bool> SolveChatReport(ChatReport chatReportToBeSolved)
        {

            string reportedMessage = chatReportToBeSolved.ReportedMessage;

            bool isReportedMessageOffensive = await IsMessageOffensive(reportedMessage);

            if (!isReportedMessageOffensive)
            {
                _chatReportRepository.DeleteChatReport(chatReportToBeSolved.Id);
                return false;
            }

            DatabaseConnection dbConn = new DatabaseConnection();
            UserRepository userRepo = new UserRepository(dbConn);

            //userRepo.GetUserByCNP("5040203070016");
            User reportedUser = userRepo.GetUserByCNP(chatReportToBeSolved.ReportedUserCNP);

            Int32 noOffenses = reportedUser.NoOffenses;
            const Int32 MINIMUM_NUMBER_OF_OFFENSES_BEFORE_PUNISHMENT_GROWS_DISTOPIANLY_ABSURD = 3;
            const Int32 CREDIT_SCORE_DECREASE_AMOUNT_FLAT_RATE = 15;

            int amount;

            if (noOffenses >= MINIMUM_NUMBER_OF_OFFENSES_BEFORE_PUNISHMENT_GROWS_DISTOPIANLY_ABSURD)
            {
                userRepo.PenalizeUser(chatReportToBeSolved.ReportedUserCNP, noOffenses * CREDIT_SCORE_DECREASE_AMOUNT_FLAT_RATE);
                Int32 decrease = reportedUser.CreditScore - CREDIT_SCORE_DECREASE_AMOUNT_FLAT_RATE * noOffenses;
                UpdateHistoryForUser(chatReportToBeSolved.ReportedUserCNP, decrease);
                amount = CREDIT_SCORE_DECREASE_AMOUNT_FLAT_RATE * noOffenses;
            }
            else
            {
                userRepo.PenalizeUser(chatReportToBeSolved.ReportedUserCNP, CREDIT_SCORE_DECREASE_AMOUNT_FLAT_RATE);
                Int32 decrease = userRepo.GetUserByCNP(chatReportToBeSolved.ReportedUserCNP).CreditScore - CREDIT_SCORE_DECREASE_AMOUNT_FLAT_RATE;
                UpdateHistoryForUser(chatReportToBeSolved.ReportedUserCNP, decrease);
                amount = CREDIT_SCORE_DECREASE_AMOUNT_FLAT_RATE;
            }
            userRepo.IncrementOffenesesCountByOne(chatReportToBeSolved.ReportedUserCNP);
            _chatReportRepository.DeleteChatReport(chatReportToBeSolved.Id);
            TipsService service = new TipsService();
            service.GiveTipToUser(chatReportToBeSolved.ReportedUserCNP);
            SqlParameter[] tipsParameters = new SqlParameter[]
            {
                 new SqlParameter("@UserCNP", chatReportToBeSolved.ReportedUserCNP)
            };
            
            int countTips = dbConn.ExecuteScalar<int>("GetNumberOfGivenTipsForUser", tipsParameters, CommandType.StoredProcedure);
            if (countTips % 3 == 0)
            {
                MessagesService services = new MessagesService();
                services.GiveMessageToUser(chatReportToBeSolved.ReportedUserCNP);
            }

            SqlParameter[] activityParameters = new SqlParameter[]
            {
                new SqlParameter("@UserCNP", chatReportToBeSolved.ReportedUserCNP),
                new SqlParameter("@Name", "Chat"),
                new SqlParameter("@LastModifiedAmount", amount),
                new SqlParameter("@Details", "Chat abuse")
            };


            dbConn.ExecuteNonQuery("UpdateActivityLog", activityParameters, CommandType.StoredProcedure);

            return true;
        }
        public async Task<bool> IsMessageOffensive(string messageToBeChecked)
        {
            bool isOffensive = await ProfanityChecker.IsMessageOffensive(messageToBeChecked);
            return isOffensive;
        }

        public void UpdateHistoryForUser(string UserCNP, int NewScore)
        {
            this._chatReportRepository.UpdateHistoryForUser(UserCNP, NewScore);
        }

        public List<ChatReport> GetChatReports()
        {
            return _chatReportRepository.GetChatReports();
        }
    }
}
