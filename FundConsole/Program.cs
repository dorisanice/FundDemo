using System.Text;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using FundConsole;

class Program
{
    private static readonly HttpClientHandler handler = new HttpClientHandler()
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
    private static readonly HttpClient client = new HttpClient(handler);

    static async Task Main(string[] args)
    {
        string username = Prompt("Input Username:");
        string password = Prompt("Input Password:");

        string token = await AuthenticateAsync(username, password);
        if (!string.IsNullOrEmpty(token))
        {
            while (true)
            {
                string query = Prompt("Input your query: ");
                if (query.ToLower() == "exit")
                {
                    break;
                }

                await QueryFundsAsync(token, query);
            }

        }
        else {
            Console.WriteLine("Authentication failed");
        }

        static string Prompt(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        static async Task<string> AuthenticateAsync(string username, string password)
        {
            var authData = new
            {
                Username = username,
                Password = password

            };
            var content = new StringContent(JsonConvert.SerializeObject(authData), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("https://localhost:7288/api/Account/login", content);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var tokenData = JObject.Parse(responseBody);
                return tokenData["token"].ToString();
            }
            return null;
        }

        static async Task QueryFundsAsync (string token, string query)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync($"https://localhost:7288/api/Funds?query={query}");

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var funds = JsonConvert.DeserializeObject<List<Fund>>(responseBody);

                Console.WriteLine("Results:");
                foreach (var fund in funds)
                {
                    Console.WriteLine($"{fund.Name} | {fund.Ticker}");

                }
            }
            else {
                Console.WriteLine("Error querying funds");
            }
        }
    }
}