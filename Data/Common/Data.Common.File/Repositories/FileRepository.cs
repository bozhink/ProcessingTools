namespace ProcessingTools.Data.Common.File.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Repositories;
    using ProcessingTools.Common.Validation;
    using ProcessingTools.Contracts;

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
                DummyValidator.ValidateFilter(filter);

                var query = this.Context.DataSet;
                query = query.Where(filter);
                return query.AsEnumerable();
            });

        public virtual Task<TEntity> FindFirst(
            Expression<Func<TEntity, bool>> filter) => Task.Run(() =>
            {
                DummyValidator.ValidateFilter(filter);

                var entity = this.Context.DataSet.FirstOrDefault(filter);
                return entity;
            });

        public virtual Task<TEntity> Get(object id)
        {
            DummyValidator.ValidateId(id);

            return this.Context.Get(id);
        }
    }
}
