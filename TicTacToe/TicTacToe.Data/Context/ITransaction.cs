namespace TicTacToe.Data.Context
{
    using System;

    public interface ITransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}
