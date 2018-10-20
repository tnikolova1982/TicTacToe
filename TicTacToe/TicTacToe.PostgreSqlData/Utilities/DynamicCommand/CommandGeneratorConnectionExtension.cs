namespace TicTacToe.PostgreSqlData.Utilities.DynamicCommand
{
    using System.Data.Common;

    public static class CommandGeneratorConnectionExtension
    {
        public static DbCommand GenerateCommand(this DbConnection cnn, string commandText, object obj = null, params Parameters.DbParameter[] additionalParameters)
        {
            var generator = new CommandGenerator();
            return generator.GenerateCommand(cnn, commandText, obj, additionalParameters);
        }
    }
}
