namespace TicTacToe.PostgreSqlData.Utilities.Query
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using TicTacToe.PostgreSqlData.Utilities.Reader;

    public static class TextQueryExtension
    {
        public static IEnumerable<T> QueryText<T>(this DbConnection cnn, string sql)
        {
            using (var command = cnn.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                return command.Read<T>();
            }
        }
    }
}
