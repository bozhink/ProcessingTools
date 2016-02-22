namespace ProcessingTools.Services.Common.Contracts
{
    using System.Linq;

    using Models.Contracts;

    public interface ICacheService<TKey, TId, TServiceModel>
        where TServiceModel : IGenericServiceModel<TId>
    {
        IQueryable<TServiceModel> All(TKey key);

        IQueryable<TServiceModel> Get(TKey key, int skip, int take);

        TServiceModel Get(TKey key, TId id);

        void Add(TKey key, TServiceModel entity);

        void Update(TKey key, TServiceModel entity);

        void Delete(TKey key);

        void Delete(TKey key, TId id);

        void Delete(TKey key, TServiceModel entity);
    }
}
