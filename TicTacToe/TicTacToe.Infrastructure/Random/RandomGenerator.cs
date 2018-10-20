namespace TicTacToe.Infrastructure.Random
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class RandomGenerator
    {
        private static readonly Random Random = new Random();

        public static string GetRandomString()
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            List<char> characters = new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '-', '_' };
            string randomString = string.Empty;

            // run the loop till I get a string of 6 characters
            for (int i = 0; i < 7; i++)
            {
                // Get random numbers, to get either a character or a number...
                int random = Random.Next(0, 3);
                if (random == 1)
                {
                    // use a number
                    random = Random.Next(0, numbers.Count);
                    randomString += numbers[random].ToString();
                }
                else
                {
                    random = Random.Next(0, characters.Count);
                    randomString += characters[random].ToString();
                }
            }

            return randomString;
        }

        public static string Generate(int length = 10, bool onlyAlpha = true)
        {
            return Generate(string.Empty, length, onlyAlpha);
        }

        public static string Generate(string extention, int length = 10, bool onlyAlpha = true)
        {
            var builder = new StringBuilder();

            string chars;

            if (onlyAlpha)
            {
                chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            }
            else
            {
                chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            }

            var result = new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[Random.Next(s.Length)])
                          .ToArray());

            builder.Append(result);

            if (!string.IsNullOrEmpty(extention))
            {
                builder.Append(extention);
            }

            return builder.ToString();
        }
    }
}
