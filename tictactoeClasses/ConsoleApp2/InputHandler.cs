using System;
namespace GameState;

class InputHandler
{
    public static int GetValidInput(Board board)
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
                else
                {
                    Console.WriteLine("Space is already taken, try again:");
                }
            }
            else
            {
                Console.WriteLine("Invalid input, try again:");
            }
        }
    }
}
