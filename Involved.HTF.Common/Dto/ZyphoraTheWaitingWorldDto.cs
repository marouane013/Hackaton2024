using System;

namespace ZyphoraTheWaitingWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            // Voorbeeld input
            ZyphoraTheWaitingWorldDto input = new ZyphoraTheWaitingWorldDto
            {
                SendDateTime = DateTime.Parse("2024-12-06T07:59:15Z"),
                TravelSpeed = 415, // lichtjaar per minuut
                Distance = 376487, // lichtjaar
                DayLength = 9 // uren in een dag op de planeet
            };

            // Bereken het verwachte antwoordtijdstip
            string result = CalculateResponseTime(input);

            // Toon het resultaat
            Console.WriteLine($"Antwoord: {result}");
        }

        public static string CalculateResponseTime(ZyphoraTheWaitingWorldDto data)
        {
            if (data.TravelSpeed <= 0 || data.Distance <= 0 || data.DayLength <= 0)
                throw new ArgumentException("Alle invoerwaarden moeten groter zijn dan 0.");

            // Bereken totale reistijd (heen en terug) in minuten
            double totalTravelTimeMinutes = 2.0 * data.Distance / data.TravelSpeed;

            // Bereken het aantal volledige planeetdagen en resterende minuten
            int minutesPerPlanetDay = data.DayLength * 60; // Minuten in één planeetdag
            int totalTravelDays = (int)(totalTravelTimeMinutes / minutesPerPlanetDay); // Aantal volledige planeetdagen
            double remainingMinutes = totalTravelTimeMinutes % minutesPerPlanetDay; // Minuten over na volledige dagen

            // Voeg de planeetdagen en resterende minuten toe aan de verzendtijd
            DateTime planetResponseTime = data.SendDateTime
                .AddHours(totalTravelDays * data.DayLength) // Voeg volledige planeetdagen toe
                .AddMinutes(remainingMinutes); // Voeg resterende minuten toe

            // Formatteer het resultaat naar ISO 8601 UTC
            return planetResponseTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }
    }

    public class ZyphoraTheWaitingWorldDto
    {
        public DateTime SendDateTime { get; set; }
        public int TravelSpeed { get; set; }
        public int Distance { get; set; }
        public int DayLength { get; set; }
    }
}
