namespace ProcessingTools.Services.Data.Abstractions.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contracts.Bio.Taxonomy;
    using ProcessingTools.Bio.Taxonomy.Contracts;

    public abstract class AbstractTaxaRankResolverOverTaxaClassificationResolver : ITaxaRankResolver
    {
        private readonly ITaxaClassificationResolver classificationResolver;

        public AbstractTaxaRankResolverOverTaxaClassificationResolver(ITaxaClassificationResolver classificationResolver)
        {
            if (classificationResolver == null)
            {
                throw new ArgumentNullException(nameof(classificationResolver));
            }

            this.classificationResolver = classificationResolver;
        }

        public async Task<IEnumerable<ITaxonRank>> Resolve(params string[] scientificNames)
        {
            return await this.classificationResolver.Resolve(scientificNames);
        }
    }
}
