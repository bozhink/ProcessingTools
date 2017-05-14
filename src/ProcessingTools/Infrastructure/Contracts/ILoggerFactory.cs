namespace ProcessingTools.Contracts
{
    using System;

    public interface ILoggerFactory
    {
        ILogger CreateLogger();

        ILogger CreateLogger(Type type);
    }
}