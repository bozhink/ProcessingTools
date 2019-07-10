// <copyright file="IJournalMetaDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using ProcessingTools.Contracts.Models.Documents;

namespace ProcessingTools.Contracts.Services.Meta
{
    /// <summary>
    /// Journal meta data service.
    /// </summary>
    public interface IJournalMetaDataService
    {
        /// <summary>
        /// Parse journal JSON file and returns processed journal meta-data.
        /// </summary>
        /// <param name="journalJsonFileName">JSON file to process.</param>
        /// <returns>Processed journal meta-data.</returns>
        Task<IJournalMeta> GetJournalMetaAsync(string journalJsonFileName);
    }
}
