using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using src.Model;
using src.Repos;
using System.Net.Http;
using System.Text.Json;

namespace src.Services
{
    public class ZodiacService : IZodiacService
    {

        private readonly IUserRepository _userRepository;
        private static readonly Random random = new Random();

        public ZodiacService(IUserRepository userRepository)
        {

            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        private static bool FlipCoin()
        {
            return random.Next(2) == 0;
        }

        private static int ComputeJokeAsciiModulo10(string joke)
        {
            int jokeCharacterSum = 0;

            if (joke == null)
                throw new ArgumentNullException(nameof(joke));

            foreach (char c in joke)
                jokeCharacterSum += (int)c;

            return jokeCharacterSum % 10;
        }
        public async Task CreditScoreModificationBasedOnJokeAndCoinFlipAsync()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage jokeApiResponse = await client.GetAsync("https://api.chucknorris.io/jokes/random");

            if (!jokeApiResponse.IsSuccessStatusCode)
            {
                throw new Exception("Failed to fetch joke from API.");
            }

            string jsonResponse = await jokeApiResponse.Content.ReadAsStringAsync();
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

                _userRepository.UpdateUserCreditScore(user.Cnp, user.CreditScore);
            }
        }

        private static int ComputeGravity()
        {
            return random.Next(-10, 11);
        }


        public void CreditScoreModificationBasedOnAttributeAndGravity()
        {
            List<User> userList = _userRepository.GetUsers();

            if (userList == null || userList.Count == 0)
                throw new Exception("No users found.");

            foreach (User user in userList)
            {
                int gravityResult = ComputeGravity();
                user.CreditScore += gravityResult;
                _userRepository.UpdateUserCreditScore(user.Cnp, user.CreditScore);
            }
        }


    }
}
