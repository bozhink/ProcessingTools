namespace ProcessingTools.Data.Common.File.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Repositories;
    using ProcessingTools.Common.Validation;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Enumerations;

    public class FileRepository<TContext, TEntity> : IFileRepository<TEntity>, IFileSearchableRepository<TEntity>, IFileIterableRepository<TEntity>
        where TContext : IFileDbContext<TEntity>
    {
        public FileRepository(IFactory<TContext> contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.Context = contextFactory.Create();
        }

        public virtual IEnumerable<TEntity> Entities => this.Context.DataSet;

        public virtual IQueryable<TEntity> Query => this.Context.DataSet;

        protected virtual TContext Context { get; private set; }

        public virtual Task<IQueryable<TEntity>> Find(
            Expression<Func<TEntity, bool>> filter) => Task.Run(() =>
            {
                DummyValidator.ValidateFilter(filter);

                var query = this.Context.DataSet;
                query = query.Where(filter);
                return query;
            });

        public virtual Task<IQueryable<TEntity>> Find(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, object>> sort,
            SortOrder sortOrder = SortOrder.Ascending,
            int skip = 0,
            int take = PagingConstants.DefaultNumberOfTopItemsToSelect) => Task.Run(() =>
            {
                DummyValidator.ValidateFilter(filter);
                DummyValidator.ValidateSort(sort);
                DummyValidator.ValidateSkip(skip);
                DummyValidator.ValidateTake(take);

                var query = this.Context.DataSet.Where(filter);

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

                return query;
            });

        public virtual Task<TEntity> FindFirst(
            Expression<Func<TEntity, bool>> filter) => Task.Run(() =>
            {
                DummyValidator.ValidateFilter(filter);

                var entity = this.Context.DataSet.FirstOrDefault(filter);
                return entity;
            });

        public virtual Task<T> FindFirst<T>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, T>> projection) => Task.Run(() =>
            {
                DummyValidator.ValidateFilter(filter);
                DummyValidator.ValidateProjection(projection);

                var entity = this.Context.DataSet
                    .Where(filter)
                    .Select(projection)
                    .FirstOrDefault();
                return entity;
            });

        public virtual Task<TEntity> Get(object id)
        {
            DummyValidator.ValidateId(id);

            return this.Context.Get(id);
        }
    }
}
