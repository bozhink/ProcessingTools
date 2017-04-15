namespace ProcessingTools.Contracts.Services.Data.Geo.Services
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;

    public interface ISynonymisableDataService<TSynonym>
        where TSynonym : ISynonym
    {
        Task<object> AddSynonymAsync(TSynonym synonym);

        Task<object> RemoveSynonymAsync(int synonymId);

        Task<object> UpdateSynonymAsync(TSynonym synonym);
    }
}
