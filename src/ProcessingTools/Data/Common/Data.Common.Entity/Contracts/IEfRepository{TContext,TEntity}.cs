namespace ProcessingTools.Data.Common.Entity.Contracts
{
    using System.Data.Entity;
    using System.Linq;
    using ProcessingTools.Data.Contracts;

    public interface IEfRepository<TContext, TEntity> : IRepository<TEntity>
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
