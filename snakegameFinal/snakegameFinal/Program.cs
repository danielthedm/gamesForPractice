using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main()
    {
        SnakeGame game = new SnakeGame();
        game.Run();
    }
}

class SnakeGame
{
    private Snake snake;
    private Food food;
    private GameBoard board;
    private InputHandler inputHandler;
    private bool gameOver;
    private int screenWidth = 30;
    private int screenHeight = 15;

    public SnakeGame()
    {
        snake = new Snake(screenWidth, screenHeight);
        food = new Food(screenWidth, screenHeight, snake);
        board = new GameBoard(screenWidth, screenHeight, snake, food);
        inputHandler = new InputHandler();
        gameOver = false;
    }

    public void Run()
    {
        while (!gameOver)
        {
            board.Draw();
            inputHandler.ProcessInput(snake);
            gameOver = snake.Move(food);
            Thread.Sleep(100); // Controls game speed
        }

        Console.WriteLine("Game Over! Press any key to exit...");
        Console.ReadKey();
    }
}

class Snake
{
    private List<(int, int)> body;
    private string direction;
    private int screenWidth;
    private int screenHeight;

    public Snake(int screenWidth, int screenHeight)
    {
        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;
        body = new List<(int, int)> { (5, 5), (5,6) };
        direction = "RIGHT";
    }

    public List<(int, int)> Body => body;
    public string Direction { get => direction; set => direction = value; }

    public bool Move(Food food)
    {
        var head = body[0];
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
            body.Contains(newHead))
        {
            return true; // Game Over
        }

        body.Insert(0, newHead); // Move snake head

        // Check if snake eats food
        if (newHead == food.Position)
        {
            food.Spawn();
        }
        else
        {
            body.RemoveAt(body.Count - 1); // Remove tail if no food eaten
        }

        return false; // Game continues
    }
}

class Food
{
    private int screenWidth;
    private int screenHeight;
    private Random rand;
    private (int, int) position;
    private Snake snake;

    public Food(int screenWidth, int screenHeight, Snake snake)
    {
        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;
        this.snake = snake;
        rand = new Random();
        Spawn();
    }

    public (int, int) Position => position;

    public void Spawn()
    {
        int x, y;
        do
        {
            x = rand.Next(1, screenWidth - 2);
            y = rand.Next(1, screenHeight - 2);
        } while (snake.Body.Contains((x, y)));

        position = (x, y);
    }
}

class GameBoard
{
    private int screenWidth;
    private int screenHeight;
    private Snake snake;
    private Food food;

    public GameBoard(int screenWidth, int screenHeight, Snake snake, Food food)
    {
        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;
        this.snake = snake;
        this.food = food;
    }

    public void Draw()
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
                else if (snake.Body.Contains((x, y))) Console.Write("O"); // Snake body
                else if (food.Position == (x, y)) Console.Write("*"); // Food
                else Console.Write(" ");
            }
            Console.WriteLine();
        }

        // Draw bottom wall
        for (int x = 0; x < screenWidth; x++) Console.Write("#");
        Console.WriteLine();
    }
}

class InputHandler
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
