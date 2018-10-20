namespace TicTacToe.Models.Game
{
    using System;

    public class GameStatus
    {
        public static Guid Play => new Guid("a6fea3cd-9d85-4e4d-9e23-ee433d45477d");

        public static Guid Win => new Guid("6491c960-1b6d-4e55-9a2b-e3862361e78b");

        public static Guid Lose => new Guid("b7a12197-4ef2-4e66-902b-fc31007133c9");

        public static Guid Draw => new Guid("0da5b350-3000-4dd8-b346-1488912f33ab");
    }
}
