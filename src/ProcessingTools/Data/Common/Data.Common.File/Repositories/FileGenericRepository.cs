namespace ProcessingTools.Data.Common.File.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Data.Expressions;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Data.Expressions;
    using ProcessingTools.Data.Common.File.Contracts;
    using ProcessingTools.Exceptions;

    public abstract class FileGenericRepository<TContext, TEntity> : IFileGenericRepository<TEntity>
        where TContext : IFileDbContext<TEntity>
        where TEntity : class
    {
        protected FileGenericRepository(IFactory<TContext> contextFactory)
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

            return this.Context.GetAsync(id);
        }

        public virtual Task<object> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return this.Context.AddAsync(entity);
        }

        public virtual Task<long> CountAsync() => Task.FromResult(this.Context.DataSet.LongCount());

        public virtual Task<long> CountAsync(Expression<Func<TEntity, bool>> filter) => Task.FromResult(this.Context.DataSet.LongCount(filter));

        public virtual Task<object> DeleteAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.Context.DeleteAsync(id);
        }

        public virtual Task<object> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return this.Context.UpdateAsync(entity);
        }

        public virtual async Task<object> UpdateAsync(object id, IUpdateExpression<TEntity> updateExpression)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (updateExpression == null)
            {
                throw new ArgumentNullException(nameof(updateExpression));
            }

            var entity = await this.GetByIdAsync(id).ConfigureAwait(false);
            if (entity == null)
            {
                throw new EntityNotFoundException();
            }

            // TODO : Updater
            var updater = new Updater<TEntity>(updateExpression);
            updater.Invoke(entity);

            return await this.Context.UpdateAsync(entity).ConfigureAwait(false);
        }

        public virtual object SaveChanges() => 0;

        public virtual Task<object> SaveChangesAsync() => Task.FromResult(this.SaveChanges());
    }
}
