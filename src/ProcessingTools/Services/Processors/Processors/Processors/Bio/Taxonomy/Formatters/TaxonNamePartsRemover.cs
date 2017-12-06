namespace ProcessingTools.Processors.Processors.Bio.Taxonomy.Formatters
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Processors.Factories.Bio;
    using ProcessingTools.Contracts.Processors.Processors.Bio.Taxonomy.Formatters;

    public class TaxonNamePartsRemover : ITaxonNamePartsRemover
    {
        private readonly IBioTaxonomyTransformersFactory transformersFactory;

        public TaxonNamePartsRemover(IBioTaxonomyTransformersFactory transformersFactory)
        {
            this.transformersFactory = transformersFactory ?? throw new ArgumentNullException(nameof(transformersFactory));
        }

        public async Task<object> FormatAsync(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var content = await this.transformersFactory
                .GetRemoveTaxonNamePartsTransformer()
                .TransformAsync(context.Xml)
                .ConfigureAwait(false);

            context.Xml = content;

            return true;
        }
    }
}
