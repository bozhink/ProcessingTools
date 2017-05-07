namespace ProcessingTools.Contracts.Services.Data.Geo.Services
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Services.Data.Geo.Filters;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;

    public interface ISynonymisableDataService<TSynonym, TSynonymFilter>
        where TSynonym : ISynonym
        where TSynonymFilter : ISynonymFilter
    {
        Task<object> AddSynonymsAsync(int modelId, params TSynonym[] synonyms);

        Task<object> RemoveSynonymsAsync(int modelId, params int[] synonymIds);

        Task<object> UpdateSynonymsAsync(int modelId, params TSynonym[] synonyms);

        Task<TSynonym[]> SelectSynonymsAsync(TSynonymFilter filter);

        Task<long> SelectSynonymCountAsync(TSynonymFilter filter);

        Task<TSynonym> GetSynonymByIdAsync(int id);
    }
}