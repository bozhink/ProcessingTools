namespace ProcessingTools.Infrastructure.Extensions
{
    using Contracts;

    public static class LoggerExtensions
    {
        public static void SendToLogger(this object objectToBeLogged, ILogger logger)
        {
            logger.Log(objectToBeLogged);
        }

        public static void SendToLogger(this object objectToBeLogged, ILogger logger, string format, params object[] args)
        {
            logger.Log(format, objectToBeLogged, args);
        }
    }
}
