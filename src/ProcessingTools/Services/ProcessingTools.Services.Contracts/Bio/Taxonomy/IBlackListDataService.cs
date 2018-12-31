// <copyright file="IBlackListDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Taxonomic blacklist data service.
    /// </summary>
    public interface IBlackListDataService
    {
        /// <summary>
        /// Add blacklist items.
        /// </summary>
        /// <param name="items">Items to be added.</param>
        /// <returns>Task</returns>
        Task<object> InsertAsync(IEnumerable<string> items);

        /// <summary>
        /// Delete blacklist items.
        /// </summary>
        /// <param name="items">Items to be deleted.</param>
        /// <returns>Task</returns>
        Task<object> DeleteAsync(IEnumerable<string> items);

        /// <summary>
        /// Do search with a specified filter.
        /// </summary>
        /// <param name="filter">Filter string for search.</param>
        /// <returns>Collection of found blacklist items.</returns>
        Task<IList<string>> SearchAsync(string filter);
    }
}
