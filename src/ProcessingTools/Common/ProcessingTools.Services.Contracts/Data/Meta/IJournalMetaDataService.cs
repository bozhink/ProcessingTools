namespace ProcessingTools.Services.Data.Contracts.Meta
{
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Documents;

    public interface IJournalMetaDataService
    {
        Task<IJournalMeta> GetJournalMeta(string journalJsonFileName);
    }
}
