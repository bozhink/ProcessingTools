// <copyright file="BlackListDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Models.Data.Bio.Taxonomy;

    /// <summary>
    /// Taxonomic black list data service.
    /// </summary>
    public class BlackListDataService : IBlackListDataService
    {
        private readonly IGenericRepositoryProvider<IBiotaxonomicBlackListRepository> repositoryProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlackListDataService"/> class.
        /// </summary>
        /// <param name="repositoryProvider">Repository provider.</param>
        public BlackListDataService(IGenericRepositoryProvider<IBiotaxonomicBlackListRepository> repositoryProvider)
        {
            this.repositoryProvider = repositoryProvider ?? throw new ArgumentNullException(nameof(repositoryProvider));
        }

        /// <inheritdoc/>
        public Task<object> AddAsync(params string[] models)
        {
            var validItems = this.ValidateInputItems(models);

            return this.repositoryProvider.ExecuteAsync(async (repository) =>
            {
                var tasks = validItems.Select(s => new BlackListEntity
                {
                    Content = s
                })
                .Select(b => repository.AddAsync(b))
                .ToArray();

                await Task.WhenAll(tasks).ConfigureAwait(false);
                return await repository.SaveChangesAsync().ConfigureAwait(false);
            });
        }

        /// <inheritdoc/>
        public Task<object> DeleteAsync(params string[] models)
        {
            var validItems = this.ValidateInputItems(models);

            return this.repositoryProvider.ExecuteAsync(async (repository) =>
            {
                var tasks = validItems.Select(b => repository.DeleteAsync(b)).ToArray();
                await Task.WhenAll(tasks).ConfigureAwait(false);
                return await repository.SaveChangesAsync().ConfigureAwait(false);
            });
        }

        private IEnumerable<string> ValidateInputItems(params string[] items)
        {
            if (items == null || items.Length < 1)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var validItems = items.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

            if (validItems.Length < 1)
            {
                throw new ArgumentNullException(nameof(items));
            }

            return validItems;
        }
    }
}
