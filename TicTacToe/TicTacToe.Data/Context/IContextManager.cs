namespace TicTacToe.Data.Context
{
    public interface IContextManager
    {
        ITransaction NewTransaction();
    }
}
