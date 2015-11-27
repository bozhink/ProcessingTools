namespace ProcessingTools.Data.Common.Contracts
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDbContext : IDisposable
    {
        DbEntityEntry Entry(object entity);

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();

        Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbSet Set(Type entityType);

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}