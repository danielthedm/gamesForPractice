using System;

namespace SnakeGameNamespace
{
    public class Food
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

        public (int, int) Position => position; // Read-only

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
}
