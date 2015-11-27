namespace ProcessingTools.Contracts.Log
{
    using System;

    public interface ILogger
    {
        void Log();

        void Log(object message);

        void Log(string format, params object[] args);

        void Log(Exception e, object message);

        void Log(Exception e, string format, params object[] args);

        void Log(LogType type, object message);

        void Log(LogType type, string format, params object[] args);

        void Log(LogType type, Exception e, object message);

        void Log(LogType type, Exception e, string format, params object[] args);
    }
}
