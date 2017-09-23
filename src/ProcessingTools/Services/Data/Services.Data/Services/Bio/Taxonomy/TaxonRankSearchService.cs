namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Data.Bio.Taxonomy.Repositories;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Data.Models.Bio.Taxonomy;

    public class TaxonRankSearchService : ITaxonRankSearchService
    {
        private readonly IGenericRepositoryProvider<ITaxonRankRepository> repositoryProvider;

        public TaxonRankSearchService(IGenericRepositoryProvider<ITaxonRankRepository> repositoryProvider)
        {
            this.repositoryProvider = repositoryProvider ?? throw new ArgumentNullException(nameof(repositoryProvider));
        }

        public async Task<IEnumerable<ITaxonRank>> Search(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return new ITaxonRank[] { };
            }

            return await this.repositoryProvider.Execute(async (repository) =>
            {
                var searchString = filter.ToLowerInvariant();

                var query = await repository.FindAsync(t => t.Name.ToLower().Contains(searchString)).ConfigureAwait(false);
                var data = query.ToList();
                var result = data.SelectMany(
                    t => t.Ranks.Select(rank => new TaxonRankServiceModel
                    {
                        ScientificName = t.Name,
                        Rank = rank
                    }))
                    .Take(PaginationConstants.DefaultLargeNumberOfItemsPerPage)
                    .ToList();

                return result;
            })
            .ConfigureAwait(false);
        }
    }
}
