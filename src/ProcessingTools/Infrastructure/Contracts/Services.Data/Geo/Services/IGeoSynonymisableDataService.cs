namespace ProcessingTools.Contracts.Services.Data.Geo.Services
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Contracts.Models.Geo;

    public interface IGeoSynonymisableDataService<TModel, TSynonym, TSynonymFilter>
        where TModel : IGeoSynonymisable<TSynonym>
        where TSynonym : IGeoSynonym
        where TSynonymFilter : ISynonymFilter
    {
        Task<object> InsertAsync(TModel model, params TSynonym[] synonyms);

        Task<object> AddSynonymsAsync(int modelId, params TSynonym[] synonyms);

        Task<object> RemoveSynonymsAsync(int modelId, params int[] synonymIds);

        Task<object> UpdateSynonymsAsync(int modelId, params TSynonym[] synonyms);

        Task<TSynonym[]> SelectSynonymsAsync(int modelId, TSynonymFilter filter);

        Task<long> SelectSynonymCountAsync(int modelId, TSynonymFilter filter);

        Task<TSynonym> GetSynonymByIdAsync(int modelId, int id);
    }
}
