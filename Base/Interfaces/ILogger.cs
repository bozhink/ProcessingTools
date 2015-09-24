namespace ProcessingTools.BaseLibrary
{
    public interface ILogger
    {
        void Log(string format, params object[] args);
    }
}
