namespace ProcessingTools.Tagger.Commands.Commands
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Processors;
    using ProcessingTools.Tagger.Commands.Contracts;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;

    [Description("Custom XQuery transform.")]
    public class RunCustomXQueryTransformCommand : IRunCustomXQueryTransformCommand
    {
        private readonly IDocumentXQueryProcessor processor;

        public RunCustomXQueryTransformCommand(IDocumentXQueryProcessor processor)
        {
            this.processor = processor ?? throw new ArgumentNullException(nameof(processor));
        }

        public async Task<object> Run(IDocument document, ICommandSettings settings)
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

            this.processor.XQueryFileFullName = settings.FileNames[2];

            await this.processor.Process(document).ConfigureAwait(false);

            return true;
        }
    }
}
