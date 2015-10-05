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
            catch
            {
                throw;
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
            catch
            {
                throw;
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
            catch
            {
                throw;
            }
        }

        public void LogException(Exception e, object customMessage)
        {
            this.Log(
                "{0}: ERROR: {1}: {2}\nMESSAGE: {3}",
                Diagnostics.GetCurrentMethod(2),
                e.GetType(),
                customMessage,
                e.Message);
        }

        public void LogException(Exception e, string format, params object[] args)
        {
            this.Log(
                "{0}: ERROR: {1}: {2}\n",
                Diagnostics.GetCurrentMethod(2),
                e.GetType(),
                e.Message);
            this.Log(format, args);
        }
    }
}
