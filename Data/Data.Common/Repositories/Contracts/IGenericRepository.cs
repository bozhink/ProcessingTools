namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IGenericRepository<TEntity> : ICountableRepository<TEntity>, ISearchableRepository<TEntity>
    {
        Task<object> Add(TEntity entity);

        Task<IQueryable<TEntity>> All();

        Task<object> Delete(TEntity entity);

        Task<object> Delete(object id);

        Task<TEntity> Get(object id);

        Task<int> SaveChanges();

        Task<object> Update(TEntity entity);
    }
}
