namespace ProcessingTools.Contracts
{
    using System;
    using ProcessingTools.Enumerations;

    public interface ILogger
    {
        void Log();

        void Log(object message);

        void Log(string format, params object[] args);

        void Log(Exception exception, object message);

        void Log(Exception exception, string format, params object[] args);

        void Log(LogType type, object message);

        void Log(LogType type, string format, params object[] args);

        void Log(LogType type, Exception exception, object message);

        void Log(LogType type, Exception exception, string format, params object[] args);
    }
}
