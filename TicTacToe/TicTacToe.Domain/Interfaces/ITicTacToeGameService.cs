namespace TicTacToe.Domain.Interfaces
{
    using System.Collections.Generic;

    using TicTacToe.Models;
    using TicTacToe.Models.Game;

    public interface ITicTacToeGameService
    {
        Game InitializeTicTacToeGame();

        Game StartTicTacToeGame(Game game);

        IEnumerable<Nomenclature> GetGameLetters();

        IEnumerable<Nomenclature> GetGameLevels();

        Board CalculateWinner(Board board);

        // AI MOVE

        // CHECK FOR WIN

        // SAVE THE GAME FOR CURRENT USER AFTER LOGIN OR SESSION CLOSED

        // GET THE NEW BORAD AFTER USER INTERACTION

        // SET USER VALUE TO THE BOARD

        // SET AI VALUE TO THE BOARD
    }
}
