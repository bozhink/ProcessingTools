namespace ProcessingTools.Bio.Taxonomy.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Models;
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

        private ITaxaContext Context { get; set; }

        private Config Config { get; set; }

        public Task<object> Add(Taxon entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return this.Context.Add(entity);
        }

        public Task<IQueryable<Taxon>> All()
        {
            return this.Context.All();
        }

        public async Task<IQueryable<Taxon>> All(Expression<Func<Taxon, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return (await this.All()).Where(filter);
        }

        public async Task<IQueryable<Taxon>> All(Expression<Func<Taxon, object>> sort, int skip, int take)
        {
            if (sort == null)
            {
                throw new ArgumentNullException(nameof(sort));
            }

            if (skip < 0)
            {
                throw new ArgumentException(string.Empty, nameof(skip));
            }

            if (take < 1)
            {
                throw new ArgumentException(string.Empty, nameof(take));
            }

            return (await this.All())
                .OrderBy(sort)
                .Skip(skip)
                .Take(take);
        }

        public async Task<IQueryable<Taxon>> All(Expression<Func<Taxon, bool>> filter, Expression<Func<Taxon, object>> sort, int skip, int take)
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
                throw new ArgumentException(string.Empty, nameof(skip));
            }

            if (take < 1)
            {
                throw new ArgumentException(string.Empty, nameof(take));
            }

            return (await this.All())
                .Where(filter)
                .OrderBy(sort)
                .Skip(skip)
                .Take(take);
        }

        public Task<object> Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.Context.Delete(id);
        }

        public Task<object> Delete(Taxon entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return this.Delete(entity.Name);
        }

        public Task<Taxon> Get(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.Get(id);
        }

        public Task<int> SaveChanges()
        {
            return this.Context.WriteTaxa(this.Config.RankListXmlFilePath);
        }

        public Task<object> Update(Taxon entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return this.Context.Update(entity);
        }
    }
}