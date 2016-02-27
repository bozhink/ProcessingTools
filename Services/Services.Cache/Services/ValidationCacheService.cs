namespace ProcessingTools.Services.Cache
{
    using Contracts;
    using Models.Contracts;

    using ProcessingTools.Cache.Data.Models;
    using ProcessingTools.Cache.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common.Factories;

    public class ValidationCacheService : SimpleCacheServiceFactory<ValidationCacheEntity, IValidationCacheServiceModel>, IValidationCacheService
    {
        public ValidationCacheService(IValidationCacheDataRepository repository)
            : base(repository)
        {
        }
    }
}
