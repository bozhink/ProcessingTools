namespace ProcessingTools
{
    using System;
    using System.Security;

    public static class Alert
    {
        private static ILogger logger = new ConsoleLogger();

        public static void Exit(int exitCode)
        {
            try
            {
                Environment.Exit(exitCode);
            }
            catch (SecurityException)
            {
            }
        }

        public static void RaiseExceptionForType(Exception e, string callerType, int exitCode)
        {
            logger.Log("{0}: ERROR: {1}: {2}", callerType, e.GetType(), e.Message);
            if (exitCode == 0)
            {
                logger.Log("This error will not break the program!");
            }
            else
            {
                Alert.Exit(exitCode);
            }
        }

        public static void RaiseExceptionForMethod(Exception e, int exitCode, int stackPosition = 2)
        {
            logger.Log("{0}(): ERROR: {1}: {2}", Diagnostics.GetCurrentMethod(stackPosition), e.GetType(), e.Message);
            if (exitCode == 0)
            {
                logger.Log("This error will not break the program!");
            }
            else
            {
                Alert.Exit(exitCode);
            }
        }

        public static void RaiseExceptionForMethod(Exception e, string callerType, int exitCode, string customMessage, int stackPosition = 2)
        {
            logger.Log("{0}.{1}(): ERROR: {2}: {3}\nMESSAGE: {4}", callerType, Diagnostics.GetCurrentMethod(stackPosition), e.GetType(), e.Message, customMessage);
            if (exitCode == 0)
            {
                logger.Log("This error will not break the program!");
            }
            else
            {
                Alert.Exit(exitCode);
            }
        }
    }
}
