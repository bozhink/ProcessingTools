namespace ProcessingTools.MainProgram
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

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
            this.InvokeProcessor(message, () => validator.Validate().Wait());
        }

        protected void InvokeProcessor(string message, ITagger tagger)
        {
            this.InvokeProcessor(message, () => tagger.Tag().Wait());
        }

        protected void InvokeProcessor(string message, IParser parser)
        {
            this.InvokeProcessor(message, () => parser.Parse().Wait());
        }

        protected void InvokeProcessor(string message, IGenerator generator)
        {
            this.InvokeProcessor(message, () => generator.Generate().Wait());
        }

        protected void InvokeProcessor(string message, IFormatter formatter)
        {
            this.InvokeProcessor(message, () => formatter.Format().Wait());
        }

        protected void InvokeProcessor(string message, ICloner cloner)
        {
            this.InvokeProcessor(message, () => cloner.Clone().Wait());
        }

        protected void InvokeProcessor(string message, IProcessor processor)
        {
            this.InvokeProcessor(message, () => processor.Process().Wait());
        }
    }
}