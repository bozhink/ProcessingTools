namespace ProcessingTools.Data.Common.Entity.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Contracts;
    using ProcessingTools.Common.Validation;
    using ProcessingTools.Contracts.Expressions;
    using ProcessingTools.Data.Common.Entity.Contracts;
    using ProcessingTools.Data.Common.Expressions;

    public abstract class EntityCrudRepository<TContext, TDbModel, TEntity> : EntitySearchableRepository<TContext, TDbModel, TEntity>, IEntityCrudRepository<TEntity>, IDisposable
        where TContext : DbContext
        where TEntity : class
        where TDbModel : class, TEntity
    {
        public EntityCrudRepository(IDbContextProvider<TContext> contextProvider)
            : base(contextProvider)
        {
        }

        protected abstract Func<TEntity, TDbModel> MapEntityToDbModel { get; }

        public virtual async Task<object> Add(TEntity entity)
        {
            DummyValidator.ValidateEntity(entity);
            var dbmodel = this.MapEntityToDbModel.Invoke(entity);
            return await this.Add(dbmodel, this.DbSet);
        }

        public virtual async Task<long> Count()
        {
            var count = await this.DbSet.LongCountAsync();
            return count;
        }

        public virtual async Task<long> Count(Expression<Func<TEntity, bool>> filter)
        {
            DummyValidator.ValidateFilter(filter);

            var count = await this.DbSet.Where(filter).LongCountAsync();
            return count;
        }

        public virtual async Task<object> Delete(object id) => await this.Delete(id, this.DbSet);

        public virtual async Task<object> Update(TEntity entity)
        {
            DummyValidator.ValidateEntity(entity);
            var dbmodel = this.MapEntityToDbModel.Invoke(entity);
            return await this.Update(dbmodel, this.DbSet);
        }

        public virtual async Task<object> Update(object id, IUpdateExpression<TEntity> update) => await this.Update(id, update, this.DbSet);

        protected Task<T> Add<T>(T entity, IDbSet<T> set) where T : class => Task.Run(() =>
        {
            DummyValidator.ValidateEntity(entity);
            DummyValidator.ValidateSet(set);

            var entry = this.GetEntry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
                return entity;
            }
            else
            {
                return set.Add(entity);
            }
        });

        protected async Task<T> AddOrGet<T>(T entity, IDbSet<T> set, Expression<Func<T, bool>> filter)
            where T : class
        {
            DummyValidator.ValidateEntity(entity);
            DummyValidator.ValidateSet(set);
            DummyValidator.ValidateFilter(filter);

            var dbmodel = await set.AsQueryable().FirstOrDefaultAsync(filter);
            if (dbmodel == null)
            {
                var result = await this.Add(entity, set);
                await this.SaveChangesAsync();

                return result;
            }

            return dbmodel;
        }

        protected Task<T> Delete<T>(T entity, IDbSet<T> set) where T : class => Task.Run(() =>
                {
                    DummyValidator.ValidateEntity(entity);
                    DummyValidator.ValidateSet(set);

                    var entry = this.GetEntry(entity);
                    if (entry.State != EntityState.Deleted)
                    {
                        entry.State = EntityState.Deleted;
                        return entity;
                    }
                    else
                    {
                        set.Attach(entity);
                        return set.Remove(entity);
                    }
                });

        protected async Task<T> Delete<T>(object id, IDbSet<T> set)
            where T : class
        {
            DummyValidator.ValidateId(id);
            DummyValidator.ValidateSet(set);

            var entity = await this.Get(id, set);
            if (entity == null)
            {
                return null;
            }

            return await this.Delete(entity, set);
        }

        protected Task<T> Update<T>(T entity, IDbSet<T> set) where T : class => Task.Run(() =>
        {
            DummyValidator.ValidateEntity(entity);
            DummyValidator.ValidateSet(set);

            var entry = this.GetEntry(entity);
            if (entry.State == EntityState.Detached)
            {
                set.Attach(entity);
            }

            entry.State = EntityState.Modified;
            return entity;
        });

        protected async Task<Tout> Update<Tin, Tout>(object id, IUpdateExpression<Tin> update, IDbSet<Tout> set)
            where Tin : class
            where Tout : class, Tin
        {
            DummyValidator.ValidateId(id);
            DummyValidator.ValidateUpdate(update);
            DummyValidator.ValidateSet(set);

            var entity = await this.Get(id, set);
            if (entity == null)
            {
                return null;
            }

            // TODO : Updater
            var updater = new Updater<Tin>(update);
            await updater.Invoke(entity);

            return await this.Update(entity, set);
        }
    }
}
