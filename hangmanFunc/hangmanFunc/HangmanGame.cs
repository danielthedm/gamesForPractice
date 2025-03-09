using System;
using System.Collections.Generic;

namespace HangmanNamespace
{
    public class HangmanGame
    {
        private string wordToGuess;
        private char[] guessedWord;
        private HashSet<char> guessedLetters;
        private int attemptsLeft;
        private const int MaxAttempts = 6;
        private WordBank wordBank;
        private Display display;

        public HangmanGame()
        {
            wordBank = new WordBank();
            display = new Display();
            wordToGuess = wordBank.GetRandomWord().ToUpper();
            guessedWord = new string('_', wordToGuess.Length).ToCharArray();
            guessedLetters = new HashSet<char>();
            attemptsLeft = MaxAttempts;
        }

        public void Start()
        {
            while (attemptsLeft > 0 && new string(guessedWord) != wordToGuess)
            {
                display.RenderGame(guessedWord, guessedLetters, attemptsLeft);
                Console.Write("\nEnter a letter: ");
                string input = Console.ReadLine()?.ToUpper();

                if (string.IsNullOrEmpty(input) || input.Length != 1 || !char.IsLetter(input[0]))
                {
                    Console.WriteLine("Invalid input. Please enter a single letter.");
                    continue;
                }

                char guess = input[0];

                if (guessedLetters.Contains(guess))
                {
                    Console.WriteLine("You've already guessed that letter.");
                    continue;
                }

                guessedLetters.Add(guess);

                if (wordToGuess.Contains(guess))
                {
                    for (int i = 0; i < wordToGuess.Length; i++)
                    {
                        if (wordToGuess[i] == guess)
                            guessedWord[i] = guess;
                    }
                }
                else
                {
                    attemptsLeft--;
                    Console.WriteLine($"Incorrect guess! {attemptsLeft} attempts remaining.");
                }
            }

            EndGame();
        }

        private void EndGame()
        {
            display.RenderGame(guessedWord, guessedLetters, attemptsLeft);

            if (new string(guessedWord) == wordToGuess)
                Console.WriteLine("\n🎉 Congratulations! You guessed the word!");
            else
                Console.WriteLine($"\n💀 Game Over! The word was: {wordToGuess}");

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
