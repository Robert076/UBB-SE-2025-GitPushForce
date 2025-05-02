using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Model
{
   public class Tip
    {
        public int Id { get; set; }
        public String CreditScoreBracket { get; set; }
        public String TipText { get; set; }


        public Tip(int id, String creditScoreBracket, String tipText)
        {
            Id = id;
            CreditScoreBracket = creditScoreBracket;
            TipText = tipText;
        }

        public Tip()
        {
            Id = 0;
            CreditScoreBracket = string.Empty;
            TipText = string.Empty;
        }
    }
}
