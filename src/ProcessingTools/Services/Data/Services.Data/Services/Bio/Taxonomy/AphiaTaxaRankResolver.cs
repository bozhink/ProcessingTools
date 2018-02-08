namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Data.Abstractions.Bio.Taxonomy;

    public class AphiaTaxaRankResolver : AbstractTaxaRankResolverOverTaxaClassificationResolver, IAphiaTaxaRankResolver
    {
        public AphiaTaxaRankResolver(IAphiaTaxaClassificationResolver classificationResolver)
            : base(classificationResolver)
        {
        }
    }
}
