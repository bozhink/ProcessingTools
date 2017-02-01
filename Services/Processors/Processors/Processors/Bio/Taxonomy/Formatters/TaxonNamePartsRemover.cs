namespace ProcessingTools.Bio.Taxonomy.Processors.Processors.Formatters
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Factories.Bio;
    using ProcessingTools.Processors.Contracts.Processors.Bio.Taxonomy.Formatters;

    public class TaxonNamePartsRemover : ITaxonNamePartsRemover
    {
        private readonly IBioTaxonomyTransformersFactory transformersFactory;

        public TaxonNamePartsRemover(IBioTaxonomyTransformersFactory transformersFactory)
        {
            if (transformersFactory == null)
            {
                throw new ArgumentNullException(nameof(transformersFactory));
            }

            this.transformersFactory = transformersFactory;
        }

        public async Task<object> Format(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var content = await this.transformersFactory
                .GetRemoveTaxonNamePartsTransformer()
                .Transform(document.Xml);

            document.Xml = content;

            return true;
        }
    }
}
