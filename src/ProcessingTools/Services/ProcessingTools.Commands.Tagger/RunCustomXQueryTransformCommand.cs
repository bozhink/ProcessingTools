namespace ProcessingTools.Commands.Tagger
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Commands.Models.Contracts;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts;

    [System.ComponentModel.Description("Custom XQuery transform.")]
    public class RunCustomXQueryTransformCommand : IRunCustomXQueryTransformCommand
    {
        private readonly IDocumentXQueryProcessor processor;

        public RunCustomXQueryTransformCommand(IDocumentXQueryProcessor processor)
        {
            this.processor = processor ?? throw new ArgumentNullException(nameof(processor));
        }

        public async Task<object> RunAsync(IDocument document, ICommandSettings settings)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            int numberOfFileNames = settings.FileNames.Count;
            if (numberOfFileNames < 3)
            {
                throw new InvalidOperationException("The name of the XQuey file should be set.");
            }

            this.processor.XQueryFileName = settings.FileNames[2];

            await this.processor.ProcessAsync(document);

            return true;
        }
    }
}
