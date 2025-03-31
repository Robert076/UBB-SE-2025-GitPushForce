using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Model;
using src.Repos;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;



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

        private static int VerificationCreditSocreLimits(int creditScore)
        {
            if (creditScore < 100)
                return 100;

            if (creditScore > 700)
                return 700;

            return creditScore;
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
        public async Task CreditScoreModificationBasedOnJokeAndCoinFlipAsync()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://api.chucknorris.io/jokes/random");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to fetch joke from API.");
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();
            using JsonDocument doc = JsonDocument.Parse(jsonResponse);
            string joke = doc.RootElement.GetProperty("value").GetString();

            int asciiJokeModulo10 = ComputeJokeAsciiModulo10(joke);
            List<User> users = _userRepository.GetUsers();
            bool flip = FlipCoin();

            foreach (User user in users)
            {
                if (flip)
                {
                    user.CreditScore += asciiJokeModulo10;
                }
                else
                {
                    user.CreditScore -= asciiJokeModulo10;
                }

                user.CreditScore = VerificationCreditSocreLimits(user.CreditScore);

                _userRepository.UpdateUserCreditScore(user.CNP, user.CreditScore);
            }
        }

        private static int ComputeGravity()
        {
            return random.Next(-10, 11);
        }


        public void CreditScoreModificationBasedOnAttributeAndGravity()
        {
            List<User> users = _userRepository.GetUsers();

            if (users == null || users.Count == 0)
                throw new Exception("No users found.");

            foreach (User user in users)
            {
                int gravityResult = ComputeGravity();
                user.CreditScore += gravityResult;

                user.CreditScore = VerificationCreditSocreLimits(user.CreditScore);
                _userRepository.UpdateUserCreditScore(user.CNP, user.CreditScore);
            }
        }


    }
}
