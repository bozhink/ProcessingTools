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
    using ProcessingTools.Common.Validation;
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

        public override async Task<TEntity> FindFirst(Expression<Func<TEntity, bool>> filter)
        {
            DummyValidator.ValidateFilter(filter);
            return await this.Query.FirstOrDefaultAsync(filter);
        }

        public override async Task<T> FindFirst<T>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, T>> projection)
        {
            DummyValidator.ValidateFilter(filter);
            DummyValidator.ValidateProjection(projection);
            return await this.Query.Where(filter).Select(projection).FirstOrDefaultAsync();
        }
    }
}
