namespace ProcessingTools.Data.Common.File.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Common.Expressions;
    using ProcessingTools.Data.Common.File.Contracts;
    using ProcessingTools.Data.Common.File.Contracts.Repositories;
    using ProcessingTools.Exceptions;

    public abstract class FileGenericRepository<TContext, TEntity> : FileRepository<TContext, TEntity>, IFileGenericRepository<TEntity>
        where TContext : IFileDbContext<TEntity>
        where TEntity : class
    {
        protected FileGenericRepository(IFactory<TContext> contextFactory)
            : base(contextFactory)
        {
        }

        public virtual Task<object> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return this.Context.Add(entity);
        }

        public virtual Task<long> CountAsync() => Task.FromResult(this.Context.DataSet.LongCount());

        public virtual Task<long> CountAsync(Expression<Func<TEntity, bool>> filter) => Task.FromResult(this.Context.DataSet.LongCount(filter));

        public virtual Task<object> DeleteAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.Context.Delete(id);
        }

        public virtual Task<object> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return this.Context.Update(entity);
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
            await updater.Invoke(entity).ConfigureAwait(false);

            return await this.Context.Update(entity).ConfigureAwait(false);
        }
    }
}
