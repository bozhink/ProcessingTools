namespace ProcessingTools.Data.Common.Entity.Contracts.Repositories
{
    using System.Data.Entity;
    using System.Linq;
    using ProcessingTools.Data.Contracts.Repositories;
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IGenericRepository<TContext, TEntity> : IRepository<TEntity>
        where TContext : IDbContext
        where TEntity : class
    {
        TContext Context { get; }

        IDbSet<TEntity> DbSet { get; }

        void Add(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entity);

        void Detach(TEntity entity);

        TEntity Get(object id);

        IQueryable<TEntity> Queryable();

        IQueryable<T> Queryable<T>()
            where T : class;

        void Update(TEntity entity);
    }
}
