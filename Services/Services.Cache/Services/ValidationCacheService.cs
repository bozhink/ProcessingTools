namespace ProcessingTools.Services.Cache
{
    using Contracts;
    using Models.Contracts;

    using ProcessingTools.Cache.Data.Models.Contracts;
    using ProcessingTools.Cache.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common.Factories;

    public class ValidationCacheService : SimpleCacheServiceFactory<IValidationCacheEntity, IValidationCacheServiceModel>, IValidationCacheService
    {
        public ValidationCacheService(IValidationCacheDataRepository repository)
            : base(repository)
        {
        }
    }
}
