namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using ProcessingTools.Contracts.Services.Data.Bio.Taxonomy;
    using ProcessingTools.Services.Data.Abstractions.Bio.Taxonomy;

    public class GbifTaxaRankResolver : AbstractTaxaRankResolverOverTaxaClassificationResolver, IGbifTaxaRankResolver
    {
        public GbifTaxaRankResolver(IGbifTaxaClassificationResolver classificationResolver)
            : base(classificationResolver)
        {
        }
    }
}
