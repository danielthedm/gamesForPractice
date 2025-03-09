using System;

class Program
{
    static void Main()
    {
        TicTacToeGame game = new TicTacToeGame();
        game.Start();
    }
}

class TicTacToeGame
{
    private Board board;
    private Player currentPlayer;
    private int moves;

    public TicTacToeGame()
    {
        board = new Board();
        currentPlayer = Player.Player1;
        moves = 0;
    }

    public void Start()
    {
        Console.Clear();
        Console.WriteLine("===== Tic-Tac-Toe =====");
        Console.WriteLine("Player 1 (X)  |  Player 2 (O)");

        while (moves < 9 && !board.HasWinner())
        {
            board.Print();
            Console.WriteLine($"\n{currentPlayer}'s turn. Enter your move (1-9):");

            int choice = InputHandler.GetValidMove(board);
            board.Update(choice, currentPlayer);
            moves++;

            if (board.HasWinner())
            {
                Console.Clear();
                board.Print();
                Console.WriteLine($"\n🎉 {currentPlayer} wins!");
                return;
            }

            currentPlayer = (currentPlayer == Player.Player1) ? Player.Player2 : Player.Player1;
        }

        Console.Clear();
        board.Print();
        Console.WriteLine("\n🤝 It's a tie!");
    }
}

class Board
{
    private char[,] grid;

    public Board()
    {
        grid = new char[,]
        {
            { '1', '2', '3' },
            { '4', '5', '6' },
            { '7', '8', '9' }
        };
    }

    public void Print()
    {
        Console.WriteLine("\nCurrent Board:");
        Console.WriteLine("------------------");
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($" {grid[i, 0]} | {grid[i, 1]} | {grid[i, 2]} ");
            if (i < 2)
            {
                Console.WriteLine("___|___|___");
            }
        }
    }

    public bool HasWinner()
    {
        // Check rows and columns
        for (int i = 0; i < 3; i++)
        {
            if ((grid[i, 0] == grid[i, 1] && grid[i, 1] == grid[i, 2]) ||
                (grid[0, i] == grid[1, i] && grid[1, i] == grid[2, i]))
            {
                return true;
            }
        }

        // Check diagonals
        if ((grid[0, 0] == grid[1, 1] && grid[1, 1] == grid[2, 2]) ||
            (grid[0, 2] == grid[1, 1] && grid[1, 1] == grid[2, 0]))
        {
            return true;
        }

        return false;
    }

    public void Update(int choice, Player player)
    {
        int row = (choice - 1) / 3;
        int col = (choice - 1) % 3;
        grid[row, col] = (player == Player.Player1) ? 'X' : 'O';
    }

    public bool IsCellAvailable(int choice)
    {
        int row = (choice - 1) / 3;
        int col = (choice - 1) % 3;
        return char.IsDigit(grid[row, col]);
    }
}

class InputHandler
{
    public static int GetValidMove(Board board)
    {
        while (true)
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= 9)
            {
                if (board.IsCellAvailable(choice))
                {
                    return choice;
                }
                Console.WriteLine("⚠ Space already taken. Try again:");
            }
            else
            {
                Console.WriteLine("❌ Invalid input. Enter a number between 1-9:");
            }
        }
    }
}

enum Player
{
    Player1,
    Player2
}
