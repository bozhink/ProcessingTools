namespace ProcessingTools.Data.Common.File.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Expressions;
    using ProcessingTools.Data.Common.Expressions;
    using ProcessingTools.Data.Common.File.Contracts;
    using ProcessingTools.Data.Common.File.Contracts.Repositories;

    public abstract class FileGenericRepository<TContext, TEntity> : FileRepository<TContext, TEntity>, IFileGenericRepository<TEntity>, IFileCrudRepository<TEntity>
        where TContext : IFileDbContext<TEntity>
        where TEntity : class
    {
        public FileGenericRepository(IFactory<TContext> contextFactory)
            : base(contextFactory)
        {
        }

        public virtual Task<object> Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return this.Context.Add(entity);
        }

        public virtual Task<long> Count() => Task.FromResult(this.Context.DataSet.LongCount());

        public virtual Task<long> Count(Expression<Func<TEntity, bool>> filter) => Task.FromResult(this.Context.DataSet.LongCount(filter));

        public virtual Task<object> Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.Context.Delete(id);
        }

        public virtual Task<object> Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return this.Context.Update(entity);
        }

        public virtual async Task<object> Update(object id, IUpdateExpression<TEntity> update)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (update == null)
            {
                throw new ArgumentNullException(nameof(update));
            }

            var entity = await this.GetById(id);
            if (entity == null)
            {
                throw new EntityNotFoundException();
            }

            // TODO : Updater
            var updater = new Updater<TEntity>(update);
            await updater.Invoke(entity);

            return await this.Context.Update(entity);
        }
    }
}
