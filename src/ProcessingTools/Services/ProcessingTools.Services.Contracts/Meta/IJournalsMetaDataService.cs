// <copyright file="IJournalsMetaDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using ProcessingTools.Contracts.Models.Documents;

namespace ProcessingTools.Contracts.Services.Meta
{
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
