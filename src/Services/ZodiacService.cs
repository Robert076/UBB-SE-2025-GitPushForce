using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Src.Model;
using Src.Repos;

namespace Src.Services
{
    public class ZodiacService : IZodiacService
    {
        private readonly IUserRepository userRepository;
        private static readonly Random Random = new Random();
        private readonly HttpClient httpClient;

        public ZodiacService(IUserRepository userRepository, HttpClient httpClient)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }


        
        public bool FlipCoin()
        {
            return Random.Next(2) == 0;
        }

        public int ComputeJokeAsciiModulo10(string joke)
        {
            if (joke == null)
            {
                throw new ArgumentNullException(nameof(joke));
            }

            int jokeCharacterSum = 0;
            foreach (char character in joke)
            {
                jokeCharacterSum += (int)character;
            }

            return jokeCharacterSum % 10;
        }

        public async Task CreditScoreModificationBasedOnJokeAndCoinFlipAsync()
        {
            
            HttpResponseMessage jokeApiResponse = await httpClient.GetAsync("https://api.chucknorris.io/jokes/random");

            if (!jokeApiResponse.IsSuccessStatusCode)
            {
                throw new Exception("Failed to fetch joke from API.");
            }

            string jsonResponse = await jokeApiResponse.Content.ReadAsStringAsync();
            using JsonDocument doc = JsonDocument.Parse(jsonResponse);
            string joke = doc.RootElement.GetProperty("value").GetString();

            int asciiJokeModulo10 = ComputeJokeAsciiModulo10(joke);
            List<User> users = userRepository.GetUsers();
            bool flip = FlipCoin();

            foreach (User user in users)
            {
                user.CreditScore += flip ? asciiJokeModulo10 : -asciiJokeModulo10;
                userRepository.UpdateUserCreditScore(user.Cnp, user.CreditScore);
            }
        }

        public int ComputeGravity()
        {
            return Random.Next(-10, 11);
        }

        public void CreditScoreModificationBasedOnAttributeAndGravity()
        {
            List<User> userList = userRepository.GetUsers();

            if (userList == null || userList.Count == 0)
            {
                throw new Exception("No users found.");
            }

            foreach (User user in userList)
            {
                int gravityResult = ComputeGravity();
                user.CreditScore += gravityResult;
                userRepository.UpdateUserCreditScore(user.Cnp, user.CreditScore);
            }
        }
    }
}
