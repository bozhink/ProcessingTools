namespace ProcessingTools.Tagger.Commands
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Commands;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors;
    using ProcessingTools.Layout.Processors.Contracts.Normalizers;

    public class DocumentTaggerWithNormalizationCommand<TTagger> : ITaggerCommand
        where TTagger : class, IDocumentTagger
    {
        private readonly TTagger tagger;
        private readonly IDocumentSchemaNormalizer documentNormalizer;

        public DocumentTaggerWithNormalizationCommand(TTagger tagger, IDocumentSchemaNormalizer documentNormalizer)
        {
            this.tagger = tagger ?? throw new ArgumentNullException(nameof(tagger));
            this.documentNormalizer = documentNormalizer ?? throw new ArgumentNullException(nameof(documentNormalizer));
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

            var result = await this.tagger.TagAsync(document).ConfigureAwait(false);
            await this.documentNormalizer.NormalizeToSystem(document).ConfigureAwait(false);

            return result;
        }
    }
}
