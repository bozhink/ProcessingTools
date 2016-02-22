namespace ProcessingTools.Services.Common.Contracts
{
    using Models.Contracts;

    public interface ISimpleCacheService<T> : ICacheService<string, int, T>
        where T : IGenericServiceModel<int>
    {
    }
}
