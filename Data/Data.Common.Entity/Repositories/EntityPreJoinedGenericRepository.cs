namespace ProcessingTools.Data.Common.Entity.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;
    using Models.Contracts;

    using ProcessingTools.Data.Common.Entity.Contracts;

    public class EntityPreJoinedGenericRepository<TContext, TEntity> : EntityGenericRepository<TContext, TEntity>, IEntityGenericRepository<TEntity>, IDisposable
        where TContext : DbContext
        where TEntity : class, IEntityWithPreJoinedFields
    {
        private IEnumerable<string> prejoinFields;

        public EntityPreJoinedGenericRepository(IDbContextProvider<TContext> contextProvider)
            : base(contextProvider)
        {
            var entity = Activator.CreateInstance<TEntity>();
            this.prejoinFields = entity.PreJoinFieldNames?.ToArray();
        }

        public override Task<IQueryable<TEntity>> All()
        {
            var query = this.DbSet.AsQueryable();

            if (this.prejoinFields != null)
            {
                foreach (var fieldName in this.prejoinFields)
                {
                    query = query.Include(fieldName);
                }
            }

            return Task.FromResult(query);
        }

        public override async Task<TEntity> FindFirst(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var query = await this.All();

            return await query.FirstOrDefaultAsync(filter);
        }

        public override async Task<T> FindFirst<T>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, T>> projection)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (projection == null)
            {
                throw new ArgumentNullException(nameof(projection));
            }

            var query = await this.All();

            return await query.Where(filter).Select(projection).FirstOrDefaultAsync();
        }
    }
}
