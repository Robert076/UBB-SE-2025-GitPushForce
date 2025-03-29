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

        private readonly UserRepository _userRepository;
        private static readonly Random random = new Random();

        public ZodiacService(UserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        private static bool FlipCoin()
        {
            return random.Next(2) == 0;
        }

        private static int ComputeJokeAsciiModulo10(string joke)
        {
            int sum = 0;

            if (joke == null)
                throw new ArgumentNullException(nameof(joke));

            foreach (char c in joke)
                sum += (int)c;

            return sum % 10;
        }

        private static int ComputeGravity()
        {
            return random.Next(-10, 11);
        }

        public void CreditScoreModificationBaseOnJokeAndCoinFlip(string userCNP, string joke)
        {
            int asciiJoke = ComputeJokeAsciiModulo10(joke);
            bool flip = FlipCoin();
            User user = _userRepository.GetUserByCNP(userCNP);

            if (user == null)
                throw new Exception("User not found for the provided CNP.");
           

            if (flip)
                user.CreditScore += asciiJoke;
            

            if (!flip)
                user.CreditScore -= asciiJoke;

            _userRepository.UpdateUserCreditScore(user.CNP, user.CreditScore);
        }

        public void CreditScoreModificationBadeOnAtributeAndGravity(string userCNP)
        {
            int gravity = ComputeGravity();
            User user = _userRepository.GetUserByCNP(userCNP);

            if (user == null)
                throw new Exception("User not found for the provided CNP.");

            user.CreditScore += gravity;

            _userRepository.UpdateUserCreditScore(user.CNP, user.CreditScore);
        }


    }
}
