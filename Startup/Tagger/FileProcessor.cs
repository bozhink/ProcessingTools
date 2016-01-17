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
            await this.InvokeProcessor(message, async () => await validator.Validate());
        }

        protected async Task InvokeProcessor(string message, ITagger tagger)
        {
            await this.InvokeProcessor(message, async () => await tagger.Tag());
        }

        protected async Task InvokeProcessor(string message, IParser parser)
        {
            await this.InvokeProcessor(message, async () => await parser.Parse());
        }

        protected async Task InvokeProcessor(string message, IGenerator generator)
        {
            await this.InvokeProcessor(message, async () => await generator.Generate());
        }

        protected async Task InvokeProcessor(string message, IFormatter formatter)
        {
            await this.InvokeProcessor(message, async () => await formatter.Format());
        }

        protected async Task InvokeProcessor(string message, ICloner cloner)
        {
            await this.InvokeProcessor(message, async () => await cloner.Clone());
        }

        protected async Task InvokeProcessor(string message, IProcessor processor)
        {
            await this.InvokeProcessor(message, async () => await processor.Process());
        }
    }
}