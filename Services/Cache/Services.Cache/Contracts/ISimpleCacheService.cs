namespace ProcessingTools.Services.Cache.Contracts
{
    using ProcessingTools.Contracts;

    public interface ISimpleCacheService<TServiceModel> : ICacheService<string, int, TServiceModel>
        where TServiceModel : IIntegerIdentifiable
    {
    }
}
