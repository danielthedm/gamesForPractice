using System;

class Program
{
    static void Main()
    {
        NumberGuessingGame game = new NumberGuessingGame();
        game.Start();
    }
}

class NumberGuessingGame
{
    private Random rand = new Random();
    private int targetNumber;
    private int maxAttempts;
    private ScoreTracker scoreTracker = new ScoreTracker();

    public void Start()
    {
        Console.WriteLine("===== 🎯 Number Guessing Game 🎯 =====");
        SetDifficulty();

        while (true)
        {
            targetNumber = rand.Next(1, 101); // Random number between 1 and 100
            PlayRound();

            Console.Write("\nPlay again? (yes/no): ");
            if (!Console.ReadLine().Trim().ToLower().StartsWith("y"))
            {
                Console.WriteLine($"Final Score: {scoreTracker.GamesWon} Wins, {scoreTracker.GamesLost} Losses");
                Console.WriteLine("Thanks for playing!");
                break;
            }
        }
    }

    private void SetDifficulty()
    {
        while (true)
        {
            Console.Write("\nChoose difficulty (Easy, Medium, Hard): ");
            string difficulty = Console.ReadLine()?.Trim().ToLower();

            if (difficulty == "easy") { maxAttempts = 10; break; }
            if (difficulty == "medium") { maxAttempts = 7; break; }
            if (difficulty == "hard") { maxAttempts = 5; break; }

            Console.WriteLine("Invalid choice. Please enter Easy, Medium, or Hard.");
        }
    }

    private void PlayRound()
    {
        int attempts = 0;

        Console.WriteLine($"\nI have chosen a number between 1 and 100. You have {maxAttempts} attempts. Good luck!");

        while (attempts < maxAttempts)
        {
            Console.Write("\nEnter your guess: ");
            if (!int.TryParse(Console.ReadLine(), out int guess) || guess < 1 || guess > 100)
            {
                Console.WriteLine("Invalid input! Enter a number between 1 and 100.");
                continue;
            }

            attempts++;

            if (guess == targetNumber)
            {
                Console.WriteLine($"🎉 Correct! You guessed the number in {attempts} attempts.");
                scoreTracker.IncreaseWins();
                return;
            }

            Console.WriteLine(guess < targetNumber ? "📈 Too low!" : "📉 Too high!");
            Console.WriteLine($"Attempts left: {maxAttempts - attempts}");
        }

        Console.WriteLine($"\n💀 Out of attempts! The correct number was {targetNumber}.");
        scoreTracker.IncreaseLosses();
    }
}

class ScoreTracker
{
    public int GamesWon { get; private set; } = 0;
    public int GamesLost { get; private set; } = 0;

    public void IncreaseWins() => GamesWon++;
    public void IncreaseLosses() => GamesLost++;
}
