namespace ProcessingTools.Contracts.Services.Data.Geo.Services
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;

    public interface ISynonymisableDataService<TSynonym>
        where TSynonym : ISynonym
    {
        Task<object> AddSynonymAsync(object modelId, TSynonym synonym);

        Task<object> RemoveSynonymAsync(object modelId, int synonymId);

        Task<object> UpdateSynonymAsync(object modelId, TSynonym synonym);
    }
}
