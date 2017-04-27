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
        private TextWriter textWriter;

        public TextWriterLogger(TextWriter textWriter)
        {
            if (textWriter == null)
            {
                throw new ArgumentNullException(nameof(textWriter));
            }

            this.textWriter = textWriter;
        }

        public override void Log()
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

        public override void Log(object message)
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
