namespace ProcessingTools.Tagger.Core.Helpers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Helpers;
    using ProcessingTools.Contracts;
    using ProcessingTools.Layout.Processors.Contracts.Normalizers;
    using ProcessingTools.Processors.Contracts.Processors.Documents;
    using ProcessingTools.Tagger.Commands.Contracts;

    public class ReadDocumentHelper : IReadDocumentHelper
    {
        private readonly IDocumentFactory documentFactory;
        private readonly IDocumentMerger documentMerger;
        private readonly IDocumentReader documentReader;
        private readonly IDocumentNormalizer documentNormalizer;

        public ReadDocumentHelper(
            IDocumentFactory documentFactory,
            IDocumentMerger documentMerger,
            IDocumentReader documentReader,
            IDocumentNormalizer documentNormalizer)
        {
            if (documentFactory == null)
            {
                throw new ArgumentNullException(nameof(documentFactory));
            }

            if (documentMerger == null)
            {
                throw new ArgumentNullException(nameof(documentMerger));
            }

            if (documentReader == null)
            {
                throw new ArgumentNullException(nameof(documentReader));
            }

            if (documentNormalizer == null)
            {
                throw new ArgumentNullException(nameof(documentNormalizer));
            }

            this.documentFactory = documentFactory;
            this.documentMerger = documentMerger;
            this.documentReader = documentReader;
            this.documentNormalizer = documentNormalizer;
        }

        public async Task<IDocument> Read(IProgramSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            IDocument document;

            if (settings.MergeInputFiles)
            {
                document = await this.documentMerger.Merge(settings.FileNames.ToArray());
            }
            else
            {
                document = await this.documentReader.ReadDocument(settings.FileNames[0]);
            }

            settings.ArticleSchemaType = document.SchemaType;
            document.SchemaType = settings.ArticleSchemaType;

            await this.documentNormalizer.NormalizeToSystem(document);

            return document;
        }
    }
}
