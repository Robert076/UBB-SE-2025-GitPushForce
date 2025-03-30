using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Model
{
    public class ActivityLog
    {
        private int _id;
        private string _userCNP;
        private string _name;
        private int _amount;
        private string _details;

        public ActivityLog(int id, string userCNP, string name, int amount, string details)
        {
            _id = id;
            _userCNP = userCNP;
            _name = name;
            _amount = amount;
            _details = details;
        }

        public ActivityLog() 
        {
            _id = 0;
            _userCNP = string.Empty;
            _name = string.Empty;
            _amount = 0;
            _details = string.Empty;
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

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public string Details
        {
            get { return _details; }
            set { _details = value; }
        }
    }
}
