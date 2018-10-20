namespace TicTacToe.Models.Game
{
    using System;

    public class Game
    {
        public Board Borad { get; set; }

        public int UserWins { get; set; }

        public int ComputerWins { get; set; }

        public int MovesCount { get; set; }

        public Nomenclature GameStatus { get; set; }

        public Guid GameLevelId { get; set; }

        public Guid UserLetterId { get; set; }
    }
}
