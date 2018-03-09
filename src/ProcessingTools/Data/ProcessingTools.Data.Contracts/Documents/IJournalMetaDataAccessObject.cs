// <copyright file="IJournalMetaDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Documents;

    /// <summary>
    /// Journals meta data access object.
    /// </summary>
    public interface IJournalMetaDataAccessObject
    {
        /// <summary>
        /// Get all journal meta items.
        /// </summary>
        /// <returns>Array of journal meta items.</returns>
        Task<IJournalMeta[]> GetAllAsync();
    }
}
