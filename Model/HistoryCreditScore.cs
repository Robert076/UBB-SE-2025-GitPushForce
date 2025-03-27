using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Model
{
    public class HistoryCreditScore
    {
        private int _id;
        private string _userCNP;
        private DateOnly _date;
        private int _creditScore;

        public HistoryCreditScore(int id, string userCNP, DateOnly date, int creditScore)
        {
            _id = id;
            _userCNP = userCNP;
            _date = date;
            _creditScore = creditScore;
        }

        public HistoryCreditScore()
        {
            _id = 0;
            _userCNP = string.Empty;
            _date = new DateOnly();
            _creditScore = 0;
        }


        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string UserCNP
        {
            get { return _userCNP; }
            set { _userCNP = value; }
        }

        public DateOnly Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public int CreditScore
        {
            get { return _creditScore; }
            set { _creditScore = value; }
        }
    }
}
