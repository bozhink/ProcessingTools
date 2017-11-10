namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using Abstractions.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Data.Bio.Taxonomy;

    public class CatalogueOfLifeTaxaRankResolver : AbstractTaxaRankResolverOverTaxaClassificationResolver, ICatalogueOfLifeTaxaRankResolver
    {
        public CatalogueOfLifeTaxaRankResolver(ICatalogueOfLifeTaxaClassificationResolver classificationResolver)
            : base(classificationResolver)
        {
        }
    }
}
