

using System.Text.Json;
using System.Text;
using System.Net.Http.Json;

namespace Involved.HTF.Common;

public class HackTheFutureClient : HttpClient
{
    public HackTheFutureClient()
    {
        BaseAddress = new Uri("https://app-htf-2024.azurewebsites.net/");
    }

    public async Task Login(string teamname, string password)
    {
        var encodedTeamname = Uri.EscapeDataString(teamname);
        var response = await GetAsync($"/api/team/token?teamname={encodedTeamname}&password={password}");

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Login mislukt: {response.StatusCode}");
            Console.WriteLine($"Foutmelding: {await response.Content.ReadAsStringAsync()}");
            throw new Exception("Login mislukt");
        }

        var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
        if (tokenResponse == null || string.IsNullOrWhiteSpace(tokenResponse.Token))
        {
            throw new Exception("Geen geldige token ontvangen.");
        }

        Console.WriteLine($"Token ontvangen: {tokenResponse.Token}");
        DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenResponse.Token);
    }

    public async Task SolveRandomChallenge()
    {
        Console.WriteLine("Ophalen van random commando's...");
        var response = await GetAsync("/api/a/easy/sample");

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Fout bij ophalen van commando's: {response.StatusCode}");
            throw new Exception("Kon de commando's niet ophalen.");
        }

        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Volledige respons: {content}");

        var commandResponse = JsonSerializer.Deserialize<CommandResponse>(content);
        if (commandResponse == null || string.IsNullOrWhiteSpace(commandResponse.Commands))
        {
            throw new Exception("Geen commando's ontvangen.");
        }

        Console.WriteLine($"Ontvangen commando's: {commandResponse.Commands}");

        // Bereken resultaat
        int result = CalculateDepthAndDistance(commandResponse.Commands);
        Console.WriteLine($"Berekend antwoord: {result}");

        // Stuur resultaat terug
        await SubmitAnswer(result);
    }

    private int CalculateDepthAndDistance(string commands)
    {
        int currentDepthRate = 0;
        int totalDepth = 0;
        int totalDistance = 0;

        foreach (var command in commands.Split(','))
        {
            var parts = command.Trim().Split(' ');

            string action = parts[0];
            int value = int.Parse(parts[1]);

            switch (action)
            {
                case "Down":
                    currentDepthRate += value;
                    break;
                case "Up":
                    currentDepthRate -= value;
                    break;
                case "Forward":
                    totalDistance += value;
                    totalDepth += currentDepthRate * value;
                    break;
            }
        }

        return totalDepth * totalDistance;
    }

    private async Task SubmitAnswer(int answer)
    {
        Console.WriteLine($"Indienen van het antwoord: {answer}");
        var jsonContent = JsonSerializer.Serialize(new { answer });
        var contentToPost = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await PostAsync("/api/a/easy/sample", contentToPost);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Fout bij indienen: {response.StatusCode}");
            Console.WriteLine($"Response: {await response.Content.ReadAsStringAsync()}");
            throw new Exception("Kon het antwoord niet indienen.");
        }

        Console.WriteLine("Antwoord succesvol ingediend!");
    }

    private class CommandResponse
    {
        public string Commands { get; set; } = string.Empty;
    }

    private class TokenResponse
    {
        public string Token { get; set; } = string.Empty;
    }
}