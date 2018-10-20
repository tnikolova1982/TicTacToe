namespace TicTacToe.Core.Logger
{
    using System;

    public interface ILogger
    {
        void Debug(string e);

        void Error(Exception e);

        void Error(string e);

        void Info(string e);

        void Trace(string e);

        void Warning(string e);
    }
}
