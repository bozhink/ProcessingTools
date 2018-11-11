// <copyright file="ITaxonRanksDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Bio.Taxonomy
{
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

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
        /// <returns>Task of result as taxon-rank array.</returns>
        Task<ITaxonRankItem[]> FindAsync(string filter);

        /// <summary>
        /// Gets all white-listed taxon-rank items.
        /// </summary>
        /// <returns>Task of result as string array.</returns>
        Task<string[]> GetWhiteListedAsync();
    }
}
