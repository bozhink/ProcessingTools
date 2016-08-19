namespace ProcessingTools.Data.Common.Entity.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Common.Validation;
    using ProcessingTools.Data.Common.Entity.Contracts;
    using ProcessingTools.Data.Common.Expressions;
    using ProcessingTools.Data.Common.Expressions.Contracts;

    public abstract class EntityRepository<TContext, TEntity> : IEntityRepository<TEntity>, IDisposable
        where TContext : DbContext
        where TEntity : class
    {
        public EntityRepository(IDbContextProvider<TContext> contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.Context = contextProvider.Create();
            this.DbSet = this.GetDbSet<TEntity>();
        }

        protected IDbSet<TEntity> DbSet { get; private set; }

        private TContext Context { get; set; }

        public virtual async Task<long> SaveChanges()
        {
            long result = await this.Context.SaveChangesAsync();
            return result;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Context.Dispose();
            }
        }

        protected DbEntityEntry<T> GetEntry<T>(T entity) where T : class => this.Context.Entry(entity);

        protected IDbSet<T> GetDbSet<T>()
            where T : class
        {
            return this.Context.Set<T>();
        }

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

        protected async Task<T> Update<T>(object id, IUpdateExpression<T> update, IDbSet<T> set) where T : class
        {
            DummyValidator.ValidateId(id);
            DummyValidator.ValidateUpdate(update);
            DummyValidator.ValidateSet(set);

            var entity = await this.Get(id, set);
            if (entity == null)
            {
                throw new EntityNotFoundException();
            }

            // TODO : Updater
            var updater = new Updater<T>(update);
            await updater.Invoke(entity);

            var entry = this.GetEntry(entity);
            if (entry.State == EntityState.Detached)
            {
                set.Attach(entity);
            }

            entry.State = EntityState.Modified;
            return entity;
        }

        protected async Task<T> Upsert<T>(T entity, IDbSet<T> set, Expression<Func<T, bool>> filter)
            where T : class
        {
            DummyValidator.ValidateEntity(entity);
            DummyValidator.ValidateSet(set);
            DummyValidator.ValidateFilter(filter);

            var dbmodel = await set.AsQueryable().FirstOrDefaultAsync(filter);
            if (dbmodel == null)
            {
                return await this.Add(entity, set);
            }
            else
            {
                return await this.Update(entity, set);
            }
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

        protected Task<T> Get<T>(object id, IDbSet<T> set) where T : class => Task.Run(() =>
        {
            DummyValidator.ValidateId(id);
            DummyValidator.ValidateSet(set);

            var entity = set.Find(id);
            return entity;
        });
    }
}
