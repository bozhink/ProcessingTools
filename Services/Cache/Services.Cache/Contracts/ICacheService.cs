namespace ProcessingTools.Services.Cache.Contracts
{
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ICacheService<TContext, TId, TServiceModel>
        where TServiceModel : IGenericIdentifiable<TId>
    {
        Task<IQueryable<TServiceModel>> All(TContext context);

        Task<TServiceModel> Get(TContext context, TId id);

        Task Add(TContext context, TServiceModel model);

        Task Update(TContext context, TServiceModel model);

        Task Delete(TContext context);

        Task Delete(TContext context, TId id);

        Task Delete(TContext context, TServiceModel model);
    }
}