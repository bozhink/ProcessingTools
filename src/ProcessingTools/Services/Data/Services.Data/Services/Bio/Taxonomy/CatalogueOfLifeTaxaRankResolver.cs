namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Data.Abstractions.Bio.Taxonomy;

    public class CatalogueOfLifeTaxaRankResolver : AbstractTaxaRankResolverOverTaxaClassificationResolver, ICatalogueOfLifeTaxaRankResolver
    {
        public CatalogueOfLifeTaxaRankResolver(ICatalogueOfLifeTaxaClassificationResolver classificationResolver)
            : base(classificationResolver)
        {
        }
    }
}
