namespace ProcessingTools.Data.Entity.Abstractions
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using ProcessingTools.Common.Code.Data.Expressions;
    using ProcessingTools.Contracts.Data.Expressions;
    using ProcessingTools.Data.Contracts;

    public abstract class AbstractEntityRepository<TEntity, TContext, TDbModel> : ICrudRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
        where TDbModel : class, TEntity
    {
        protected AbstractEntityRepository(IEfRepository<TContext, TDbModel> repository)
        {
            this.Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public virtual IQueryable<TEntity> Query => this.Repository.DbSet.AsQueryable<TEntity>();

        protected abstract Func<TEntity, TDbModel> MapEntityToDbModel { get; }

        protected IEfRepository<TContext, TDbModel> Repository { get; }

        public virtual Task<object> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var model = this.MapEntityToDbModel(entity);

            this.Repository.Add(model);

            return Task.FromResult<object>(model);
        }

        public virtual Task<long> CountAsync()
        {
            return this.Repository.DbSet.LongCountAsync();
        }

        public virtual Task<long> CountAsync(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return this.Repository.DbSet.LongCountAsync(filter);
        }

        public virtual Task<object> DeleteAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            this.Repository.Delete(id);
            return Task.FromResult(id);
        }

        public virtual async Task<TEntity[]> FindAsync(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var query = this.Repository.DbSet.Where(filter);
            var data = await query.ToArrayAsync().ConfigureAwait(false);
            return data;
        }

        public virtual Task<TEntity> FindFirstAsync(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return this.Repository.DbSet.FirstOrDefaultAsync(filter);
        }

        public virtual Task<TEntity> GetByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = this.Repository.Get(id);
            return Task.FromResult<TEntity>(entity);
        }

        public virtual object SaveChanges() => this.Repository.Context.SaveChanges();

        public virtual async Task<object> SaveChangesAsync() => await this.Repository.Context.SaveChangesAsync().ConfigureAwait(false);

        public virtual Task<object> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var model = this.MapEntityToDbModel(entity);

            this.Repository.Update(model);

            return Task.FromResult<object>(model);
        }

        public virtual Task<object> UpdateAsync(object id, IUpdateExpression<TEntity> updateExpression)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (updateExpression == null)
            {
                throw new ArgumentNullException(nameof(updateExpression));
            }

            var model = this.Repository.Get(id);
            if (model == null)
            {
                return Task.FromResult<object>(null);
            }

            // TODO : Updater
            var updater = new Updater<TEntity>(updateExpression);
            updater.Invoke(model);

            this.Repository.Update(model);

            return Task.FromResult<object>(model);
        }
    }
}
