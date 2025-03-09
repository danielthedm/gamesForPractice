using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        HangmanGame game = new HangmanGame();
        game.Start();
    }
}

class HangmanGame
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

class WordBank
{
    private string[] words = { "developer", "hangman", "computer", "software", "programming", "keyboard", "interface" };
    private Random rand = new Random();

    public string GetRandomWord()
    {
        return words[rand.Next(words.Length)];
    }
}

class Display
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
