namespace TicTacToe.PostgreSqlData.Utilities.Query
{
    using System.Collections.Generic;
    using System.Data.Common;
    using TicTacToe.PostgreSqlData.Utilities.DynamicCommand;

    public static class CommandQueryExtension
    {
        public static IEnumerable<T> Query<T>(this DbConnection cnn, string commandText, object obj = null)
        {
            var generator = new CommandGenerator();
            var query = new CommandQuery();

            return query.Query<T, ProcedureParameters>(cnn, generator, commandText, obj);
        }
    }
}
