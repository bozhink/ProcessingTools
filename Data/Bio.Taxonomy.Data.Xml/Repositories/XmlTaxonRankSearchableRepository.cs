namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Common.Types;
    using ProcessingTools.Configurator;

    public class XmlTaxonRankSearchableRepository : IXmlTaxonRankSearchableRepository
    {
        public XmlTaxonRankSearchableRepository(ITaxaContextProvider contextProvider, Config config)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            this.Config = config;

            this.Context = contextProvider.Create();
            this.Context.LoadTaxa(this.Config.RankListXmlFilePath).Wait();
        }

        protected Config Config { get; private set; }

        protected ITaxaContext Context { get; private set; }

        public virtual async Task<IQueryable<ITaxonRankEntity>> Find(
            Expression<Func<ITaxonRankEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var query = await this.Context.All();

            query = query.Where(filter);

            return query;
        }

        public virtual async Task<IQueryable<T>> Find<T>(
            Expression<Func<ITaxonRankEntity, bool>> filter,
            Expression<Func<ITaxonRankEntity, T>> projection)
        {
            if (projection == null)
            {
                throw new ArgumentNullException(nameof(projection));
            }

            return (await this.Find(filter))
                .Select(projection);
        }

        public virtual async Task<IQueryable<ITaxonRankEntity>> Find(
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

            var query = await this.Context.All();

            query = query.Where(filter);

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

        public virtual async Task<ITaxonRankEntity> FindFirst(Expression<Func<ITaxonRankEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var entity = (await this.Context.All())
                .FirstOrDefault(filter);

            if (entity == null)
            {
                throw new EntityNotFoundException();
            }

            return entity;
        }

        public virtual async Task<T> FindFirst<T>(Expression<Func<ITaxonRankEntity, bool>> filter, Expression<Func<ITaxonRankEntity, T>> projection)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (projection == null)
            {
                throw new ArgumentNullException(nameof(projection));
            }

            var entity = (await this.Context.All())
                .Where(filter)
                .Select(projection)
                .FirstOrDefault();

            if (entity == null)
            {
                throw new EntityNotFoundException();
            }

            return entity;
        }
    }
}
