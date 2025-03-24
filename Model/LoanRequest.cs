using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class LoanRequest
    {
        private int _loanID;
        private string _userCNP;
        private float _loanAmount;
        private DateTime _applicationDate;
        private DateTime _repaymentDate;
        private float _interestRate;
        private int _noMonths;
        private float _monthlyPaymentAmount;
        private string _approvalStatus;
        private string _state = "active";
        private int _monthlyPaymentsCompleted;
        private float _repaidAmount;
        private float _penalty;

        public LoanRequest(int loanID, string userCNP, float loanAmount, DateTime repaymentDate, float interestRate, int noMonths, float monthlyPaymentAmount)
        {
            _loanID = loanID;
            _userCNP = userCNP;
            _loanAmount = loanAmount;
            _applicationDate = DateTime.Today;
            _repaymentDate = repaymentDate;
            _interestRate = interestRate;
            _noMonths = noMonths;
            _monthlyPaymentAmount = monthlyPaymentAmount;
            _approvalStatus = "pending";
            _state = "active";
            _monthlyPaymentsCompleted = 0;
            _repaidAmount = 0;
            _penalty = 0;
        }

        public int LoanID
        {
            get { return _loanID; }
            set { _loanID = value; }
        }

        public string UserCNP
        {
            get { return _userCNP; }
            set { _userCNP = value; }
        }

        public float LoanAmount
        {
            get { return _loanAmount; }
            set { _loanAmount = value; }
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

        public float InterestRate
        {
            get { return _interestRate; }
            set { _interestRate = value; }
        }

        public int NoMonths
        {
            get { return _noMonths; }
            set { _noMonths = value; }
        }

        public float MonthlyPaymentAmount
        {
            get { return _monthlyPaymentAmount; }
            set { _monthlyPaymentAmount = value; }
        }

        public string ApprovalStatus
        {
            get { return _approvalStatus; }
            set { _approvalStatus = value; }
        }

        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        public int MonthlyPaymentsCompleted
        {
            get { return _monthlyPaymentsCompleted; }
            set { _monthlyPaymentsCompleted = value; }
        }

        public float RepaidAmount
        {
            get { return _repaidAmount; }
            set { _repaidAmount = value; }
        }

        public float Penalty
        {
            get { return _penalty; }
            set { _penalty = value; }
        }
    }
}
