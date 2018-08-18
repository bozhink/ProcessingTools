// <copyright file="TaxonRanksSearchService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.Taxonomy
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Constants;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Models.Data.Bio.Taxonomy;

    /// <summary>
    /// Taxon ranks search service.
    /// </summary>
    public class TaxonRanksSearchService : ITaxonRanksSearchService
    {
        private readonly IGenericRepositoryProvider<ITaxonRanksRepository> repositoryProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonRanksSearchService"/> class.
        /// </summary>
        /// <param name="repositoryProvider">Repository provider.</param>
        public TaxonRanksSearchService(IGenericRepositoryProvider<ITaxonRanksRepository> repositoryProvider)
        {
            this.repositoryProvider = repositoryProvider ?? throw new ArgumentNullException(nameof(repositoryProvider));
        }

        /// <inheritdoc/>
        public Task<ITaxonRank[]> SearchAsync(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return Task.FromResult(Array.Empty<ITaxonRank>());
            }

            return this.repositoryProvider.ExecuteAsync<ITaxonRank[]>(async (repository) =>
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
