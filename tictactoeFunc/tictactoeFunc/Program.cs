
using System;
using System.Data;
using System.Reflection;
using System.Runtime.InteropServices;
class TicTactoe
{
    static char[,] board =
    {
       { '1', '2', '3'},
       { '4', '5', '6'},
       { '7', '8', '9'}
    };

    static void Main()
    {
        int moves = 0;
        bool gameWon = false;
        PLAYER currentPlayer = PLAYER.PLAYER1;

        while (moves < 9 && !gameWon)
        {
            if (moves != 0)
            {
                currentPlayer = currentPlayer.Equals(PLAYER.PLAYER1) ? PLAYER.PLAYER2 : PLAYER.PLAYER1;
            }
            Console.Clear();
            PrintBoard();
            Console.WriteLine("Make user choice:");

            int choice = GetValidInput();

            UpdateBoard(choice, currentPlayer);
            gameWon = CheckForWinner(choice);

            moves += 1;
        }

        if (gameWon)
        {
            Console.WriteLine($"{currentPlayer.ToString()} has won");
        }
        else
        {
            Console.WriteLine("It is a tie!");
        }
    }

    static bool CheckForWinner(int choice)
    {
        //check rows and columns
        for (int i = 0; i < 3; i++)
        {
            if ((board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2]) ||
                (board[0, i] == board[1, i] && board[1, i] == board[2, i]))
            {
                return true;
            }
        }

        //check diagnals
        if ((board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2]) ||
            (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0]))
        {
            return true;
        }

        return false;
    }

    static void UpdateBoard(int choice, PLAYER player)
    {
        // Parse into a function
        int row = (choice - 1) / 3;
        int col = (choice - 1) % 3;
        board[row, col] = player.Equals(PLAYER.PLAYER1) ? char.Parse("X") : char.Parse("O");
    }

    static void PrintBoard()
    {
        Console.WriteLine($"Player 1 is X, Player 2 is O");
        Console.WriteLine("This is the board...");
        Console.WriteLine("------------------");
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($" {board[i, 0]} | {board[i, 1]} | {board[i, 2]} ");
            if (i < 2)
            {
                Console.WriteLine("___|___|____");
            }
        }
    }
    static int GetValidInput()
    {
        while (true)
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int choice))
            {
                int row = (choice - 1) / 3;
                int col = (choice - 1) % 3;
                if (char.IsDigit(board[row, col]))
                {
                    return choice;
                }
                else
                {
                    Console.WriteLine("Space is already taken, try again:");
                }
            }
            else
            {
                Console.WriteLine("Invalid input try again:");
            }
        }
    }

    enum PLAYER
    {
        PLAYER1,
        PLAYER2,
    }
}
