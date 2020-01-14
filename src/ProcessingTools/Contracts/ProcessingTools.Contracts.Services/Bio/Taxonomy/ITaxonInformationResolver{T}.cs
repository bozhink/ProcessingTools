// <copyright file="ITaxonInformationResolver{T}.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Taxon information resolver.
    /// </summary>
    /// <typeparam name="T">Type of result object.</typeparam>
    public interface ITaxonInformationResolver<T>
        where T : ISearchResult
    {
        /// <summary>
        /// Resolves information about specified scientific names.
        /// </summary>
        /// <param name="names">Taxonomic names to be resolved.</param>
        /// <returns>Information about specified scientific names.</returns>
        Task<IList<T>> ResolveAsync(IEnumerable<string> names);
    }
}
