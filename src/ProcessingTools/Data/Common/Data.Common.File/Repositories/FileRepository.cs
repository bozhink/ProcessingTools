namespace ProcessingTools.Data.Common.File.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Common.File.Contracts;

    public class FileRepository<TContext, TEntity> : IFileSearchableRepository<TEntity>, IFileIterableRepository<TEntity>
        where TContext : IFileDbContext<TEntity>
    {
        public FileRepository(IFactory<TContext> contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.Context = contextFactory.Create();
        }

        public virtual IEnumerable<TEntity> Entities => this.Context.DataSet;

        public virtual IQueryable<TEntity> Query => this.Context.DataSet;

        protected virtual TContext Context { get; private set; }

        public virtual Task<TEntity[]> FindAsync(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return Task.Run(() =>
            {
                var query = this.Context.DataSet.Where(filter);
                var data = query.ToArray();
                return data;
            });
        }

        public virtual Task<TEntity> FindFirstAsync(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return Task.Run(() => this.Context.DataSet.FirstOrDefault(filter));
        }

        public virtual Task<TEntity> GetByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.Context.Get(id);
        }

        public virtual object SaveChanges() => 0;

        public virtual Task<object> SaveChangesAsync() => Task.FromResult(this.SaveChanges());
    }
}
