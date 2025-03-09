using System;
using System.Collections.Generic;
using System.Threading;

class SnakeGame
{
    static int screenWidth = 30;
    static int screenHeight = 15;
    static bool gameOver = false;
    static List<(int, int)> snake = new List<(int, int)> { (5, 5) };
    static (int, int) food;
    static string direction = "RIGHT";
    static Random rand = new Random();

    static void Main()
    {
        Console.CursorVisible = false;
        SpawnFood();

        while (!gameOver)
        {
            DrawBoard();
            Input();
            Move();
            Thread.Sleep(100); // Controls game speed
        }

        Console.SetCursorPosition(0, screenHeight + 2);
        Console.WriteLine("Game Over! Press any key to exit...");
        Console.ReadKey();
    }

    static void DrawBoard()
    {
        Console.Clear();

        // Draw top and bottom walls
        for (int x = 0; x < screenWidth; x++) Console.Write("#");
        Console.WriteLine();

        for (int y = 1; y < screenHeight - 1; y++)
        {
            for (int x = 0; x < screenWidth; x++)
            {
                if (x == 0 || x == screenWidth - 1) Console.Write("#"); // Side walls
                else if (snake.Contains((x, y))) Console.Write("O");   // Snake body
                else if (food == (x, y)) Console.Write("*");           // Food
                else Console.Write(" ");
            }
            Console.WriteLine();
        }

        // Draw bottom wall
        for (int x = 0; x < screenWidth; x++) Console.Write("#");
        Console.WriteLine();
    }

    static void Input()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.UpArrow && direction != "DOWN") direction = "UP";
            if (key == ConsoleKey.DownArrow && direction != "UP") direction = "DOWN";
            if (key == ConsoleKey.LeftArrow && direction != "RIGHT") direction = "LEFT";
            if (key == ConsoleKey.RightArrow && direction != "LEFT") direction = "RIGHT";
        }
    }

    static void Move()
    {
        var head = snake[0];
        (int, int) newHead = direction switch
        {
            "UP" => (head.Item1, head.Item2 - 1),
            "DOWN" => (head.Item1, head.Item2 + 1),
            "LEFT" => (head.Item1 - 1, head.Item2),
            "RIGHT" => (head.Item1 + 1, head.Item2),
            _ => head
        };

        // Check for collision with walls or itself
        if (newHead.Item1 == 0 || newHead.Item1 == screenWidth - 1 ||
            newHead.Item2 == 0 || newHead.Item2 == screenHeight - 1 ||
            snake.Contains(newHead))
        {
            gameOver = true;
            return;
        }

        snake.Insert(0, newHead); // Move snake head

        // Check if snake eats food
        if (newHead == food)
        {
            SpawnFood();
        }
        else
        {
            snake.RemoveAt(snake.Count - 1); // Remove tail if no food eaten
        }
    }

    static void SpawnFood()
    {
        int x, y;
        do
        {
            x = rand.Next(1, screenWidth - 2);
            y = rand.Next(1, screenHeight - 2);
        } while (snake.Contains((x, y)));

        food = (x, y);
    }
}