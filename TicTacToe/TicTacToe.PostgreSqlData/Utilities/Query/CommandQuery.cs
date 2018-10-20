namespace TicTacToe.PostgreSqlData.Utilities.Query
{
    using System.Collections.Generic;
    using System.Data.Common;
    using TicTacToe.PostgreSqlData.Utilities.DynamicCommand;
    using TicTacToe.PostgreSqlData.Utilities.DynamicCommand.Parameters;
    using TicTacToe.PostgreSqlData.Utilities.Reader;

    public class CommandQuery
    {
        public IEnumerable<T> Query<T, TProcedureParameters>(DbConnection cnn, AbstractCommandGenerator<TProcedureParameters> commandGenerator, string commandText, object obj = null)
           where TProcedureParameters : IProcedureParameters
        {
            using (var command = commandGenerator.GenerateCommand(cnn, commandText, obj))
            {
                return command.Read<T>();
            }
        }
    }
}
