using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class HistoryCreditScore
    {
        private int _historyId;
        private string _userCNP;
        private DateOnly _date;
        private int _creditScore;

        public HistoryCreditScore(int historyId, string userCNP, DateOnly date, int creditScore)
        {
            _historyId = historyId;
            _userCNP = userCNP;
            _date = date;
            _creditScore = creditScore;
        }

        public HistoryCreditScore()
        {
            _historyId = 0;
            _userCNP = string.Empty;
            _date = new DateOnly();
            _creditScore = 0;
        }


        public int HistoryId
        {
            get { return _historyId; }
            set { _historyId = value; }
        }

    }
}
