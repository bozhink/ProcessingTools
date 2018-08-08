namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using ProcessingTools.Services.Abstractions.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;

    public class CatalogueOfLifeTaxonRankResolver : AbstractTaxonRankResolverOverTaxaClassificationResolver, ICatalogueOfLifeTaxonRankResolver
    {
        public CatalogueOfLifeTaxonRankResolver(ICatalogueOfLifeTaxonClassificationResolver classificationResolver)
            : base(classificationResolver)
        {
        }
    }
}
