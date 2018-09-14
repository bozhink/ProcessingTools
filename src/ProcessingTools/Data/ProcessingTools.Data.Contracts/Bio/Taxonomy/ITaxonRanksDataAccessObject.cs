// <copyright file="ITaxonRanksDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Taxon ranks data access object.
    /// </summary>
    public interface ITaxonRanksDataAccessObject : IDataAccessObject
    {
        /// <summary>
        /// Inserts one taxon-rank item.
        /// </summary>
        /// <param name="item">Item to be inserted.</param>
        /// <returns>Task of result.</returns>
        Task<object> InsertOneAsync(ITaxonRankItem item);

        /// <summary>
        /// Inserts many taxon-rank items.
        /// </summary>
        /// <param name="items">Items to be inserted.</param>
        /// <returns>Task of result.</returns>
        Task<object> InsertManyAsync(IEnumerable<ITaxonRankItem> items);

        /// <summary>
        /// Deletes one taxon-rank item by taxon name.
        /// </summary>
        /// <param name="name">Item to be deleted.</param>
        /// <returns>Task of result.</returns>
        Task<object> DeleteOneAsync(string name);

        /// <summary>
        /// Deletes many taxon-rank items by taxon name.
        /// </summary>
        /// <param name="names">Items to be deleted.</param>
        /// <returns>Task of result.</returns>
        Task<object> DeleteManyAsync(IEnumerable<string> names);

        /// <summary>
        /// Finds taxon-rank items by matching taxon name.
        /// </summary>
        /// <param name="filter">Filter value to be applied.</param>
        /// <returns>Task of result as string array.</returns>
        Task<ITaxonRankItem[]> FindAsync(string filter);

        /// <summary>
        /// Gets all taxon-rank items.
        /// </summary>
        /// <returns>Task of result as string array.</returns>
        Task<ITaxonRankItem[]> GetAllAsync();
    }
}
