namespace ProcessingTools.Services.Common.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using Models.Contracts;

    public interface ICacheService<TContext, TId, TServiceModel>
        where TServiceModel : IGenericServiceModel<TId>
    {
        Task<IQueryable<TServiceModel>> All(TContext context);

        Task<TServiceModel> Get(TContext context, TId id);

        Task Add(TContext context, TServiceModel entity);

        Task Update(TContext context, TServiceModel entity);

        Task Delete(TContext context);

        Task Delete(TContext context, TId id);

        Task Delete(TContext context, TServiceModel entity);
    }
}
