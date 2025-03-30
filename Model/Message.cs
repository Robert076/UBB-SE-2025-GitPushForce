using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Model
{
    public class Message
    {
        private int _id;
        private string _type; 
        private string _message;

        public Message(int id, string type, string message)
        {
            _id = id;
            _type = type;
            _message = message;
        }

        public Message()
        {
            _id = 0;
            _type = string.Empty;
            _message = string.Empty;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public string MessageText
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}
