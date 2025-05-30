﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace src.Helpers
{   
    static public class ProfanityChecker
    {
        private static readonly HttpClient client = new HttpClient();

        static public async Task<bool> IsMessageOffensive(string messageToBeChecked)
        {
            try
            {
                string apiUrl = $"https://www.purgomalum.com/service/containsprofanity?text={Uri.EscapeDataString(messageToBeChecked)}";
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                string result = await response.Content.ReadAsStringAsync();
                return result.Trim().ToLower() == "true";
            }
            catch (Exception)
            {
                return false; // Assume false if API fails
            }
        }
    }
}

