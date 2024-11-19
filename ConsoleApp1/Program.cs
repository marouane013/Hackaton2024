using System;
using System.Collections.Generic;

namespace HackTheFuture.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            // Stel console in op UTF-8
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Alien boodschap
            string alienMessage = "λ⊕ᚦΣ⎍ζ⊗Ω ∆⍟⊕ᚦΣ ✦λΣ ⍟∆⎍⎍Σ⊗ ϡ∆φ✦Σ◯∆⊗Ψφ ⊕ϕ ∆⊗ ∆⍟∆⊗Ψ⊕⊗ΣΨ ◊⊕◯⊕⊗⍝, ✦λΣ ◊⊕◯⊕φφ∆◯ ⧖⊕✦λΣ⎍ φλζ∇ ◊∆φ✦ ∆ φλ∆Ψ⊕ϡ ✦λ∆✦ φ∇∆⊗⊗ΣΨ ⧖ζ◯Σφ, ζ✦φ ∆◯ζΣ⊗ ∆⎍◊λζ✦Σ◊✦⨅⎍Σ ∆ ⧖⍝φ✦Σ⎍⍝ ✦⊕ ∆◯◯ ϡλ⊕ λ∆Ψ Ψ∆⎍ΣΨ ∆∇∇⎍⊕∆◊λ, ϡζ✦λ ⊗⊕ ᚦζφζ⍟◯Σ Σ⊗✦⎍⍝ ∇⊕ζ⊗✦ ⊕⎍ φζΩ⊗ ⊕ϕ ζ✦φ ◊⎍Σ∆✦⊕⎍φ.";

            // Decodeer de boodschap
            string decodedMessage = DecodeAlienMessage(alienMessage);
            Console.WriteLine($"Decoded message: {decodedMessage}");
        }

        public static string DecodeAlienMessage(string alienMessage)
        {
            // Hardcoded mapping tussen alien symbols en ons alfabet
            var alphabetMapping = new Dictionary<string, string>
            {
                { "∆", "A" }, { "⍟", "B" }, { "◊", "C" }, { "Ψ", "D" },
                { "Σ", "E" }, { "ϕ", "F" }, { "Ω", "G" }, { "λ", "H" },
                { "ζ", "I" }, { "Ϭ", "J" }, { "ↄ", "K" }, { "◯", "L" },
                { "⧖", "M" }, { "⊗", "N" }, { "⊕", "O" }, { "∇", "P" },
                { "⟁", "Q" }, { "⎍", "R" }, { "φ", "S" }, { "✦", "T" },
                { "⨅", "U" }, { "ᚦ", "V" }, { "ϡ", "W" }, { "⍾", "X" },
                { "⍝", "Y" }, { "≈", "Z" }
            };

            string decodedMessage = string.Empty;

            foreach (var symbol in alienMessage)
            {
                string symbolString = symbol.ToString();

                // Controleer of het symbool in de mapping zit
                if (alphabetMapping.ContainsKey(symbolString))
                {
                    decodedMessage += alphabetMapping[symbolString];
                }
                else
                {
                    decodedMessage += symbol; // Houdt spaties en leestekens
                }
            }

            return decodedMessage.ToUpper(); // Case-insensitief
        }
    }
}
