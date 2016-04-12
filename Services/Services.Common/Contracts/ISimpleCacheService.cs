namespace ProcessingTools.Services.Common.Contracts
{
    using Models.Contracts;

    public interface ISimpleCacheService<TServiceModel> : ICacheService<string, int, TServiceModel>
        where TServiceModel : ISimpleServiceModel
    {
    }
}
