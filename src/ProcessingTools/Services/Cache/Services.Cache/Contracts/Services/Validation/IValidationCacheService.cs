namespace ProcessingTools.Services.Cache.Contracts.Services.Validation
{
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Cache;

    public interface IValidationCacheService
    {
        Task<object> Add(string key, IValidationCacheModel value);

        Task<IValidationCacheModel> Get(string key);
    }
}
