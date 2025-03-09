using System;

namespace HangmanNamespace
{
    public class WordBank
    {
        private string[] words = { "developer", "hangman", "computer", "software", "programming", "keyboard", "interface" };
        private Random rand = new Random();

        public string GetRandomWord()
        {
            return words[rand.Next(words.Length)];
        }
    }
}
