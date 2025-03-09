using System;

class Program
{
    static void Main()
    {
        RockPaperScissorsGame game = new RockPaperScissorsGame();
        game.Start();
    }
}

class RockPaperScissorsGame
{
    private string[] choices = { "Rock", "Paper", "Scissors" };
    private Random rand = new Random();
    private ScoreTracker scoreTracker = new ScoreTracker();

    public void Start()
    {
        Console.WriteLine("===== Rock Paper Scissors =====");

        while (true)
        {
            Console.Write("\nEnter Rock, Paper, or Scissors (or type 'exit' to quit): ");
            string playerChoice = Console.ReadLine()?.Trim().ToLower();

            if (playerChoice == "exit")
            {
                Console.WriteLine($"Final Score: You {scoreTracker.PlayerScore} - Computer {scoreTracker.ComputerScore}");
                Console.WriteLine("Thanks for playing!");
                break;
            }

            if (!IsValidChoice(playerChoice))
            {
                Console.WriteLine("Invalid choice! Please enter Rock, Paper, or Scissors.");
                continue;
            }

            string computerChoice = choices[rand.Next(choices.Length)];
            Console.WriteLine($"Computer chose: {computerChoice}");

            string result = DetermineWinner(playerChoice, computerChoice.ToLower());
            scoreTracker.UpdateScore(result);
            Console.WriteLine(result);
        }
    }

    private bool IsValidChoice(string choice)
    {
        return choice == "rock" || choice == "paper" || choice == "scissors";
    }

    private string DetermineWinner(string player, string computer)
    {
        if (player == computer)
            return "It's a tie!";

        if ((player == "rock" && computer == "scissors") ||
            (player == "paper" && computer == "rock") ||
            (player == "scissors" && computer == "paper"))
        {
            return "You win!";
        }

        return "You lose!";
    }
}

class ScoreTracker
{
    public int PlayerScore { get; private set; } = 0;
    public int ComputerScore { get; private set; } = 0;

    public void UpdateScore(string result)
    {
        if (result == "You win!") PlayerScore++;
        else if (result == "You lose!") ComputerScore++;
    }
}
