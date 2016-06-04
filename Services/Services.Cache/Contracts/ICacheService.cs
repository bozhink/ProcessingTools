namespace ProcessingTools.Services.Cache.Contracts
{
    using ProcessingTools.Services.Common.Contracts;
    using ProcessingTools.Services.Common.Models.Contracts;

    public interface ICacheService<TContext, TId, TServiceModel> : IContextDataService<TContext, TId, TServiceModel>
        where TServiceModel : IGenericServiceModel<TId>
    {
    }
}