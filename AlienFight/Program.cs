using System;
using System.Collections.Generic;
using System.Text.Json;
using Involved.HTF.Common.Dto;

class Program
{
    static void Main(string[] args)
    {
        // JSON input als string
        string jsonInput = @"
        {
          ""teamA"": [
            { ""strength"": 42, ""speed"": 56, ""health"": 59 },
            { ""strength"": 59, ""speed"": 23, ""health"": 16 },
            { ""strength"": 96, ""speed"": 23, ""health"": 29 },
            { ""strength"": 85, ""speed"": 70, ""health"": 37 },
            { ""strength"": 73, ""speed"": 11, ""health"": 3 }
          ],
          ""teamB"": [
            { ""strength"": 18, ""speed"": 48, ""health"": 104 },
            { ""strength"": 32, ""speed"": 47, ""health"": 141 },
            { ""strength"": 15, ""speed"": 72, ""health"": 108 },
            { ""strength"": 29, ""speed"": 40, ""health"": 90 },
            { ""strength"": 12, ""speed"": 42, ""health"": 84 }
          ]
        }";

        try
        {
            // Parse JSON naar BattleOfNovaCentauriDto
            var battleData = JsonSerializer.Deserialize<BattleOfNovaCentauriDto>(jsonInput);

            if (battleData == null)
            {
                Console.WriteLine("Deserialisatie mislukt: Ongeldige JSON.");
                return;
            }

            // Controleer of beide teams correct geladen zijn
            if (battleData.TeamA == null || battleData.TeamB == null)
            {
                Console.WriteLine("Eén van de teams is null.");
            }
            else
            {
                Console.WriteLine($"Team A heeft {battleData.TeamA.Count} leden.");
                Console.WriteLine($"Team B heeft {battleData.TeamB.Count} leden.");
            }

            // Simuleer het gevecht
            var result = SimulateBattle(battleData);

            // Toon het resultaat
            Console.WriteLine($"Winnend team: {result.TeamName}");
            Console.WriteLine($"Totale resterende gezondheid: {result.TotalHealth}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Er trad een fout op: {ex.Message}");
        }
    }

    public static BattleResult SimulateBattle(BattleOfNovaCentauriDto battleData)
    {
        if (battleData.TeamA == null || battleData.TeamB == null)
        {
            throw new ArgumentNullException("TeamA of TeamB", "Eén van de teams in de JSON is null.");
        }

        var teamA = battleData.TeamA.Where(a => a != null).ToList();
        var teamB = battleData.TeamB.Where(b => b != null).ToList();

        while (teamA.Any(a => a.Health > 0) && teamB.Any(b => b.Health > 0))
        {
            var fighterA = teamA.FirstOrDefault(a => a.Health > 0);
            var fighterB = teamB.FirstOrDefault(b => b.Health > 0);

            if (fighterA == null || fighterB == null)
            {
                break;
            }

            Fight(fighterA, fighterB);
        }

        if (teamA.Any(a => a.Health > 0))
        {
            return new BattleResult
            {
                TeamName = "TeamA",
                TotalHealth = teamA.Where(a => a.Health > 0).Sum(a => a.Health)
            };
        }
        else
        {
            return new BattleResult
            {
                TeamName = "TeamB",
                TotalHealth = teamB.Where(b => b.Health > 0).Sum(b => b.Health)
            };
        }
    }

    public static void Fight(Alien alienA, Alien alienB)
    {
        while (alienA.Health > 0 && alienB.Health > 0)
        {
            if (alienA.Speed >= alienB.Speed)
            {
                alienB.Health -= alienA.Strength;
                if (alienB.Health <= 0) break;

                alienA.Health -= alienB.Strength;
            }
            else
            {
                alienA.Health -= alienB.Strength;
                if (alienA.Health <= 0) break;

                alienB.Health -= alienA.Strength;
            }
        }
    }
}

public class BattleOfNovaCentauriDto
{
    public List<Alien> TeamA { get; set; } = new List<Alien>();
    public List<Alien> TeamB { get; set; } = new List<Alien>();
}

public class Alien
{
    public int Strength { get; set; }
    public int Speed { get; set; }
    public int Health { get; set; }
}

public class BattleResult
{
    public string TeamName { get; set; }
    public int TotalHealth { get; set; }
}
