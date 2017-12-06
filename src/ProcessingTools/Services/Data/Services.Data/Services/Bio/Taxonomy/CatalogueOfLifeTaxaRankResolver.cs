namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using Abstractions.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services.Data.Bio.Taxonomy;

    public class CatalogueOfLifeTaxaRankResolver : AbstractTaxaRankResolverOverTaxaClassificationResolver, ICatalogueOfLifeTaxaRankResolver
    {
        public CatalogueOfLifeTaxaRankResolver(ICatalogueOfLifeTaxaClassificationResolver classificationResolver)
            : base(classificationResolver)
        {
        }
    }
}
