namespace ProcessingTools.Globals
{
    using System;

    public interface ILogger
    {
        void Log();

        void Log(object message);

        void Log(string format, params object[] args);

        void LogException(Exception e, object message);

        void LogException(Exception e, string format, params object[] args);

        void LogInfo(object message);

        void LogInfo(string format, params object[] args);

        void LogInfo(Exception e, object message);

        void LogInfo(Exception e, string format, params object[] args);

        void LogWarning(object message);

        void LogWarning(string format, params object[] args);

        void LogWarning(Exception e, object message);

        void LogWarning(Exception e, string format, params object[] args);

        void LogError(object message);

        void LogError(string format, params object[] args);

        void LogError(Exception e, object message);

        void LogError(Exception e, string format, params object[] args);
    }
}
