using System;
using System.Threading;

namespace SnakeGameNamespace
{
    public class SnakeGame
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
            Console.CursorVisible = false;
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
                Thread.Sleep(100);
            }

            Console.SetCursorPosition(0, screenHeight + 2);
            Console.WriteLine("Game Over! Press any key to exit...");
            Console.ReadKey();
        }
    }
}
