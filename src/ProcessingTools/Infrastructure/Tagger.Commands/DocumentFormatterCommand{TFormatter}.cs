namespace ProcessingTools.Tagger.Commands
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Commands;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors;

    public class DocumentFormatterCommand<TFormatter> : ITaggerCommand
        where TFormatter : class, IDocumentFormatter
    {
        private readonly TFormatter formatter;

        public DocumentFormatterCommand(TFormatter formatter)
        {
            this.formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
        }

        public Task<object> RunAsync(IDocument document, ICommandSettings settings)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            return this.formatter.FormatAsync(document);
        }
    }
}
