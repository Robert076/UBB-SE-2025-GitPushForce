using System;

namespace src.Model
{
    public class ChatReport
    {
        public int Id { get; set; }
        public string ReportedUserCnp { get; set; }
        public string ReportedMessage { get; set; }

        public ChatReport(int id = 0, string reportedUserCNP = "", string reportedMessage = "")
        {
            Id = id;
            ReportedUserCnp = reportedUserCNP;
            ReportedMessage = reportedMessage;
        }
    }
}
