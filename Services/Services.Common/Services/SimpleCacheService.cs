namespace ProcessingTools.Services.Common
{
    using Contracts;
    using Models.Contracts;
    using ProcessingTools.Data.Common.Models.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public abstract class SimpleCacheService<TDbModel, TServiceModel> : GenericCacheService<string, int, TDbModel, TServiceModel>, ISimpleCacheService<TServiceModel>
        where TDbModel : IEntity
        where TServiceModel : ISimpleServiceModel
    {
        public SimpleCacheService(ISimpleGenericContextRepository<TDbModel> repository)
            : base(repository)
        {
        }
    }
}