namespace ProcessingTools.Loggers.Loggers
{
    using System;
    using System.IO;
    using Base;
    using Contracts;
    using Diagnostics;
    using ProcessingTools.Enumerations;

    public class TextWriterLogger : LoggerBase, ITextWriterLogger
    {
        private readonly TextWriter textWriter;

        public TextWriterLogger(TextWriter textWriter)
        {
            this.textWriter = textWriter ?? throw new ArgumentNullException(nameof(textWriter));
        }

        public override void Log()
        {
            try
            {
                this.textWriter.WriteLine();
            }
            catch (IOException)
            {
                // Skip
            }
            catch
            {
                throw;
            }
        }

        public override void Log(object message)
        {
            try
            {
                this.textWriter.WriteLine(message);
            }
            catch (IOException)
            {
                // Skip
            }
            catch
            {
                throw;
            }
        }

        public override void Log(LogType type, object message)
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
                // Skip
            }
            catch
            {
                throw;
            }
        }

        public override void Log(LogType type, string format, params object[] args)
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
                // Skip
            }
            catch
            {
                throw;
            }
        }

        protected virtual void ResetLogTypeColor()
        {
        }

        protected virtual void SetLogTypeColor(LogType type)
        {
        }
    }
}
