namespace ProcessingTools.Layout.Processors.Processors.Normalizers
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Layout.Processors.Contracts.Factories;
    using ProcessingTools.Layout.Processors.Contracts.Normalizers;

    public class DocumentNormalizer : IDocumentNormalizer
    {
        private readonly INormalizationTransformerFactory transformerFactory;

        public DocumentNormalizer(INormalizationTransformerFactory transformerFactory)
        {
            if (transformerFactory == null)
            {
                throw new ArgumentOutOfRangeException(nameof(transformerFactory));
            }

            this.transformerFactory = transformerFactory;
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

            document.Xml = await transformer.Transform(document.Xml);

            return true;
        }
    }
}
