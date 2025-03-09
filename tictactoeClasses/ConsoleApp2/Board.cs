using System;
namespace GameState;
public class Board
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
        Console.WriteLine("Player 1 is X, Player 2 is O");
        Console.WriteLine("This is the board...");
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

    public bool CheckWinner()
    {
        for (int i = 0; i < 3; i++)
        {
            if ((grid[i, 0] == grid[i, 1] && grid[i, 1] == grid[i, 2]) ||
                (grid[0, i] == grid[1, i] && grid[1, i] == grid[2, i]))
            {
                return true;
            }
        }

        if ((grid[0, 0] == grid[1, 1] && grid[1, 1] == grid[2, 2]) ||
            (grid[0, 2] == grid[1, 1] && grid[1, 1] == grid[2, 0]))
        {
            return true;
        }

        return false;
    }

    public void Update(int choice, PLAYER player)
    {
        int row = (choice - 1) / 3;
        int col = (choice - 1) % 3;
        grid[row, col] = (player == PLAYER.PLAYER1) ? 'X' : 'O';
    }

    public bool IsCellAvailable(int choice)
    {
        int row = (choice - 1) / 3;
        int col = (choice - 1) % 3;
        return char.IsDigit(grid[row, col]);
    }
}
