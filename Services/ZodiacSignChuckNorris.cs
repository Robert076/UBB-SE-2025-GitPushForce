using src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Services
{
    class ZodiacSignChuckNorrisService
    {
        public UserZodiacSign User;

        public ZodiacSignChuckNorrisService()
        {
            this.User= new UserZodiacSign(); 
        }

        private int CalculateAsciiSumModulo(string joke)
        {
            int sum = 0;

            foreach (char c in joke)
                ++sum;

            return sum % 10;
        }

        private static readonly Random _random = new Random();

        // TRUE heads / FALSE tails
        private bool CoinFlipSimulation()
        {
            return _random.Next(2) == 0;
        }

        public void ComputCreditScoreBasedOnCoinFlipAndJoke(string joke)
        {
            int asciiResult = CalculateAsciiSumModulo(joke);
            bool coinFlipResult = CoinFlipSimulation();
            decimal percentageFactor = asciiResult / 100M;

            if (coinFlipResult)
                this.User.AmountCredit += this.User.AmountCredit * percentageFactor;

            if (!coinFlipResult)
                this.User.AmountCredit -= this.User.AmountCredit * percentageFactor;
        }


    }
}
