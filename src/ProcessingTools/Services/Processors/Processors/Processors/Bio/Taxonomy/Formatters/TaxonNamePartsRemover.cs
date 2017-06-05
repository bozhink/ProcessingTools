namespace ProcessingTools.Processors.Processors.Bio.Taxonomy.Formatters
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
            this.transformersFactory = transformersFactory ?? throw new ArgumentNullException(nameof(transformersFactory));
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
