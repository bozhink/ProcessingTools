namespace ProcessingTools
{
    public static class ILoggerExtensions
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
