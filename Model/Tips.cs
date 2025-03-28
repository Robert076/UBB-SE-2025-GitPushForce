using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Model
{
    class Tips
    {
        private int _id;
        private String _creditScoreBracket;
        private String _tipText;

        public Tips(int id, String creditScoreBracket, String tipText)
        {
            _id = id;
            _creditScoreBracket = creditScoreBracket;
            _tipText = tipText;
        }

        public Tips()
        {
            _id = 0;
            _creditScoreBracket = string.Empty;
            _tipText = string.Empty;
        }

        public int id
        {
            get { return _id;  }
            set { _id = value; }
        }

        public String creditScoreBracket
        {
            get { return _creditScoreBracket; }
            set { _creditScoreBracket = value; }
        }

        public String tipText
        {
            get { return _tipText; }
            set { _tipText = value; }
        }
    }
}
