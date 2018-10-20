namespace TicTacToe.PostgreSqlData.Context
{
    using TicTacToe.Data.Context;

    public class ContextManager : IContextManager
    {
        private readonly Context context;

        public ContextManager(IContext context)
        {
            this.context = context as Context;
        }

        public ITransaction NewTransaction()
        {
            var transaction = new Transaction(context);

            return transaction;
        }
    }
}
