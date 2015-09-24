namespace ProcessingTools.BaseLibrary
{
    public interface ILogger
    {
        void Log();

        void Log(object message);

        void Log(string format, params object[] args);
    }
}
