namespace ProcessingTools.NlmArchiveConsoleManager.Contracts.Services
{
    using System.Threading.Tasks;
    using Models;

    public interface IJournalsMetaDataService
    {
        Task<IJournal> GetJournalMeta();
    }
}