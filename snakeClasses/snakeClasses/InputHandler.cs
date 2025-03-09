using System;

namespace SnakeGameNamespace
{
    public class InputHandler
    {
        public void ProcessInput(Snake snake)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow && snake.Direction != "DOWN") snake.Direction = "UP";
                if (key == ConsoleKey.DownArrow && snake.Direction != "UP") snake.Direction = "DOWN";
                if (key == ConsoleKey.LeftArrow && snake.Direction != "RIGHT") snake.Direction = "LEFT";
                if (key == ConsoleKey.RightArrow && snake.Direction != "LEFT") snake.Direction = "RIGHT";
            }
        }
    }
}
