using System;
using System.Collections.Generic;

namespace HangmanNamespace
{
    public class Display
    {
        public void RenderGame(char[] guessedWord, HashSet<char> guessedLetters, int attemptsLeft)
        {
            Console.Clear();
            Console.WriteLine("\n===== HANGMAN GAME =====");
            Console.WriteLine(GetHangmanArt(attemptsLeft));
            Console.WriteLine("\nWord: " + string.Join(" ", guessedWord));
            Console.WriteLine("Guessed Letters: " + string.Join(", ", guessedLetters));
            Console.WriteLine($"Attempts Left: {attemptsLeft}");
        }

        private string GetHangmanArt(int attemptsLeft)
        {
            string[] hangmanStages = new string[]
            {
                "  +---+\n  |   |\n  O   |\n /|\\  |\n / \\  |\n      |\n=========",
                "  +---+\n  |   |\n  O   |\n /|\\  |\n /    |\n      |\n=========",
                "  +---+\n  |   |\n  O   |\n /|\\  |\n      |\n      |\n=========",
                "  +---+\n  |   |\n  O   |\n /|   |\n      |\n      |\n=========",
                "  +---+\n  |   |\n  O   |\n  |   |\n      |\n      |\n=========",
                "  +---+\n  |   |\n  O   |\n      |\n      |\n      |\n=========",
                "  +---+\n  |   |\n      |\n      |\n      |\n      |\n========="
            };

            return hangmanStages[attemptsLeft];
        }
    }
}
