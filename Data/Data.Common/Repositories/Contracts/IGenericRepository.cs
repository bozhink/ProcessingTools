namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IGenericRepository<TEntity> : ICountableRepository<TEntity>, ISearchableRepository<TEntity>, IIterableRepository<TEntity>, IRepository<TEntity>
    {
        Task<object> Add(TEntity entity);

        Task<object> Delete(TEntity entity);

        Task<object> Delete(object id);

        Task<TEntity> Get(object id);

        Task<long> SaveChanges();

        Task<object> Update(TEntity entity);
    }
}
