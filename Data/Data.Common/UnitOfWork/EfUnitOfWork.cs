namespace ProcessingTools.Data.Common.UnitOfWork
{
    using System.Collections.Concurrent;

    using ProcessingTools.Data.Common.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;
    using ProcessingTools.Data.Common.Repositories.Factories;
    using ProcessingTools.Data.Common.UnitOfWork.Contracts;

    public class EfUnitOfWork<IContext> : IEfUnitOfWork
        where IContext : IDbContext
    {
        private ConcurrentDictionary<string, object> repos;

        public EfUnitOfWork(IContext context)
        {
            this.DbContext = context;
            this.repos = new ConcurrentDictionary<string, object>();
        }

        public IContext DbContext { get; private set; }

        public IEfRepository<T> Get<T>()
            where T : class
        {
            var key = typeof(T).FullName;
            return (IEfRepository<T>)this.repos.GetOrAdd(key, k => new EfGenericRepository<IContext, T>(this.DbContext));
        }

        public int SaveChanges()
        {
            return this.DbContext.SaveChanges();
        }
    }
}
