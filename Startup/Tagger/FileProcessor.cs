namespace ProcessingTools.MainProgram
{
    using System;
    using System.Threading.Tasks;
    using Contracts;

    public abstract class FileProcessor
    {
        public abstract void Run();

        public Task RunAsync()
        {
            return Task.Run(() =>
            {
                this.Run();
            });
        }

        protected abstract void InvokeProcessor(string message, Action action);

        protected void InvokeProcessor(string message, IValidator validator)
        {
            this.InvokeProcessor(message, validator.Validate);
        }

        protected void InvokeProcessor(string message, ITagger tagger)
        {
            this.InvokeProcessor(message, tagger.Tag);
        }

        protected void InvokeProcessor(string message, IParser parser)
        {
            this.InvokeProcessor(message, parser.Parse);
        }

        protected void InvokeProcessor(string message, IGenerator generator)
        {
            this.InvokeProcessor(message, generator.Generate);
        }

        protected void InvokeProcessor(string message, IFormatter formatter)
        {
            this.InvokeProcessor(message, formatter.Format);
        }

        protected void InvokeProcessor(string message, ICloner cloner)
        {
            this.InvokeProcessor(message, cloner.Clone);
        }
    }
}