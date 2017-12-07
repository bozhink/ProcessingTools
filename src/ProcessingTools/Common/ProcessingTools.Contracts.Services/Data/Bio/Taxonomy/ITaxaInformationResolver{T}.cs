// <copyright file="ITaxaInformationResolver{T}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data.Bio.Taxonomy
{
    using System.Threading.Tasks;

    /// <summary>
    /// Taxon information resolver.
    /// </summary>
    /// <typeparam name="T">Type of result object.</typeparam>
    public interface ITaxaInformationResolver<T>
    {
        /// <summary>
        /// Resolves information about specified scientific names.
        /// </summary>
        /// <param name="scientificNames">Scientific names to be resolved.</param>
        /// <returns>Information about specified scientific names.</returns>
        Task<T[]> ResolveAsync(params string[] scientificNames);
    }
}
