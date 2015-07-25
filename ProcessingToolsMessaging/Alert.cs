using System;

namespace ProcessingTools
{
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

        public static void Log(string message, bool indent)
        {
            try
            {
                Alert.Log();
                if (indent)
                {
                    Alert.Log("\t" + message);
                }
                else
                {
                    Alert.Log(message);
                }

                Alert.Log();
            }
            catch (System.IO.IOException)
            {
            }
        }

        public static void Log(int message, bool indent)
        {
            try
            {
                Alert.Log();
                if (indent)
                {
                    Alert.Log("\t" + message);
                }
                else
                {
                    Alert.Log(message);
                }

                Alert.Log();
            }
            catch (System.IO.IOException)
            {
            }
        }

        public static void Log(double message, bool indent)
        {
            try
            {
                Alert.Log();
                if (indent)
                {
                    Alert.Log("\t" + message);
                }
                else
                {
                    Alert.Log(message);
                }

                Alert.Log();
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
            Alert.Log(Messages.writeOutputFileMessage, true);
        }

        public static void ZoobankCloneMessage()
        {
            Alert.Log(Messages.zoobankCloneMessage, true);
        }

        public static void PrintHelp()
        {
            Alert.Log(Messages.helpMessage);
        }
    }
}
