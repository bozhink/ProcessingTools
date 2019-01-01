// <copyright file="ITaxonInformationResolver{T}.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Taxon information resolver.
    /// </summary>
    /// <typeparam name="T">Type of result object.</typeparam>
    public interface ITaxonInformationResolver<T>
    {
        /// <summary>
        /// Resolves information about specified scientific names.
        /// </summary>
        /// <param name="scientificNames">Scientific names to be resolved.</param>
        /// <returns>Information about specified scientific names.</returns>
        Task<IList<T>> ResolveAsync(IEnumerable<string> scientificNames);
    }
}
