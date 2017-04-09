namespace ProcessingTools.Data.Common.Entity.Abstractions.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Repositories;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Contracts.Expressions;
    using ProcessingTools.Data.Common.Expressions;

    public abstract class AbstractEntityRepository<TEntity, TContext, TDbModel> : ICrudRepository<TEntity>
        where TEntity : class
        where TContext : IDbContext
        where TDbModel : class, TEntity
    {
        private readonly IGenericRepository<TContext, TDbModel> repository;

        public AbstractEntityRepository(IGenericRepository<TContext, TDbModel> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            this.repository = repository;
        }

        protected abstract Func<TEntity, TDbModel> MapEntityToDbModel { get; }

        protected IGenericRepository<TContext, TDbModel> Repository => repository;

        public virtual IQueryable<TEntity> Query => this.repository.DbSet.AsQueryable<TEntity>();

        public virtual Task<object> Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var model = this.MapEntityToDbModel(entity);

            this.repository.Add(model);

            return Task.FromResult<object>(model);
        }

        public virtual async Task<long> Count()
        {
            var count = await this.repository.DbSet.LongCountAsync();
            return count;
        }

        public virtual async Task<long> Count(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var count = await this.repository.DbSet.LongCountAsync(filter);
            return count;
        }

        public virtual Task<object> Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            this.repository.Delete(id);
            return Task.FromResult(id);
        }

        public virtual Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var query = this.repository.DbSet.Where(filter);

            return Task.FromResult(query.AsEnumerable());
        }

        public virtual async Task<TEntity> FindFirst(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var entity = await this.repository.DbSet.FirstOrDefaultAsync(filter);

            return entity;
        }

        public virtual Task<TEntity> GetById(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = this.repository.Get(id);
            return Task.FromResult<TEntity>(entity);
        }

        public virtual object SaveChanges() => this.repository.Context.SaveChanges();

        public virtual async Task<object> SaveChangesAsync() => await this.repository.Context.SaveChangesAsync();

        public virtual Task<object> Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var model = this.MapEntityToDbModel(entity);

            this.repository.Update(model);

            return Task.FromResult<object>(model);
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

            var model = this.repository.Get(id);
            if (model == null)
            {
                return null;
            }

            // TODO : Updater
            var updater = new Updater<TEntity>(update);
            await updater.Invoke(model);

            this.repository.Update(model);

            return Task.FromResult<object>(model);
        }
    }
}
