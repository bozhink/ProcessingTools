namespace ProcessingTools.Data.Common.File.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Common.File.Contracts;
    using ProcessingTools.Data.Common.File.Contracts.Repositories;

    public class FileRepository<TContext, TEntity> : IFileRepository<TEntity>, IFileSearchableRepository<TEntity>, IFileIterableRepository<TEntity>
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

        // TODO
        public virtual Task<IEnumerable<TEntity>> Find(
            Expression<Func<TEntity, bool>> filter) => Task.Run(() =>
            {
                if (filter == null)
                {
                    throw new ArgumentNullException(nameof(filter));
                }

                var query = this.Context.DataSet;
                query = query.Where(filter);
                return query.AsEnumerable();
            });

        public virtual Task<TEntity> FindFirst(
            Expression<Func<TEntity, bool>> filter) => Task.Run(() =>
            {
                if (filter == null)
                {
                    throw new ArgumentNullException(nameof(filter));
                }

                var entity = this.Context.DataSet.FirstOrDefault(filter);
                return entity;
            });

        public virtual Task<TEntity> GetById(object id)
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
