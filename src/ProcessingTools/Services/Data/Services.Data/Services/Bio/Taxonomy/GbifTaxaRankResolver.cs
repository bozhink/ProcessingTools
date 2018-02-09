namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using ProcessingTools.Services.Abstractions.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;

    public class GbifTaxaRankResolver : AbstractTaxaRankResolverOverTaxaClassificationResolver, IGbifTaxaRankResolver
    {
        public GbifTaxaRankResolver(IGbifTaxaClassificationResolver classificationResolver)
            : base(classificationResolver)
        {
        }
    }
}
