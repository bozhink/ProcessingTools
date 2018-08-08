namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using ProcessingTools.Services.Abstractions.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;

    public class GbifTaxonRankResolver : AbstractTaxonRankResolverOverTaxaClassificationResolver, IGbifTaxonRankResolver
    {
        public GbifTaxonRankResolver(IGbifTaxonClassificationResolver classificationResolver)
            : base(classificationResolver)
        {
        }
    }
}
