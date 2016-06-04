namespace ProcessingTools.Services.Common.Contracts
{
    using Models.Contracts;

    public interface ICacheService<TContext, TId, TServiceModel> : IContextDataService<TContext, TId, TServiceModel>
        where TServiceModel : IGenericServiceModel<TId>
    {
    }
}
