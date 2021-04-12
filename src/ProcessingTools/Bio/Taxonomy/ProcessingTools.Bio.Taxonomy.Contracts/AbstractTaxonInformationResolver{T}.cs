// <copyright file="AbstractTaxonInformationResolver{T}.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Contracts
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Contracts.Models;

    /// <summary>
    /// Abstract taxon information resolver.
    /// </summary>
    /// <typeparam name="T">Type of result object.</typeparam>
    public abstract class AbstractTaxonInformationResolver<T> : ITaxonInformationResolver<T>
        where T : ITaxonSearchResult
    {
        /// <inheritdoc/>
        public async Task<IList<T>> ResolveAsync(IEnumerable<string> names)
        {
            var resolvedData = new ConcurrentQueue<T>();
            var exceptions = new ConcurrentQueue<Exception>();

            var tasks = names.Select(name => this.ResolveSingleName(name, resolvedData, exceptions)).ToArray();

            await Task.WhenAll(tasks).ConfigureAwait(true);

            if (exceptions.Any())
            {
                throw new AggregateException(exceptions.ToList());
            }

            return resolvedData.ToArray();
        }

        /// <summary>
        /// Resolve scientific name.
        /// </summary>
        /// <param name="name">Taxon name.</param>
        /// <returns>Resolved information.</returns>
        protected abstract Task<IList<T>> ResolveNameAsync(string name);

        private async Task ResolveSingleName(string name, ConcurrentQueue<T> resolvedData, ConcurrentQueue<Exception> exceptions)
        {
            if (resolvedData is null || exceptions is null || string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            try
            {
                var searchResults = await this.ResolveNameAsync(name).ConfigureAwait(false);

                foreach (var searchResult in searchResults)
                {
                    searchResult.SearchKey = name;

                    resolvedData.Enqueue(searchResult);
                }
            }
            catch (Exception e)
            {
                exceptions.Enqueue(e);
            }
        }
    }
}
