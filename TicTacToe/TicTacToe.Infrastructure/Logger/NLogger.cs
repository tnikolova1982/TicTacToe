namespace TicTacToe.Infrastructure.Logger
{
    using System;
    using NLog;
    using TicTacToe.Infrastructure.Reflection;
    using ILogger = Core.Logger.ILogger;

    public class NLogger : ILogger
    {
        private readonly Logger logger;

        public NLogger()
        {
            logger = LogManager.GetLogger(ReflectionUtils.CallClassName);
        }

        public void Debug(string e)
        {
            logger.Error(e);
        }

        public void Error(Exception e)
        {
            logger.Error(e);
        }

        public void Error(string e)
        {
            logger.Error(e);
        }

        public void Info(string e)
        {
            logger.Info(e);
        }

        public void Trace(string e)
        {
            logger.Trace(e);
        }

        public void Warning(string e)
        {
            logger.Warn(e);
        }
    }
}
