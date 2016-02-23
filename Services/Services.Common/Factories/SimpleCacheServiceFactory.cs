namespace ProcessingTools.Services.Common.Factories
{
    using Contracts;
    using Models.Contracts;
    using ProcessingTools.Data.Common.Models.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public abstract class SimpleCacheServiceFactory<TDbModel, TServiceModel> : GenericCacheServiceFactory<string, int, TDbModel, TServiceModel>, ISimpleCacheService<TServiceModel>
        where TDbModel : IEntity
        where TServiceModel : ISimpleServiceModel
    {
        public SimpleCacheServiceFactory(ISimpleGenericRepository<TDbModel> repository)
            : base(repository)
        {
        }
    }
}