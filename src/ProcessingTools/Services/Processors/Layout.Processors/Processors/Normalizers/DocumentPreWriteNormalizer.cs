namespace ProcessingTools.Layout.Processors.Processors.Normalizers
{
    using System;
    using System.Threading.Tasks;
    using Contracts.Formatters;
    using Contracts.Normalizers;
    using ProcessingTools.Contracts;

    public class DocumentPreWriteNormalizer : IDocumentPreWriteNormalizer
    {
        private readonly IDocumentSchemaNormalizer documentNormalizer;
        private readonly IDocumentFinalFormatter documentFinalFormatter;

        public DocumentPreWriteNormalizer(IDocumentSchemaNormalizer documentNormalizer, IDocumentFinalFormatter documentFinalFormatter)
        {
            if (documentNormalizer == null)
            {
                throw new ArgumentNullException(nameof(documentNormalizer));
            }

            if (documentFinalFormatter == null)
            {
                throw new ArgumentNullException(nameof(documentFinalFormatter));
            }

            this.documentNormalizer = documentNormalizer;
            this.documentFinalFormatter = documentFinalFormatter;
        }

        public async Task<object> Normalize(IDocument document)
        {
            if (document == null)
            {
                return false;
            }

            // Due to some XSL characteristics, double normalization is better than a single one.
            var result = await this.documentNormalizer.NormalizeToDocumentSchema(document)
                .ContinueWith(
                    _ =>
                    {
                        _.Wait();
                        return this.documentNormalizer.NormalizeToDocumentSchema(document);
                    })
                .ContinueWith(
                    _ =>
                    {
                        _.Wait();
                        return this.documentFinalFormatter.Format(document).Result;
                    })
                .ConfigureAwait(false);

            return result;
        }
    }
}
