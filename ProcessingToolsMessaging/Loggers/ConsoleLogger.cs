namespace ProcessingTools.Globals
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

        public void LogError(object message)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("{0}: Error", Diagnostics.GetCurrentMethod(2));
                Console.WriteLine(message);
                Console.ResetColor();
            }
            catch (IOException)
            {
            }
            catch
            {
                throw;
            }
        }

        public void LogError(Exception e, object message)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("{0}: Error: {1}: {2}", Diagnostics.GetCurrentMethod(2), e.GetType(), e.Message);
                Console.WriteLine(message);
                Console.ResetColor();
            }
            catch (IOException)
            {
            }
            catch
            {
                throw;
            }
        }

        public void LogError(string format, params object[] args)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("{0}: Error", Diagnostics.GetCurrentMethod(2));
                Console.WriteLine(format, args);
                Console.ResetColor();
            }
            catch (IOException)
            {
            }
            catch
            {
                throw;
            }
        }

        public void LogError(Exception e, string format, params object[] args)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("{0}: Error: {1}: {2}", Diagnostics.GetCurrentMethod(2), e.GetType(), e.Message);
                Console.WriteLine(format, args);
                Console.ResetColor();
            }
            catch (IOException)
            {
            }
            catch
            {
                throw;
            }
        }

        public void LogException(Exception e, object message)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0}: Exception: {1}: {2}", Diagnostics.GetCurrentMethod(2), e.GetType(), e.Message);
                Console.WriteLine(message);
                Console.ResetColor();
            }
            catch (IOException)
            {
            }
            catch
            {
                throw;
            }
        }

        public void LogException(Exception e, string format, params object[] args)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0}: Exception: {1}: {2}", Diagnostics.GetCurrentMethod(2), e.GetType(), e.Message);
                Console.WriteLine(format, args);
                Console.ResetColor();
            }
            catch (IOException)
            {
            }
            catch
            {
                throw;
            }
        }

        public void LogInfo(object message)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(message);
                Console.ResetColor();
            }
            catch (IOException)
            {
            }
            catch
            {
                throw;
            }
        }

        public void LogInfo(Exception e, object message)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("{0}: Info: {1}: {2}", Diagnostics.GetCurrentMethod(2), e.GetType(), e.Message);
                Console.WriteLine(message);
                Console.ResetColor();
            }
            catch (IOException)
            {
            }
            catch
            {
                throw;
            }
        }

        public void LogInfo(string format, params object[] args)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(format, args);
                Console.ResetColor();
            }
            catch (IOException)
            {
            }
            catch
            {
                throw;
            }
        }

        public void LogInfo(Exception e, string format, params object[] args)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("{0}: Info: {1}: {2}", Diagnostics.GetCurrentMethod(2), e.GetType(), e.Message);
                Console.WriteLine(format, args);
                Console.ResetColor();
            }
            catch (IOException)
            {
            }
            catch
            {
                throw;
            }
        }

        public void LogWarning(object message)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("{0}: Warning", Diagnostics.GetCurrentMethod(2));
                Console.WriteLine(message);
                Console.ResetColor();
            }
            catch (IOException)
            {
            }
            catch
            {
                throw;
            }
        }

        public void LogWarning(Exception e, object message)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("{0}: Warning: {1}: {2}", Diagnostics.GetCurrentMethod(2), e.GetType(), e.Message);
                Console.WriteLine(message);
                Console.ResetColor();
            }
            catch (IOException)
            {
            }
            catch
            {
                throw;
            }
        }

        public void LogWarning(string format, params object[] args)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("{0}: Warning", Diagnostics.GetCurrentMethod(2));
                Console.WriteLine(format, args);
                Console.ResetColor();
            }
            catch (IOException)
            {
            }
            catch
            {
                throw;
            }
        }

        public void LogWarning(Exception e, string format, params object[] args)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("{0}: Warning: {1}: {2}", Diagnostics.GetCurrentMethod(2), e.GetType(), e.Message);
                Console.WriteLine(format, args);
                Console.ResetColor();
            }
            catch (IOException)
            {
            }
            catch
            {
                throw;
            }
        }
    }
}
