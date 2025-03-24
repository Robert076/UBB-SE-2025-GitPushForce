using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Model
{
    public class ChatReportModel
    {
        private int _id;
        private string _reportedUserCNP;
        private string _reportedMessage;

        public ChatReportModel(int id, string reportedUserCNP, string reportedMessage)
        {
            _id = id;
            _reportedUserCNP = reportedUserCNP;
            _reportedMessage = reportedMessage;
        }

        public ChatReportModel()
        {
            _id = 0;
            _reportedUserCNP = "";
            _reportedMessage = "";
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
