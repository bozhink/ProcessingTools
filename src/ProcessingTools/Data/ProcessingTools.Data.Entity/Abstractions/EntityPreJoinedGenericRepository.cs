namespace ProcessingTools.Data.Entity.Abstractions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class EntityPreJoinedGenericRepository<TContext, TEntity> : EntityGenericRepository<TContext, TEntity>, IEntityGenericRepository<TEntity>
        where TContext : DbContext
        where TEntity : class, IEntityWithPreJoinedFields
    {
        private readonly IEnumerable<string> prejoinFields;

        public EntityPreJoinedGenericRepository(IDbContextProvider<TContext> contextProvider)
            : base(contextProvider)
        {
            var entity = Activator.CreateInstance<TEntity>();
            this.prejoinFields = entity.PreJoinFieldNames?.ToArray();
        }

        public override IQueryable<TEntity> Query
        {
            get
            {
                var query = this.DbSet.AsQueryable();

                if (this.prejoinFields != null)
                {
                    foreach (var fieldName in this.prejoinFields)
                    {
                        query = query.Include(fieldName);
                    }
                }

                return query;
            }
        }

        public override Task<TEntity> FindFirstAsync(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return this.Query.FirstOrDefaultAsync(filter);
        }
    }
}
