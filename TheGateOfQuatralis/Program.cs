using System;

class Program
{
    static void Main(string[] args)
    {
        string[] quatralianNumbers = new string[]
        {
           "· |||",
    "·· ···",
    "|···",
    "·",
    "· Ⱄ",
    "|||",
    "·· |···",
    "|||·",
    "· ||···",
    "· |····",
    "||",
    "|··",
    "· |||",
    "·· |",
    "|||··",
    "·· |····",
    "|···",
    "·· ·",
    "|·",
    "·· |····",
    "· ||··",
    "· ·",
    "|·",
    "· |||···",
    "|||·",
    "· ··",
    "· |||",
    "· ||·",
    "· ||",
    "·· ···",
    "· ||····",
    "|||",
    "· ||·",
    "· |",
    "· |||",
    "|||",
    "|||··",
    "·· ·",
    "· |||",
    "·· ·",
    "||··",
    "· ||··",
    "· |||····",
    "·· Ⱄ",
    "·· ··",
    "|",
    "|||",
    "· ··",
    "· |||···",
    "||····"
        };

        var solver = new QuatralianSolver();
        string result = solver.SolveQuatralianPuzzle(quatralianNumbers);

        Console.WriteLine($"Resultaat in Quatraliaans: {result}");
    }
}

public class QuatralianSolver
{
    public string SolveQuatralianPuzzle(string[] quatralianNumbers)
    {
        int totalDecimal = 0;

        foreach (string quatralianNumber in quatralianNumbers)
        {
            int decimalValue = 0;
            string[] quatralianDigits = quatralianNumber.Split(' ');

            foreach (string quatralianDigit in quatralianDigits)
            {
                int digitValue = 0;
                foreach (char c in quatralianDigit)
                {
                    if (c == '·') // Eenheid
                        digitValue++;
                    else if (c == '|') // Groep van 5
                        digitValue += 5;
                    else if (c == 'Ⱄ') // Nul
                        digitValue = 0;
                }
                decimalValue = decimalValue * 20 + digitValue; // Base-20 calculatie
            }

            totalDecimal += decimalValue; // Som van alle getallen
        }

        Console.WriteLine($"Totaal in decimaal: {totalDecimal}");

        // Decimaal naar Quatraliaans converteren
        string result = ConvertToQuatralian(totalDecimal);

        return result.Trim();
    }

    private string ConvertToQuatralian(int decimalValue)
    {
        if (decimalValue == 0) return "Ⱄ";

        string result = "";

        while (decimalValue > 0)
        {
            int remainder = decimalValue % 20;
            decimalValue /= 20;

            if (remainder == 0)
                result = "Ⱄ " + result;
            else
            {
                string digit = "";

                // Voeg punten en strepen toe op basis van de waarde
                digit += new string('|', remainder / 5);
                digit += new string('·', remainder % 5);

                result = digit + " " + result;
            }
        }

        return result.Trim();
    }
}
