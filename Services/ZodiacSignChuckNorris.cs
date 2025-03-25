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
        private static readonly Random _random = new Random();
        private string[] _attributeArray =
        {
            "Courage",
            "Patience",
            "Adaptability",
            "Empathy",
            "Generosity",
            "Perfectionism",
            "Balance",
            "Passion",
            "Optimism",
            "Ambition",
            "Originality",
            "Intuition"
         };

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

        public void RandomAssignAtributeToUser()
        {
            int randomIndex = _random.Next(0, _attributeArray.Length);
            int randomValue = _random.Next(-10, 11); // random value [-10 , 10] to increase or decrease the credit line

            this.User.SignAtributes = _attributeArray[randomIndex];
        }


    }
}
