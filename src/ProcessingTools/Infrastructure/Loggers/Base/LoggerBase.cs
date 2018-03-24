namespace ProcessingTools.Loggers.Base
{
    using System;
    using System.Text;
    using Diagnostics;
    using ProcessingTools.Contracts;
    using ProcessingTools.Enumerations;

    public abstract class LoggerBase : ILogger
    {
        public abstract void Log();

        public abstract void Log(object message);

        public abstract void Log(LogType type, object message);

        public virtual void Log(string format, params object[] args) => this.Log(message: string.Format(format, args));

        public virtual void Log(LogType type, string format, params object[] args) => this.Log(type: type, message: string.Format(format, args));

        public virtual void Log(Exception exception, object message) => this.Log(type: LogType.Exception, exception: exception, message: message);

        public virtual void Log(Exception exception, string format, params object[] args) => this.Log(type: LogType.Exception, exception: exception, message: string.Format(format, args));

        public virtual void Log(LogType type, Exception exception, string format, params object[] args) => this.Log(type: type, exception: exception, message: string.Format(format, args));

        public virtual void Log(LogType type, Exception exception, object message)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(
                "{0}: {1}: {2}: {3}",
                Tracer.GetCurrentMethod(2),
                type.ToString(),
                exception.GetType(),
                exception.Message);
            sb.Append(message);
            sb.Append(exception.ToString());

            this.Log(type: type, message: sb.ToString());
        }
    }
}
