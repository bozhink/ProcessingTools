namespace ProcessingTools.Tagger
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

    public abstract class FileProcessor
    {
        public abstract Task Run();

        protected abstract Task InvokeProcessor(string message, Action action);
    }
}