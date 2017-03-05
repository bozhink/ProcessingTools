namespace ProcessingTools.Data.Common.File.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Repositories;
    using ProcessingTools.Common.Validation;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Expressions;
    using ProcessingTools.Data.Common.Expressions;
    using ProcessingTools.Exceptions;

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
            DummyValidator.ValidateEntity(entity);
            return this.Context.Add(entity);
        }

        public virtual Task<long> Count() => Task.FromResult(this.Context.DataSet.LongCount());

        public virtual Task<long> Count(Expression<Func<TEntity, bool>> filter) => Task.FromResult(this.Context.DataSet.LongCount(filter));

        public virtual Task<object> Delete(object id)
        {
            DummyValidator.ValidateId(id);
            return this.Context.Delete(id);
        }

        public abstract Task<long> SaveChanges();

        public virtual Task<object> Update(TEntity entity)
        {
            DummyValidator.ValidateEntity(entity);
            return this.Context.Update(entity);
        }

        public virtual async Task<object> Update(object id, IUpdateExpression<TEntity> update)
        {
            DummyValidator.ValidateId(id);
            DummyValidator.ValidateUpdate(update);

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
