namespace ProcessingTools.Layout.Processors.Processors.Normalizers
{
    using System;
    using System.Threading.Tasks;
    using Contracts.Normalizers;
    using ProcessingTools.Contracts;

    public class DocumentPostReadNormalizer : IDocumentPostReadNormalizer
    {
        private readonly IDocumentSchemaNormalizer documentNormalizer;

        public DocumentPostReadNormalizer(IDocumentSchemaNormalizer documentNormalizer)
        {
            if (documentNormalizer == null)
            {
                throw new ArgumentNullException(nameof(documentNormalizer));
            }

            this.documentNormalizer = documentNormalizer;
        }

        public async Task<object> Normalize(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var result = await this.documentNormalizer.NormalizeToSystem(document);

            return result;
        }
    }
}
