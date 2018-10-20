namespace TicTacToe.PostgreSqlData.Repositories
{
    using System.Collections.Generic;
    using TicTacToe.Core.Logger;
    using TicTacToe.Data;
    using TicTacToe.Data.Context;
    using TicTacToe.Models;
    using TicTacToe.PostgreSqlData.Utilities.Query;

    public class TicTacToeGameRepository : Repository, ITicTacToeGameRepository
    {
        public TicTacToeGameRepository(ILogger logger, IContext context)
       : base(logger, context)
        {
        }

        public IEnumerable<Nomenclature> GetGameLetters()
        {
            return Context.Connection.Query<Nomenclature>("tictactoe_game.get_ntables", new { tablename = "nletters" });
        }

        public IEnumerable<Nomenclature> GetGameLevels()
        {
            return Context.Connection.Query<Nomenclature>("tictactoe_game.get_ntables", new { tablename = "nlevels" });
        }
    }
}
