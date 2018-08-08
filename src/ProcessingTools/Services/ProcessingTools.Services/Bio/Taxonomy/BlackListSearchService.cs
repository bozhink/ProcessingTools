// <copyright file="BlackListSearchService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.Taxonomy
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Taxonomic black list search service.
    /// </summary>
    public class BlackListSearchService : IBlackListSearchService
    {
        private readonly IGenericRepositoryProvider<IBiotaxonomicBlackListRepository> repositoryProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlackListSearchService"/> class.
        /// </summary>
        /// <param name="repositoryProvider">Repository provider.</param>
        public BlackListSearchService(IGenericRepositoryProvider<IBiotaxonomicBlackListRepository> repositoryProvider)
        {
            this.repositoryProvider = repositoryProvider ?? throw new ArgumentNullException(nameof(repositoryProvider));
        }

        /// <inheritdoc/>
        public Task<string[]> SearchAsync(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return Task.FromResult(Array.Empty<string>());
            }

            return this.repositoryProvider.ExecuteAsync((repository) =>
            {
                var searchString = filter.ToUpperInvariant();

                return repository.Entities
                    .Where(s => s.Content.ToUpperInvariant().Contains(searchString))
                    .Select(s => s.Content)
                    .Distinct()
                    .ToArray();
            });
        }
    }
}
