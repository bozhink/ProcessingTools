using System;

namespace Base
{
    public static class Alert
    {
        public static void Message()
        {
            try
            {
                Console.WriteLine();
            }
            catch (System.IO.IOException)
            {
            }
        }

        public static void Message(string message)
        {
            try
            {
                Console.WriteLine(message);
            }
            catch (System.IO.IOException)
            {
            }
        }

        public static void Message(int message)
        {
            try
            {
                Console.WriteLine(message);
            }
            catch (System.IO.IOException)
            {
            }
        }

        public static void Message(double message)
        {
            try
            {
                Console.WriteLine(message);
            }
            catch (System.IO.IOException)
            {
            }
        }

        public static void Message(Object message)
        {
            try
            {
                Console.WriteLine(message);
            }
            catch (System.IO.IOException)
            {
            }
        }

        public static void Message(string message, bool indent)
        {
            try
            {
                Alert.Message();
                if (indent)
                {
                    Alert.Message("\t" + message);
                }
                else
                {
                    Alert.Message(message);
                }

                Alert.Message();
            }
            catch (System.IO.IOException)
            {
            }
        }

        public static void Message(int message, bool indent)
        {
            try
            {
                Alert.Message();
                if (indent)
                {
                    Alert.Message("\t" + message);
                }
                else
                {
                    Alert.Message(message);
                }

                Alert.Message();
            }
            catch (System.IO.IOException)
            {
            }
        }

        public static void Message(double message, bool indent)
        {
            try
            {
                Alert.Message();
                if (indent)
                {
                    Alert.Message("\t" + message);
                }
                else
                {
                    Alert.Message(message);
                }

                Alert.Message();
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
            Console.WriteLine("{0}(): ERROR: {1}: {2}",
                Diagnostics.GetCurrentMethod(stackPosition),
                e.GetType(), e.Message);
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
            Console.WriteLine("{0}.{1}(): ERROR: {2}: {3}",
                callerType, Diagnostics.GetCurrentMethod(stackPosition),
                e.GetType(), e.Message);
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
            Console.WriteLine("{0}.{1}(): ERROR: {2}: {3}\nMESSAGE: {4}",
                callerType, Diagnostics.GetCurrentMethod(stackPosition),
                e.GetType(), e.Message, customMessage);
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
            Alert.Message(Messages.writeOutputFileMessage, true);
        }

        public static void ZoobankCloneMessage()
        {
            Alert.Message(Messages.zoobankCloneMessage, true);
        }

        public static void PrintHelp()
        {
            Alert.Message(Messages.helpMessage);
        }
    }
}
