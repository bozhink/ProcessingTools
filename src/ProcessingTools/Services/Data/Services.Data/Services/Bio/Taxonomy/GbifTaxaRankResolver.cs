namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using Abstractions.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Data.Bio.Taxonomy;

    public class GbifTaxaRankResolver : AbstractTaxaRankResolverOverTaxaClassificationResolver, IGbifTaxaRankResolver
    {
        public GbifTaxaRankResolver(IGbifTaxaClassificationResolver classificationResolver)
            : base(classificationResolver)
        {
        }
    }
}
