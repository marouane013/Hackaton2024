using System;

public class ZyphoraResponseTimeCalculator
{
    public string CalculateResponseTime(string sendDateTime, double travelSpeed, double distance, int dayLength)
    {
        DateTime sent = DateTime.Parse(sendDateTime);

        double travelTimeMinutes = (distance / travelSpeed) * 2; 

        int totalMinutesInADay = dayLength * 60;
        int fullDays = (int)(travelTimeMinutes / totalMinutesInADay); 
        double remainingMinutes = travelTimeMinutes % totalMinutesInADay; 

        DateTime responseTime = sent.AddDays(fullDays).AddMinutes(remainingMinutes);

        // Controleer of de uren overschrijden en voeg extra dagen toe indien nodig
        while (responseTime.Hour >= dayLength)
        {
            responseTime = responseTime.AddDays(1);
            responseTime = responseTime.AddHours(-dayLength); // Trek de uren van een daglengte af
        }

        return responseTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
    }
}

class Program
{
    static void Main(string[] args)
    {
        ZyphoraResponseTimeCalculator calculator = new ZyphoraResponseTimeCalculator();

        string sendDateTime = "2024-12-04T04:43:39";
        double travelSpeed = 332; 
        double distance = 191624; 
        int dayLength = 6; 

        string responseTime = calculator.CalculateResponseTime(sendDateTime, travelSpeed, distance, dayLength);
        Console.WriteLine($"Antwoord: {responseTime}");
    }
}
