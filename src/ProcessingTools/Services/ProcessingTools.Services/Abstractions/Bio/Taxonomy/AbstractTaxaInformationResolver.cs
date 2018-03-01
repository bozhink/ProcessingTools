// <copyright file="AbstractTaxaInformationResolver.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Abstractions.Bio.Taxonomy
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Abstract taxa information resolver.
    /// </summary>
    /// <typeparam name="T">Type of result object.</typeparam>
    public abstract class AbstractTaxaInformationResolver<T> : ITaxaInformationResolver<T>
    {
        /// <inheritdoc/>
        public async Task<T[]> ResolveAsync(params string[] scientificNames)
        {
            var queue = new ConcurrentQueue<T>();
            var exceptions = new ConcurrentQueue<Exception>();
            var tasks = scientificNames
                .Select(scientificName => this.ResolveScientificName(scientificName, queue, exceptions))
                .ToArray();

            await Task.WhenAll(tasks).ConfigureAwait(true);

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions.ToList());
            }

            return queue.ToArray();
        }

        /// <summary>
        /// Resolve scientific name.
        /// </summary>
        /// <param name="scientificName">Scientific name.</param>
        /// <returns>Resolved models.</returns>
        protected abstract Task<T[]> ResolveScientificNameAsync(string scientificName);

        private async Task ResolveScientificName(string scientificName, ConcurrentQueue<T> queue, ConcurrentQueue<Exception> exceptions)
        {
            try
            {
                var data = await this.ResolveScientificNameAsync(scientificName).ConfigureAwait(false);
                foreach (var item in data)
                {
                    queue.Enqueue(item);
                }
            }
            catch (Exception e)
            {
                exceptions.Enqueue(e);
            }
        }
    }
}
