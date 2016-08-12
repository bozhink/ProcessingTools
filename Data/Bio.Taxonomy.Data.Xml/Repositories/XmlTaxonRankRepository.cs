namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Models;
    using ProcessingTools.Configurator;

    public class XmlTaxonRankRepository : XmlTaxonRankSearchableRepository, IXmlTaxonRankRepository
    {
        public XmlTaxonRankRepository(ITaxaContextProvider contextProvider, Config config)
            : base(contextProvider, config)
        {
        }

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
