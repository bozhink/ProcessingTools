namespace ProcessingTools
{
    using System;
    using System.IO;

    public class ConsoleLogger : ILogger
    {
        public void Log()
        {
            try
            {
                Console.WriteLine();
            }
            catch (IOException)
            {
            }
        }

        public void Log(object message)
        {
            try
            {
                Console.WriteLine(message);
            }
            catch (IOException)
            {
            }
        }

        public void Log(string format, params object[] args)
        {
            try
            {
                Console.WriteLine(format, args);
            }
            catch (IOException)
            {
            }
        }
    }
}
