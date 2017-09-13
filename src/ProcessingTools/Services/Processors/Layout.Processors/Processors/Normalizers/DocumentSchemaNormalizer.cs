namespace ProcessingTools.Layout.Processors.Processors.Normalizers
{
    using System;
    using System.Threading.Tasks;
    using Contracts.Factories;
    using Contracts.Normalizers;
    using ProcessingTools.Contracts;
    using ProcessingTools.Enumerations;

    public class DocumentSchemaNormalizer : IDocumentSchemaNormalizer
    {
        private readonly INormalizationTransformerFactory transformerFactory;

        public DocumentSchemaNormalizer(INormalizationTransformerFactory transformerFactory)
        {
            this.transformerFactory = transformerFactory ?? throw new ArgumentOutOfRangeException(nameof(transformerFactory));
        }

        public Task<object> NormalizeToDocumentSchema(IDocument document) => this.Normalize(document, document.SchemaType);

        public Task<object> NormalizeToSystem(IDocument document) => this.Normalize(document, SchemaType.System);

        private async Task<object> Normalize(IDocument document, SchemaType schemaType)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var transformer = this.transformerFactory.Create(schemaType);

            document.Xml = await transformer.TransformAsync(document.Xml).ConfigureAwait(false);

            return true;
        }
    }
}
