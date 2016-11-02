namespace ProcessingTools.Services.Cache.Contracts.Validation
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Validation;

    public interface IValidationCacheService
    {
        IEnumerable<IValidationCacheServiceModel> GetAll(string key);

        Task<object> Add(string key, IValidationCacheServiceModel value);

        Task<object> Remove(string key, IValidationCacheServiceModel value);
    }
}
