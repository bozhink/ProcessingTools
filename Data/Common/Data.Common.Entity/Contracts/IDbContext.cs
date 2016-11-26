namespace ProcessingTools.Data.Common.Entity.Contracts
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Threading.Tasks;

    public interface IDbContext : IDisposable
    {
        DbEntityEntry<T> Entry<T>(T entity)
            where T : class;

        int SaveChanges();

        Task<int> SaveChangesAsync();

        IDbSet<T> Set<T>()
            where T : class;
    }
}
