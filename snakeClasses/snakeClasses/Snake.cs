using System;
using System.Collections.Generic;

namespace SnakeGameNamespace
{
    public class Snake
    {
        private List<(int, int)> body;
        private string direction;
        private int screenWidth;
        private int screenHeight;

        public Snake(int screenWidth, int screenHeight)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            body = new List<(int, int)> { (5, 5) };
            direction = "RIGHT";
        }

        public List<(int, int)> Body => body; // Read-only access
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

            // Collision Detection
            if (newHead.Item1 == 0 || newHead.Item1 == screenWidth - 1 ||
                newHead.Item2 == 0 || newHead.Item2 == screenHeight - 1 ||
                body.Contains(newHead))
            {
                return true;
            }

            body.Insert(0, newHead);

            if (newHead == food.Position)
            {
                food.Spawn();
            }
            else
            {
                body.RemoveAt(body.Count - 1);
            }

            return false;
        }
    }
}
