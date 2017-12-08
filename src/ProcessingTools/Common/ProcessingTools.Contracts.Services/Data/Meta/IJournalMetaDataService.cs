// <copyright file="IJournalMetaDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Data.Contracts.Meta
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Documents;

    public interface IJournalMetaDataService
    {
        Task<IJournalMeta> GetJournalMeta(string journalJsonFileName);
    }
}
