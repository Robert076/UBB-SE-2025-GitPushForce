using System;

namespace src.Model
{
    public class ChatReport
    {
        private int _id;
        private string _reportedUserCNP;
        private string _reportedMessage;

        public ChatReport(int id = 0, string reportedUserCNP = "", string reportedMessage = "")
        {
            _id = id;
            _reportedUserCNP = reportedUserCNP;
            _reportedMessage = reportedMessage;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string ReportedUserCNP
        {
            get { return _reportedUserCNP; }
            set { _reportedUserCNP = value; }
        }

        public string ReportedMessage
        {
            get { return _reportedMessage; }
            set { _reportedMessage = value; }
        }
    }
}
