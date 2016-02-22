namespace ProcessingTools.Services.Common.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using Models.Contracts;

    public interface ICacheService<TKey, TId, TServiceModel>
        where TServiceModel : IGenericServiceModel<TId>
    {
        Task<IQueryable<TServiceModel>> All(TKey key);

        Task<IQueryable<TServiceModel>> Get(TKey key, int skip, int take);

        Task<TServiceModel> Get(TKey key, TId id);

        Task Add(TKey key, TServiceModel entity);

        Task Update(TKey key, TServiceModel entity);

        Task Delete(TKey key);

        Task Delete(TKey key, TId id);

        Task Delete(TKey key, TServiceModel entity);
    }
}
