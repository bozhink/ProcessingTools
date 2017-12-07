namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using ProcessingTools.Contracts.Services.Data.Bio.Taxonomy;
    using ProcessingTools.Services.Data.Abstractions.Bio.Taxonomy;

    public class CatalogueOfLifeTaxaRankResolver : AbstractTaxaRankResolverOverTaxaClassificationResolver, ICatalogueOfLifeTaxaRankResolver
    {
        public CatalogueOfLifeTaxaRankResolver(ICatalogueOfLifeTaxaClassificationResolver classificationResolver)
            : base(classificationResolver)
        {
        }
    }
}
