namespace ProcessingTools.Services.Cache.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

    public interface ICacheService<TContext, TServiceModel>
        where TServiceModel : IEntity
    {
        IEnumerable<TServiceModel> All(TContext context);

        Task<TServiceModel> Get(TContext context, object id);

        Task Add(TContext context, TServiceModel model);

        Task Update(TContext context, TServiceModel model);

        Task Delete(TContext context);

        Task Delete(TContext context, object id);

        Task Delete(TContext context, TServiceModel model);
    }
}
