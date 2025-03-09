using System;

namespace SnakeGameNamespace
{
    public class GameBoard
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
            for (int x = 0; x < screenWidth; x++) Console.Write("#");
            Console.WriteLine();

            for (int y = 1; y < screenHeight - 1; y++)
            {
                for (int x = 0; x < screenWidth; x++)
                {
                    if (x == 0 || x == screenWidth - 1) Console.Write("#");
                    else if (snake.Body.Contains((x, y))) Console.Write("O");
                    else if (food.Position == (x, y)) Console.Write("*");
                    else Console.Write(" ");
                }
                Console.WriteLine();
            }

            for (int x = 0; x < screenWidth; x++) Console.Write("#");
            Console.WriteLine();
        }
    }
}
