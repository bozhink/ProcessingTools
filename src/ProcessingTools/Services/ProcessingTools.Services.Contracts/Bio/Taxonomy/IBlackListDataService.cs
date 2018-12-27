// <copyright file="IBlackListDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Bio.Taxonomy
{
    using System.Threading.Tasks;

    /// <summary>
    /// Taxonomic black list data service.
    /// </summary>
    public interface IBlackListDataService
    {
        /// <summary>
        /// Adds models.
        /// </summary>
        /// <param name="models">Models to be added.</param>
        /// <returns>Task</returns>
        Task<object> AddAsync(params string[] models);

        /// <summary>
        /// Deletes models.
        /// </summary>
        /// <param name="models">Models to be deleted.</param>
        /// <returns>Task</returns>
        Task<object> DeleteAsync(params string[] models);

        /// <summary>
        /// Do search with a specified filter.
        /// </summary>
        /// <param name="filter">Filter object for search.</param>
        /// <returns>Array of found objects.</returns>
        Task<string[]> SearchAsync(string filter);
    }
}
