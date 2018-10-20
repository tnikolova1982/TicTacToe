namespace TicTacToe.PostgreSqlData.Repositories
{
    using TicTacToe.Core.Logger;
    using TicTacToe.Data.Context;

    public class Repository
    {
        protected readonly Context.Context Context;
        protected readonly ILogger Logger;

        protected Repository(ILogger logger, IContext context)
        {
            this.Context = context as Context.Context;
            this.Logger = logger;
        }
    }
}
