// <copyright file="IJournalMetaDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data.Meta
{
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Documents;

    /// <summary>
    /// Journal meta data service.
    /// </summary>
    public interface IJournalMetaDataService
    {
        /// <summary>
        /// Parse journal JSON file and returns processed journal metadata.
        /// </summary>
        /// <param name="journalJsonFileName">JSON file to process.</param>
        /// <returns>Processed journal metadata.</returns>
        Task<IJournalMeta> GetJournalMetaAsync(string journalJsonFileName);
    }
}
