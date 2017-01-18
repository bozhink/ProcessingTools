namespace ProcessingTools.Services.Data.Contracts.Meta
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Meta;

    public interface IJournalsMetaDataService
    {
        Task<IEnumerable<IJournal>> GetAllJournalsMeta();
    }
}
