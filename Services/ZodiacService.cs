using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Model;
using src.Repos;


namespace src.Services
{
    public class ZodiacService
    {

        private static readonly Random random = new Random();

        private static bool FlipCoin()
        {
            return random.Next(2) == 0;
        }

        private static int ComputeJokeAsciiModulo10(string joke)
        {
            if (joke == null)
                throw new ArgumentNullException(nameof(joke));

            int sum = 0;

            foreach (char c in joke)
                sum += (int)c;

            return sum % 10;
        }

        public int CreditScoreModificationBaseOnJokeAndCoinFlip(int CreditScore)
        {


        }


    }
}
