namespace ProcessingTools.Data.Common.Entity.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Common.Validation;
    using ProcessingTools.Data.Common.Entity.Contracts;

    public class EntityCrudRepository<TContext, TEntity> : EntityRepository<TContext, TEntity>, IEntityCrudRepository<TEntity>, IDisposable
        where TContext : DbContext
        where TEntity : class
    {
        public EntityCrudRepository(IDbContextProvider<TContext> contextProvider)
            : base(contextProvider)
        {
        }

        public virtual Task<object> Add(TEntity entity) => Task.Run<object>(() =>
        {
            DummyValidator.ValidateEntity(entity);

            var entry = this.GetEntry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
                return entity;
            }
            else
            {
                return this.DbSet.Add(entity);
            }
        });

        public virtual Task<object> Delete(TEntity entity) => Task.Run<object>(() =>
        {
            DummyValidator.ValidateEntity(entity);

            var entry = this.GetEntry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
                return entity;
            }
            else
            {
                this.DbSet.Attach(entity);
                return this.DbSet.Remove(entity);
            }
        });

        public virtual async Task<object> Delete(object id)
        {
            DummyValidator.ValidateId(id);

            var entity = await this.Get(id);
            if (entity == null)
            {
                return null;
            }

            return await this.Delete(entity);
        }

        public virtual Task<TEntity> Get(object id) => Task.Run(() =>
        {
            DummyValidator.ValidateId(id);

            return this.DbSet.Find(id);
        });

        public virtual Task<object> Update(TEntity entity) => Task.Run<object>(() =>
        {
            DummyValidator.ValidateEntity(entity);

            var entry = this.GetEntry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
            return entity;
        });
    }
}
