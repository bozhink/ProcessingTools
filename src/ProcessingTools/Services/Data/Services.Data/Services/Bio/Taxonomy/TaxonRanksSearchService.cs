namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Contracts.Data.Repositories.Bio.Taxonomy;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services.Data.Bio.Taxonomy;
    using ProcessingTools.Services.Models.Data.Bio.Taxonomy;

    public class TaxonRanksSearchService : ITaxonRanksSearchService
    {
        private readonly IGenericRepositoryProvider<ITaxonRanksRepository> repositoryProvider;

        public TaxonRanksSearchService(IGenericRepositoryProvider<ITaxonRanksRepository> repositoryProvider)
        {
            this.repositoryProvider = repositoryProvider ?? throw new ArgumentNullException(nameof(repositoryProvider));
        }

        public Task<ITaxonRank[]> SearchAsync(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return Task.FromResult(new ITaxonRank[] { });
            }

            return this.repositoryProvider.Execute<ITaxonRank[]>(async (repository) =>
            {
                var searchString = filter.ToLowerInvariant();

                var data = await repository.FindAsync(t => t.Name.ToLower().Contains(searchString)).ConfigureAwait(false);
                var result = data.SelectMany(
                    t => t.Ranks.Select(rank => new TaxonRank
                    {
                        ScientificName = t.Name,
                        Rank = rank
                    }))
                    .Take(PaginationConstants.DefaultLargeNumberOfItemsPerPage)
                    .ToArray();

                return result;
            });
        }
    }
}
