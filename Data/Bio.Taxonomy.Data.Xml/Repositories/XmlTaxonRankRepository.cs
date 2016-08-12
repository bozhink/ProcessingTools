namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;
    using ProcessingTools.Configurator;

    public class XmlTaxonRankRepository : XmlTaxonRankSearchableRepository, IXmlTaxonRankRepository
    {
        public XmlTaxonRankRepository(ITaxaContextProvider contextProvider, Config config)
            : base(contextProvider, config)
        {
        }

        public virtual Task<object> Add(ITaxonRankEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return this.Context.Add(entity);
        }

        public virtual Task<IQueryable<ITaxonRankEntity>> All() => this.Context.All();

        public virtual async Task<long> Count()
        {
            var count = (await this.Context.All())
                .LongCount();

            return count;
        }

        public virtual async Task<long> Count(Expression<Func<ITaxonRankEntity, bool>> filter)
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

        public virtual Task<object> Delete(ITaxonRankEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return this.Delete(entity.Name);
        }

        public virtual Task<ITaxonRankEntity> Get(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.Context.Get(id);
        }

        public virtual Task<object> Update(ITaxonRankEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return this.Context.Update(entity);
        }

        public virtual Task<long> SaveChanges() => this.Context.WriteTaxa(this.Config.RankListXmlFilePath);
    }
}
