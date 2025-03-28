using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Model
{
    public class Messages
    {
        private int _id;
        private string _type; 
        private string _message;

        public Messages(int id, string type, string message)
        {
            _id = id;
            _type = type;
            _message = message;
        }

        public Messages()
        {
            _id = 0;
            _type = string.Empty;
            _message = string.Empty;
        }

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string type
        {
            get { return _type; }
            set { _type = value; }
        }
    }
}
