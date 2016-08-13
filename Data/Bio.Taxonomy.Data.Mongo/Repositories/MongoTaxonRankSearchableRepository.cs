namespace ProcessingTools.Bio.Taxonomy.Data.Mongo.Repositories
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;
    using Models;

    using MongoDB.Driver;

    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Contracts;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Common.Types;
    using ProcessingTools.Data.Common.Mongo.Repositories;

    public class MongoTaxonRankSearchableRepository : MongoRepository<MongoTaxonRankEntity>, IMongoTaxonRankSearchableRepository
    {
        private const string BiotaxonomyTaxaMongoCollectionNameKey = "BiotaxonomyTaxaMongoCollectionName";

        public MongoTaxonRankSearchableRepository(IBiotaxonomyMongoDatabaseProvider provider)
            : base(provider)
        {
            this.CollectionName = ConfigurationManager.AppSettings[BiotaxonomyTaxaMongoCollectionNameKey];
        }

        public virtual Task<IQueryable<ITaxonRankEntity>> Find(
            Expression<Func<ITaxonRankEntity, bool>> filter,
            Expression<Func<ITaxonRankEntity, object>> sort,
            SortOrder sortOrder = SortOrder.Ascending,
            int skip = 0,
            int take = PagingConstants.DefaultNumberOfTopItemsToSelect)
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

            var query = this.Collection.AsQueryable().Where(filter);

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

        public virtual async Task<IQueryable<T>> Find<T>(
            Expression<Func<ITaxonRankEntity, bool>> filter,
            Expression<Func<ITaxonRankEntity, T>> projection,
            Expression<Func<ITaxonRankEntity, object>> sort,
            SortOrder sortOrder = SortOrder.Ascending,
            int skip = 0,
            int take = PagingConstants.DefaultNumberOfTopItemsToSelect)
        {
            if (projection == null)
            {
                throw new ArgumentNullException(nameof(projection));
            }

            return (await this.Find(filter, sort, sortOrder, skip, take))
                .Select(projection);
        }

        public virtual Task<ITaxonRankEntity> FindFirst(Expression<Func<ITaxonRankEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return Task.Run(() =>
            {
                var entity = this.Collection.AsQueryable()
                    .FirstOrDefault(filter);

                if (entity == null)
                {
                    throw new EntityNotFoundException();
                }

                return entity;
            });
        }

        public virtual Task<T> FindFirst<T>(Expression<Func<ITaxonRankEntity, bool>> filter, Expression<Func<ITaxonRankEntity, T>> projection)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (projection == null)
            {
                throw new ArgumentNullException(nameof(projection));
            }

            return Task.Run(() =>
            {
                var entity = this.Collection.AsQueryable()
                    .Where(filter)
                    .Select(projection)
                    .FirstOrDefault();

                if (entity == null)
                {
                    throw new EntityNotFoundException();
                }

                return entity;
            });
        }
    }
}
