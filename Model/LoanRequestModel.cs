using System;

namespace src.Model
{
    public class LoanRequest
    {
        private int _requestID;
        private string _userCNP;
        private float _amount;
        private DateTime _applicationDate;
        private DateTime _repaymentDate;

        public LoanRequest(int requestID, string userCNP, float amount, DateTime applicationDate, DateTime repaymentDate)
        {
            _requestID = requestID;
            _userCNP = userCNP;
            _amount = amount;
            _applicationDate = applicationDate;
            _repaymentDate = repaymentDate;
        }

        public int RequestID
        {
            get { return _requestID; }
            set { _requestID = value; }
        }

        public string UserCNP
        {
            get { return _userCNP; }
            set { _userCNP = value; }
        }

        public float Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public DateTime ApplicationDate
        {
            get { return _applicationDate; }
            set { _applicationDate = value; }
        }

        public DateTime RepaymentDate
        {
            get { return _repaymentDate; }
            set { _repaymentDate = value; }
        }
    }
}
