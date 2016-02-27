namespace ProcessingTools.Services.Cache
{
    using Contracts;
    using Models;

    using ProcessingTools.Cache.Data.Models;
    using ProcessingTools.Cache.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common.Factories;

    public class ValidationCacheService : SimpleCacheServiceFactory<ValidationCacheEntity, ValidationCacheServiceModel>, IValidationCacheService
    {
        public ValidationCacheService(IValidationCacheDataRepository repository)
            : base(repository)
        {
        }
    }
}
