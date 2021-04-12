// <copyright file="ITaxonInformationResolver{T}.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Contracts.Models;

    /// <summary>
    /// Taxon information resolver.
    /// </summary>
    /// <typeparam name="T">Type of result object.</typeparam>
    public interface ITaxonInformationResolver<T>
        where T : ITaxonSearchResult
    {
        /// <summary>
        /// Resolves information about specified scientific names.
        /// </summary>
        /// <param name="names">Taxonomic names to be resolved.</param>
        /// <returns>Information about specified scientific names.</returns>
        Task<IList<T>> ResolveAsync(IEnumerable<string> names);
    }
}
