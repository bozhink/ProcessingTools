namespace ProcessingTools.Data.Entity.Abstractions
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using ProcessingTools.Data.Contracts;

    public interface IEfRepository<TContext, TEntity> : IRepository<TEntity>
        where TContext : DbContext
        where TEntity : class
    {
        TContext Context { get; }

        DbSet<TEntity> DbSet { get; }

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
