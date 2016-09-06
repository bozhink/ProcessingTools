namespace ProcessingTools.Services.Cache.Contracts
{
    using ProcessingTools.Contracts;
    using ProcessingTools.Services.Common.Contracts;

    public interface ICacheService<TContext, TId, TServiceModel> : IContextDataService<TContext, TId, TServiceModel>
        where TServiceModel : IGenericIdentifiable<TId>
    {
    }
}