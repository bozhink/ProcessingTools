namespace ProcessingTools.Loggers
{
    using System;
    using System.IO;

    using Contracts;
    using Diagnostics;

    using ProcessingTools.Contracts.Types;

    public class TextWriterLogger : ITextWriterLogger
    {
        private TextWriter textWriter;

        public TextWriterLogger()
            : this(Console.Out)
        {
        }

        public TextWriterLogger(TextWriter textWriter)
        {
            if (textWriter == null)
            {
                throw new ArgumentNullException(nameof(textWriter));
            }

            this.textWriter = textWriter;
        }

        public void Log()
        {
            try
            {
                this.textWriter.WriteLine();
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
                this.textWriter.WriteLine(message);
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
                this.textWriter.WriteLine(format, args);
            }
            catch (IOException)
            {
            }
            catch
            {
                throw;
            }
        }

        public void Log(LogType type, object message)
        {
            try
            {
                this.SetLogTypeColor(type);
                if (type != LogType.Info)
                {
                    this.textWriter.WriteLine("{0}: {1}", Tracer.GetCurrentMethod(2), type.ToString());
                }

                this.textWriter.WriteLine(message);
                this.ResetLogTypeColor();
            }
            catch (IOException)
            {
            }
            catch
            {
                throw;
            }
        }

        public void Log(Exception e, object message)
        {
            this.Log(LogType.Exception, e, message);
        }

        public void Log(LogType type, string format, params object[] args)
        {
            try
            {
                this.SetLogTypeColor(type);
                if (type != LogType.Info)
                {
                    this.textWriter.WriteLine("{0}: {1}", Tracer.GetCurrentMethod(2), type.ToString());
                }

                this.textWriter.WriteLine(format, args);
                this.ResetLogTypeColor();
            }
            catch (IOException)
            {
            }
            catch
            {
                throw;
            }
        }

        public void Log(LogType type, Exception e, object message)
        {
            try
            {
                this.SetLogTypeColor(type);
                this.textWriter.WriteLine("{0}: {1}: {2}: {3}", Tracer.GetCurrentMethod(2), type.ToString(), e.GetType(), e.Message);
                this.textWriter.WriteLine(message);
                this.textWriter.WriteLine(e.ToString());
                this.ResetLogTypeColor();
            }
            catch (IOException)
            {
            }
            catch
            {
                throw;
            }
        }

        public void Log(Exception e, string format, params object[] args)
        {
            this.Log(LogType.Exception, e, format, args);
        }

        public void Log(LogType type, Exception e, string format, params object[] args)
        {
            try
            {
                this.SetLogTypeColor(type);
                this.textWriter.WriteLine("{0}: {1}: {2}: {3}", Tracer.GetCurrentMethod(2), type.ToString(), e.GetType(), e.Message);
                this.textWriter.WriteLine(format, args);
                this.textWriter.WriteLine(e.ToString());
                this.ResetLogTypeColor();
            }
            catch (IOException)
            {
            }
            catch
            {
                throw;
            }
        }

        private void ResetLogTypeColor()
        {
            Console.ResetColor();
        }

        private void SetLogTypeColor(LogType type)
        {
            switch (type)
            {
                case LogType.Info:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;

                case LogType.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;

                case LogType.Exception:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                default:
                    break;
            }
        }
    }
}
