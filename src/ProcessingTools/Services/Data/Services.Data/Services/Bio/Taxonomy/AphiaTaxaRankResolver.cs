namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using Abstractions.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services.Data.Bio.Taxonomy;

    public class AphiaTaxaRankResolver : AbstractTaxaRankResolverOverTaxaClassificationResolver, IAphiaTaxaRankResolver
    {
        public AphiaTaxaRankResolver(IAphiaTaxaClassificationResolver classificationResolver)
            : base(classificationResolver)
        {
        }
    }
}
