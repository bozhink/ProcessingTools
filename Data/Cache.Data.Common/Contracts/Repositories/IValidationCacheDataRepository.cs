namespace ProcessingTools.Cache.Data.Common.Contracts.Repositories
{
    using Models;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IValidationCacheDataRepository : IStringKeyCollectionValuePairsRepository<IValidationCacheEntity>
    {
    }
}
