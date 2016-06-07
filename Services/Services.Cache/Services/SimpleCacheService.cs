namespace ProcessingTools.Services.Cache
{
    using Contracts;
    using ProcessingTools.Data.Common.Models.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;
    using ProcessingTools.Services.Common.Models.Contracts;

    public class SimpleCacheService<TDbModel, TServiceModel> : GenericCacheService<string, int, TDbModel, TServiceModel>, ISimpleCacheService<TServiceModel>
        where TDbModel : IEntity
        where TServiceModel : ISimpleServiceModel
    {
        public SimpleCacheService(ISimpleGenericContextRepository<TDbModel> repository)
            : base(repository)
        {
        }
    }
}
