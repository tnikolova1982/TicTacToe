namespace TicTacToe.Infrastructure.Random
{
    using System;

    public class RandomProvider
    {
        private static Random instance;

        /// <summary>
        /// The instance of the random class
        /// </summary>
        public static Random Instance
        {
            get
            {
                return instance ?? new Random();
            }
        }
    }
}
