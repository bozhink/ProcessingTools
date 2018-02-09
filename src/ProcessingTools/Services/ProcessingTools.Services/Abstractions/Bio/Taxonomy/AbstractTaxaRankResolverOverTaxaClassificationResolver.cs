namespace ProcessingTools.Services.Abstractions.Bio.Taxonomy
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;

    public abstract class AbstractTaxaRankResolverOverTaxaClassificationResolver : ITaxaRankResolver
    {
        private readonly ITaxaClassificationResolver classificationResolver;

        protected AbstractTaxaRankResolverOverTaxaClassificationResolver(ITaxaClassificationResolver classificationResolver)
        {
            this.classificationResolver = classificationResolver ?? throw new ArgumentNullException(nameof(classificationResolver));
        }

        public async Task<ITaxonRank[]> ResolveAsync(params string[] scientificNames)
        {
            return await this.classificationResolver.ResolveAsync(scientificNames).ConfigureAwait(false);
        }
    }
}
