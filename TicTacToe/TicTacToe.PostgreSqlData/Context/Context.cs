namespace TicTacToe.PostgreSqlData.Context
{
    using System.Data.Common;
    using TicTacToe.Data.Context;

    public class Context : IContext
    {
        public Context(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; private set; }

        public DbConnection Connection { get; set; }

        public DbTransaction Transaction { get; set; }
    }
}
