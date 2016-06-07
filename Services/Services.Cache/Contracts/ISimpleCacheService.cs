namespace ProcessingTools.Services.Cache.Contracts
{
    using ProcessingTools.Services.Common.Models.Contracts;

    public interface ISimpleCacheService<TServiceModel> : ICacheService<string, int, TServiceModel>
        where TServiceModel : ISimpleServiceModel
    {
    }
}