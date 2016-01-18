namespace ProcessingTools.MainProgram
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

    public abstract class FileProcessor
    {
        public abstract Task Run();

        protected abstract Task InvokeProcessor(string message, Action action);

        protected async Task InvokeProcessor(string message, IValidator validator)
        {
            await this.InvokeProcessor(message, () => validator.Validate().Wait());
        }

        protected async Task InvokeProcessor(string message, ITagger tagger)
        {
            await this.InvokeProcessor(message, () => tagger.Tag().Wait());
        }

        protected async Task InvokeProcessor(string message, IParser parser)
        {
            await this.InvokeProcessor(message, () => parser.Parse().Wait());
        }

        protected async Task InvokeProcessor(string message, IGenerator generator)
        {
            await this.InvokeProcessor(message, () => generator.Generate().Wait());
        }

        protected async Task InvokeProcessor(string message, IFormatter formatter)
        {
            await this.InvokeProcessor(message, () => formatter.Format().Wait());
        }

        protected async Task InvokeProcessor(string message, ICloner cloner)
        {
            await this.InvokeProcessor(message, () => cloner.Clone().Wait());
        }

        protected async Task InvokeProcessor(string message, IProcessor processor)
        {
            await this.InvokeProcessor(message, () => processor.Process().Wait());
        }
    }
}