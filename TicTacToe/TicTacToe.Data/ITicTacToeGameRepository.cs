namespace TicTacToe.Data
{
    using System.Collections.Generic;
    using TicTacToe.Models;

    public interface ITicTacToeGameRepository
    {
        IEnumerable<Nomenclature> GetGameLetters();

        IEnumerable<Nomenclature> GetGameLevels();
    }
}
