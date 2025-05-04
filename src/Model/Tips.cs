using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Model
{
   public class Tip
    {
        public int Id { get; set; }
        public string CreditScoreBracket { get; set; }
        public string TipText { get; set; }

        public Tip(int id, string creditScoreBracket, string tipText)
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
