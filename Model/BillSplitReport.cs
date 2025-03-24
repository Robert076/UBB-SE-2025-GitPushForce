using System;

namespace src.Model
{
    public class BillSplitReport
    {
        private int _id;
        private string _reportedCNP;
        private string _reporterCNP;
        private DateTime _dateTransaction;
        private float _billShare;
        private float _gravityFactor;

        public BillSplitReport(int id, string reportedCNP, string reporterCNP, DateTime dateTransaction, float billShare, float gravityFactor)
        {
            _id = id;
            _reportedCNP = reportedCNP;
            _reporterCNP = reporterCNP;
            _dateTransaction = dateTransaction;
            _billShare = billShare;
            _gravityFactor = gravityFactor;
        }

        public BillSplitReport()
        {
            _id = 0;
            _reportedCNP = string.Empty;
            _reporterCNP = string.Empty;
            _dateTransaction = DateTime.Now;
            _billShare = 0;
            _gravityFactor = 0;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string ReportedCNP
        {
            get { return _reportedCNP; }
            set { _reportedCNP = value; }
        }

        public string ReporterCNP
        {
            get { return _reporterCNP; }
            set { _reporterCNP = value; }
        }

        public DateTime DateTransaction
        {
            get { return _dateTransaction; }
            set { _dateTransaction = value; }
        }

        public float BillShare
        {
            get { return _billShare; }
            set { _billShare = value; }
        }

        public float GravityFactor
        {
            get { return _gravityFactor; }
            set { _gravityFactor = value; }
        }
    }
}