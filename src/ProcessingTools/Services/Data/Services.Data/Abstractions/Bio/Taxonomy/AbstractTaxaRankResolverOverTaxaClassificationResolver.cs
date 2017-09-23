namespace ProcessingTools.Services.Data.Abstractions.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contracts.Bio.Taxonomy;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    public abstract class AbstractTaxaRankResolverOverTaxaClassificationResolver : ITaxaRankResolver
    {
        private readonly ITaxaClassificationResolver classificationResolver;

        protected AbstractTaxaRankResolverOverTaxaClassificationResolver(ITaxaClassificationResolver classificationResolver)
        {
            this.classificationResolver = classificationResolver ?? throw new ArgumentNullException(nameof(classificationResolver));
        }

        public async Task<IEnumerable<ITaxonRank>> Resolve(params string[] scientificNames)
        {
            return await this.classificationResolver.Resolve(scientificNames).ConfigureAwait(false);
        }
    }
}
