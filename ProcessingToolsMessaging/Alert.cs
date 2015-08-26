namespace ProcessingTools
{
    using System;
    using ProcessingToolsMessaging.Messages;

    public static class Alert
    {
        public static void Log()
        {
            try
            {
                Console.WriteLine();
            }
            catch (System.IO.IOException)
            {
            }
        }

        public static void Log(string message)
        {
            try
            {
                Console.WriteLine(message);
            }
            catch (System.IO.IOException)
            {
            }
        }

        public static void Log(int message)
        {
            try
            {
                Console.WriteLine(message);
            }
            catch (System.IO.IOException)
            {
            }
        }

        public static void Log(double message)
        {
            try
            {
                Console.WriteLine(message);
            }
            catch (System.IO.IOException)
            {
            }
        }

        public static void Log(object message)
        {
            try
            {
                Console.WriteLine(message);
            }
            catch (System.IO.IOException)
            {
            }
        }

        public static void Log(string format, params object[] args)
        {
            try
            {
                Console.WriteLine(format, args);
            }
            catch (System.IO.IOException)
            {
            }
        }

        public static void Exit(int exitCode)
        {
            try
            {
                System.Environment.Exit(exitCode);
            }
            catch (System.Security.SecurityException)
            {
            }
        }

        public static void Die(int exitCode, string format, params object[] args)
        {
            Alert.Log(format, args);
            Alert.Exit(exitCode);
        }

        public static void RaiseExceptionForType(Exception e, string callerType, int exitCode)
        {
            Console.WriteLine("{0}: ERROR: {1}: {2}", callerType, e.GetType(), e.Message);
            if (exitCode == 0)
            {
                Console.WriteLine("This error will not break the program!");
            }
            else
            {
                Environment.Exit(exitCode);
            }
        }

        public static void RaiseExceptionForMethod(Exception e, int exitCode, int stackPosition = 2)
        {
            Console.WriteLine("{0}(): ERROR: {1}: {2}", Diagnostics.GetCurrentMethod(stackPosition), e.GetType(), e.Message);
            if (exitCode == 0)
            {
                Console.WriteLine("This error will not break the program!");
            }
            else
            {
                Environment.Exit(exitCode);
            }
        }

        public static void RaiseExceptionForMethod(Exception e, string callerType, int exitCode, int stackPosition = 2)
        {
            Console.WriteLine("{0}.{1}(): ERROR: {2}: {3}", callerType, Diagnostics.GetCurrentMethod(stackPosition), e.GetType(), e.Message);
            if (exitCode == 0)
            {
                Console.WriteLine("This error will not break the program!");
            }
            else
            {
                Environment.Exit(exitCode);
            }
        }

        public static void RaiseExceptionForMethod(Exception e, string callerType, int exitCode, string customMessage, int stackPosition = 2)
        {
            Console.WriteLine("{0}.{1}(): ERROR: {2}: {3}\nMESSAGE: {4}", callerType, Diagnostics.GetCurrentMethod(stackPosition), e.GetType(), e.Message, customMessage);
            if (exitCode == 0)
            {
                Console.WriteLine("This error will not break the program!");
            }
            else
            {
                Environment.Exit(exitCode);
            }
        }

        public static void WriteOutputFileMessage()
        {
            Alert.Log("\n\t{0}\n", Messages.writeOutputFileMessage);
        }

        public static void ZoobankCloneMessage()
        {
            Alert.Log("\n\t{0}\n", Messages.zoobankCloneMessage);
        }

        public static void PrintHelp()
        {
            Alert.Log(Messages.helpMessage);
        }
    }
}
