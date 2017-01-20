﻿namespace ProcessingTools.Services.Data.Contracts.Meta
{
    using System.Threading.Tasks;
    using Models.Meta;

    public interface IJournalMetaDataService
    {
        Task<IJournal> GetJournalMeta(string journalJsonFileName);
    }
}