namespace ProcessingTools.Services.Cache
{
    using Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Common.Models.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public class SimpleCacheService<TDbModel, TServiceModel> : GenericCacheService<string, int, TDbModel, TServiceModel>, ISimpleCacheService<TServiceModel>
        where TDbModel : IEntity
        where TServiceModel : IIntegerIdentifiable
    {
        public SimpleCacheService(ISimpleGenericContextRepository<TDbModel> repository)
            : base(repository)
        {
        }
    }
}
