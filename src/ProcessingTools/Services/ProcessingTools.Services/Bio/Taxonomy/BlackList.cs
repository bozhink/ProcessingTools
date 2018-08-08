// <copyright file="BlackList.cs" company="ProcessingTools">
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

    /// <summary>
    /// Taxonomic black list.
    /// </summary>
    public class BlackList : IBlackList
    {
        private readonly IGenericRepositoryProvider<IBiotaxonomicBlackListRepository> repositoryProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlackList"/> class.
        /// </summary>
        /// <param name="repositoryProvider">Repository provider.</param>
        public BlackList(IGenericRepositoryProvider<IBiotaxonomicBlackListRepository> repositoryProvider)
        {
            this.repositoryProvider = repositoryProvider ?? throw new ArgumentNullException(nameof(repositoryProvider));
        }

        /// <inheritdoc/>
        public IEnumerable<string> GetItems()
        {
            return this.GetItemsAsync().Result;
        }

        /// <inheritdoc/>
        public Task<IEnumerable<string>> GetItemsAsync()
        {
            return this.repositoryProvider.ExecuteAsync<IEnumerable<string>>((repository) =>
            {
                var result = repository.Entities
                    .Select(s => s.Content)
                    .ToList();

                return new HashSet<string>(result);
            });
        }
    }
}
