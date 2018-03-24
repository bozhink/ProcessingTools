namespace ProcessingTools.Tagger.Commands
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Commands;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Processors.Contracts;

    public class DocumentTaggerCommand<TTagger> : ITaggerCommand
        where TTagger : class, IDocumentTagger
    {
        private readonly TTagger tagger;

        public DocumentTaggerCommand(TTagger tagger)
        {
            this.tagger = tagger ?? throw new ArgumentNullException(nameof(tagger));
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

            return this.tagger.TagAsync(document);
        }
    }
}
