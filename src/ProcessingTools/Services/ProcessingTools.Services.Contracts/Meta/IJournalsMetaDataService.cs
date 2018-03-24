// <copyright file="IJournalsMetaDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Meta
{
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Documents;

    /// <summary>
    /// Journals meta data service.
    /// </summary>
    public interface IJournalsMetaDataService
    {
        /// <summary>
        /// Retrieves meta-data for all journals.
        /// </summary>
        /// <returns>Array of all journal meta-data.</returns>
        Task<IJournalMeta[]> GetAllJournalsMetaAsync();
    }
}
