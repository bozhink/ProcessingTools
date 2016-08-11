namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Models;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Common.Types;
    using ProcessingTools.Configurator;

    public class TaxaRepository : ITaxaRepository
    {
        public TaxaRepository(ITaxaContextProvider contextProvider, Config config)
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

        private Config Config { get; set; }

        private ITaxaContext Context { get; set; }

        public virtual Task<object> Add(Taxon entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return this.Context.Add(entity);
        }

        public virtual Task<IQueryable<Taxon>> All()
        {
            return this.Context.All();
        }

        public virtual async Task<long> Count()
        {
            var count = (await this.Context.All())
                .LongCount();

            return count;
        }

        public virtual async Task<long> Count(Expression<Func<Taxon, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var count = (await this.Context.All())
                .Where(filter)
                .LongCount();

            return count;
        }

        public virtual Task<object> Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.Context.Delete(id);
        }

        public virtual Task<object> Delete(Taxon entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return this.Delete(entity.Name);
        }

        public virtual Task<Taxon> Get(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.Context.Get(id);
        }

        public virtual async Task<IQueryable<Taxon>> Find(
            Expression<Func<Taxon, bool>> filter,
            Expression<Func<Taxon, object>> sort,
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
            Expression<Func<Taxon, bool>> filter,
            Expression<Func<Taxon, T>> projection,
            Expression<Func<Taxon, object>> sort,
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

        public virtual async Task<Taxon> FindFirst(Expression<Func<Taxon, bool>> filter)
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

        public virtual async Task<T> FindFirst<T>(Expression<Func<Taxon, bool>> filter, Expression<Func<Taxon, T>> projection)
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

        public virtual Task<object> Update(Taxon entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return this.Context.Update(entity);
        }

        public virtual Task<int> SaveChanges() => this.Context.WriteTaxa(this.Config.RankListXmlFilePath);
    }
}