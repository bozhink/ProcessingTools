// <copyright file="ITaxonRanksDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;

    /// <summary>
    /// Taxon ranks data access object.
    /// </summary>
    public interface ITaxonRanksDataAccessObject : IDataAccessObject
    {
        /// <summary>
        /// Inserts or updates taxon-rank item.
        /// </summary>
        /// <param name="item">Item to be inserted.</param>
        /// <returns>Task of result.</returns>
        Task<object> UpsertAsync(ITaxonRankItem item);

        /// <summary>
        /// Deletes taxon-rank item by taxon name.
        /// </summary>
        /// <param name="name">Item to be deleted.</param>
        /// <returns>Task of result.</returns>
        Task<object> DeleteAsync(string name);

        /// <summary>
        /// Finds taxon-rank items by matching taxon name.
        /// </summary>
        /// <param name="filter">Filter value to be applied.</param>
        /// <returns>Task of result as taxon-rank collection.</returns>
        Task<IList<ITaxonRankItem>> FindAsync(string filter);

        /// <summary>
        /// Finds taxon-rank items by exact matching taxon name.
        /// </summary>
        /// <param name="filter">Filter value to be applied.</param>
        /// <returns>Task of result as taxon-rank collection.</returns>
        Task<IList<ITaxonRankItem>> FindExactAsync(string filter);

        /// <summary>
        /// Gets all taxon-rank items.
        /// </summary>
        /// <returns>Task of result as taxon-rank collection.</returns>
        Task<IList<ITaxonRankItem>> GetAllAsync();

        /// <summary>
        /// Gets all white-listed taxon-rank items.
        /// </summary>
        /// <returns>Task of result as string collection.</returns>
        Task<IList<string>> GetWhiteListedAsync();
    }
}
