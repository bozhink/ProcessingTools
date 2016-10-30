namespace ProcessingTools.Contracts.Data.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGenericContextRepository<TContext, TEntity> : IRepository<TEntity>
    {
        Task<object> Add(TContext context, TEntity entity);

        IEnumerable<TEntity> All(TContext context);

        IEnumerable<TEntity> All(TContext context, int skip, int take);

        Task<object> Delete(TContext context);

        Task<object> Delete(TContext context, object id);

        Task<object> Delete(TContext context, TEntity entity);

        Task<TEntity> Get(TContext context, object id);

        Task<long> SaveChanges(TContext context);

        Task<object> Update(TContext context, TEntity entity);
    }
}
