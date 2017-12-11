// <copyright file="IJournalsMetaDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data.Meta
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Documents;

    /// <summary>
    /// Journals meta data service.
    /// </summary>
    public interface IJournalsMetaDataService
    {
        /// <summary>
        /// Retrieves metadata for all journals.
        /// </summary>
        /// <returns>Array of all journal metadata.</returns>
        Task<IJournalMeta[]> GetAllJournalsMetaAsync();
    }
}
