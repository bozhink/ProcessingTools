namespace ProcessingTools
{
    using System;

    public interface ILogger
    {
        void Log();

        void Log(object message);

        void Log(string format, params object[] args);

        void LogException(Exception e, object customMessage);

        void LogException(Exception e, string format, params object[] args);
    }
}
