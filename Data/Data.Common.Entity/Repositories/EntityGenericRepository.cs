namespace ProcessingTools.Data.Common.Entity.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Common.Types;
    using ProcessingTools.Data.Common.Entity.Contracts;

    public class EntityGenericRepository<TContext, TEntity> : IEntityGenericRepository<TEntity>, IDisposable
        where TContext : DbContext
        where TEntity : class
    {
        public EntityGenericRepository(IDbContextProvider<TContext> contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.Context = contextProvider.Create();
            this.DbSet = this.Context.Set<TEntity>();
        }

        protected IDbSet<TEntity> DbSet { get; set; }

        protected TContext Context { get; set; }

        public virtual Task<IQueryable<TEntity>> All()
        {
            return Task.FromResult(this.DbSet.AsQueryable());
        }

        public virtual Task<IQueryable<TEntity>> Query(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, object>> sort,
            int skip = 0,
            int take = PagingConstants.DefaultNumberOfTopItemsToSelect,
            SortOrder sortOrder = SortOrder.Ascending)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (sort == null)
            {
                throw new ArgumentNullException(nameof(sort));
            }

            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (1 > take || take > PagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException();
            }

            IQueryable<TEntity> query = this.DbSet.Where(filter);

            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    query = query.OrderBy(sort);
                    break;

                case SortOrder.Descending:
                    query = query.OrderByDescending(sort);
                    break;

                default:
                    throw new NotImplementedException();
            }

            query = query.Skip(skip).Take(take);

            return Task.FromResult(query);
        }

        public virtual async Task<IQueryable<T>> Query<T>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, T>> projection,
            Expression<Func<TEntity, object>> sort,
            int skip = 0,
            int take = PagingConstants.DefaultNumberOfTopItemsToSelect,
            SortOrder sortOrder = SortOrder.Ascending)
        {
            if (projection == null)
            {
                throw new ArgumentNullException(nameof(projection));
            }

            return (await this.Query(filter, sort, skip, take, sortOrder))
                .Select(projection);
        }

        public virtual Task<TEntity> Get(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return Task.FromResult(this.DbSet.Find(id));
        }

        public virtual Task<object> Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return Task.Run<object>(() =>
            {
                var entry = this.Context.Entry(entity);
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
        }

        public virtual Task<object> Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return Task.Run<object>(() =>
            {
                var entry = this.Context.Entry(entity);
                if (entry.State == EntityState.Detached)
                {
                    this.DbSet.Attach(entity);
                }

                entry.State = EntityState.Modified;
                return entity;
            });
        }

        public virtual Task<object> Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return Task.Run<object>(() =>
            {
                var entry = this.Context.Entry(entity);
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
        }

        public virtual async Task<object> Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = await this.Get(id);
            if (entity == null)
            {
                return null;
            }

            return await this.Delete(entity);
        }

        public virtual Task<int> SaveChanges()
        {
            return this.Context.SaveChangesAsync();
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
    }
}