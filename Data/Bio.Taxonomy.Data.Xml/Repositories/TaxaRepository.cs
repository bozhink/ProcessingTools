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

        public virtual async Task<Taxon> Get(Expression<Func<Taxon, bool>> selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return (await this.Context.All())
                .FirstOrDefault(selector);
        }

        public virtual Task<Taxon> Get(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.Context.Get(id);
        }

        public virtual async Task<IQueryable<Taxon>> Query(
            Expression<Func<Taxon, bool>> filter,
            Expression<Func<Taxon, object>> sort,
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

        public virtual async Task<IQueryable<T>> Query<T>(
            Expression<Func<Taxon, bool>> filter,
            Expression<Func<Taxon, T>> projection,
            Expression<Func<Taxon, object>> sort,
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