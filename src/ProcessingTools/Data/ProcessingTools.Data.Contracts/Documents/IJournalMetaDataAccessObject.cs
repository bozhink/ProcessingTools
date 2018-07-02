// <copyright file="IJournalMetaDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Documents;

    /// <summary>
    /// Journals meta data access object.
    /// </summary>
    public interface IJournalMetaDataAccessObject : IDataAccessObject
    {
        /// <summary>
        /// Inserts new journal meta item to data store.
        /// </summary>
        /// <param name="journalMeta">Journal meta item to be inserted.</param>
        /// <returns>Inserted journal meta item.</returns>
        Task<IJournalMeta> InsertAsync(IJournalMeta journalMeta);

        /// <summary>
        /// Updates journal meta item to data store.
        /// </summary>
        /// <param name="journalMeta">Journal meta item to be updated.</param>
        /// <returns>Updated journal meta item.</returns>
        Task<IJournalMeta> UpdateAsync(IJournalMeta journalMeta);

        /// <summary>
        /// Deletes journal meta item from data store.
        /// </summary>
        /// <param name="id">ID of the journal meta item to be deleted.</param>
        /// <returns>Task</returns>
        Task<object> DeleteAsync(object id);

        /// <summary>
        /// Gets all journal meta items.
        /// </summary>
        /// <returns>Array of journal meta items.</returns>
        Task<IJournalMeta[]> GetAllAsync();

        /// <summary>
        /// Gets journal meta item by ID.
        /// </summary>
        /// <param name="id">ID of the journal meta item.</param>
        /// <returns>Journal meta item.</returns>
        Task<IJournalMeta> GetAsync(object id);
    }
}
