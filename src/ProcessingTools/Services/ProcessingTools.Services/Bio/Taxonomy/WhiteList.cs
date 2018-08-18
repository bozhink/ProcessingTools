// <copyright file="WhiteList.cs" company="ProcessingTools">
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
    /// Taxonomic white list.
    /// </summary>
    public class WhiteList : IWhiteList
    {
        private readonly IGenericRepositoryProvider<ITaxonRanksRepository> repositoryProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="WhiteList"/> class.
        /// </summary>
        /// <param name="repositoryProvider">Repository provider.</param>
        public WhiteList(IGenericRepositoryProvider<ITaxonRanksRepository> repositoryProvider)
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
            return this.repositoryProvider.ExecuteAsync<IEnumerable<string>>(async (repository) =>
            {
                var data = await repository.FindAsync(t => t.IsWhiteListed).ConfigureAwait(false);

                var result = data.Select(t => t.Name).ToList();

                return new HashSet<string>(result);
            });
        }
    }
}
