namespace ProcessingTools.Tagger.Commands.Generics
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Commands;
    using ProcessingTools.Contracts;
    using ProcessingTools.Layout.Processors.Contracts.Normalizers;
    using ProcessingTools.Processors.Contracts;

    public class GenericDocumentTaggerWithNormalizationCommand<TTagger> : ITaggerCommand
        where TTagger : class, IDocumentTagger
    {
        private readonly TTagger tagger;
        private readonly IDocumentSchemaNormalizer documentNormalizer;

        public GenericDocumentTaggerWithNormalizationCommand(TTagger tagger, IDocumentSchemaNormalizer documentNormalizer)
        {
            this.tagger = tagger ?? throw new ArgumentNullException(nameof(tagger));
            this.documentNormalizer = documentNormalizer ?? throw new ArgumentNullException(nameof(documentNormalizer));
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

            var result = await this.tagger.TagAsync(document).ConfigureAwait(false);
            await this.documentNormalizer.NormalizeToSystem(document).ConfigureAwait(false);

            return result;
        }
    }
}
