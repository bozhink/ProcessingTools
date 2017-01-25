namespace ProcessingTools.Services.Cache.Contracts.Services.Validation
{
    using System.Threading.Tasks;
    using Models.Validation;

    public interface IValidationCacheService
    {
        Task<object> Add(string key, IValidationCacheServiceModel value);

        Task<IValidationCacheServiceModel> Get(string key);
    }
}
