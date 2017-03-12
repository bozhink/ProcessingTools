namespace ProcessingTools.Data.Common.Entity.Contracts.Repositories
{
    using System.Data.Entity;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IGenericRepository<TContext, T> : IRepository<T>
        where TContext : IDbContext
        where T : class
    {
        TContext Context { get; }

        IDbSet<T> DbSet { get; }

        void Add(T entity);

        void Delete(object id);

        void Delete(T entity);

        void Detach(T entity);

        T Get(object id);

        void Update(T entity);
    }
}
