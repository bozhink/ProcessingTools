namespace ProcessingTools.Services.Data.Contracts.Meta
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Documents;

    public interface IJournalsMetaDataService
    {
        Task<IJournalMeta[]> GetAllJournalsMetaAsync();
    }
}
