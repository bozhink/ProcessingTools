namespace ProcessingTools.Contracts.Data.Cache.Repositories
{
    using ProcessingTools.Contracts.Data.Cache.Models;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IValidationCacheDataRepository : IStringKeyCollectionValuePairsRepository<IValidationCacheEntity>
    {
    }
}
