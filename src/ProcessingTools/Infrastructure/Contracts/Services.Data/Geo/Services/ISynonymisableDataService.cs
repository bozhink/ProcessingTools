namespace ProcessingTools.Contracts.Services.Data.Geo.Services
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;

    public interface ISynonymisableDataService<TSynonym>
        where TSynonym : ISynonym
    {
        Task<object> AddSynonymAsync(int modelId, TSynonym synonym);

        Task<object> RemoveSynonymAsync(int modelId, int synonymId);

        Task<object> UpdateSynonymAsync(int modelId, TSynonym synonym);
    }
}
