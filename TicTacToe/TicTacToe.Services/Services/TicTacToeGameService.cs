namespace TicTacToe.Services.Services
{
    using System;
    using System.Collections.Generic;
    using TicTacToe.Data;
    using TicTacToe.Domain.Interfaces;
    using TicTacToe.Models;
    using TicTacToe.Models.Game;

    public class TicTacToeGameService : ITicTacToeGameService
    {
        private const int TicTacToeBoardSize = 9;

        private readonly ITicTacToeGameRepository ticTacToeGameRepository;
        private readonly ITicTacToeCommonServices ticTacToeCommonServices;

        public TicTacToeGameService(ITicTacToeGameRepository ticTacToeGameRepository, ITicTacToeCommonServices ticTacToeCommonServices)
        {
            this.ticTacToeGameRepository = ticTacToeGameRepository;
            this.ticTacToeCommonServices = ticTacToeCommonServices;
        }

        public IEnumerable<Nomenclature> GetGameLetters()
        {
            return ticTacToeGameRepository.GetGameLetters();
        }

        public IEnumerable<Nomenclature> GetGameLevels()
        {
            return ticTacToeGameRepository.GetGameLevels();
        }

        public Game InitializeTicTacToeGame()
        {
            Game game = new Game();

            game.Borad = GetTicTacToeBorad();
            game.GameStatus = new Nomenclature
            {
                Id = default(Guid)
            };

            return game;
        }

        public Game StartTicTacToeGame(Game game)
        {
            game.Borad = GetTitTacToeEnabledBoard();
            game.GameStatus = new Nomenclature
            {
                Id = default(Guid)
            };
            game.MovesCount = 0;

            return game;
        }

        private Board GetTitTacToeEnabledBoard()
        {
            var gameBoard = new List<Box>();

            for (int index = 0; index < TicTacToeBoardSize; index++)
            {
                gameBoard.Add(new Box { IsEnabled = true, Positon = index });
            }

            return new Board
            {
                GameBoard = gameBoard
            };
        }

        public Board CalculateWinner(Board board)
        {
            var lines = ticTacToeCommonServices.GetLines();

            for (int index = 0; index < lines.Count; index++)
            {
                var line = lines[index];

                if (board.GameBoard[line[0]].Value !=null && board.GameBoard[line[0]].Value == board.GameBoard[line[1]].Value && board.GameBoard[line[0]].Value == board.GameBoard[line[2]].Value)
                {
                    board.GameBoard[line[0]].IsWinBox = true;
                    board.GameBoard[line[1]].IsWinBox = true;
                    board.GameBoard[line[2]].IsWinBox = true;

                    return board;
                }
            }

            return null;
        }

        private Board GetTicTacToeBorad()
        {
            var gameBoard = new List<Box>();

            for (int index = 0; index < TicTacToeBoardSize; index++)
            {
                gameBoard.Add(new Box());
            }

            return new Board
            {
                GameBoard = gameBoard
            };
        }
    }
}
