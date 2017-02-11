namespace ProcessingTools.Tagger.Commands.Commands
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Commands;
    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Processors;

    [Description("Custom XSL transform.")]
    public class RunCustomXslTransformCommand : IRunCustomXslTransformCommand
    {
        private readonly IDocumentXslProcessor processor;

        public RunCustomXslTransformCommand(IDocumentXslProcessor processor)
        {
            if (processor == null)
            {
                throw new ArgumentNullException(nameof(processor));
            }

            this.processor = processor;
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
                throw new ApplicationException("The name of the XSLT file should be set.");
            }

            this.processor.XslFileFullName = settings.FileNames[2];

            await this.processor.Process(document);

            return true;
        }
    }
}
