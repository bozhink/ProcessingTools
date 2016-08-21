namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Common.Validation;
    using ProcessingTools.Configurator;
    using ProcessingTools.Data.Common.Expressions;
    using ProcessingTools.Data.Common.Expressions.Contracts;

    public class XmlTaxonRankRepository : XmlTaxonRankSearchableRepository, IXmlTaxonRankRepository
    {
        public XmlTaxonRankRepository(ITaxaContextProvider contextProvider, Config config)
            : base(contextProvider, config)
        {
        }

        public virtual Task<object> Add(ITaxonRankEntity entity)
        {
            DummyValidator.ValidateEntity(entity);

            return this.Context.Add(entity);
        }

        public virtual Task<long> Count() => Task.FromResult(this.Context.DataSet.LongCount());

        public virtual Task<long> Count(Expression<Func<ITaxonRankEntity, bool>> filter) => Task.FromResult(this.Context.DataSet.LongCount(filter));

        public virtual Task<object> Delete(object id)
        {
            DummyValidator.ValidateId(id);

            return this.Context.Delete(id);
        }

        public virtual Task<object> Update(ITaxonRankEntity entity)
        {
            DummyValidator.ValidateEntity(entity);

            return this.Context.Update(entity);
        }

        public virtual Task<long> SaveChanges() => this.Context.WriteToFile(this.Config.RankListXmlFilePath);

        public virtual async Task<object> Update(object id, IUpdateExpression<ITaxonRankEntity> update)
        {
            DummyValidator.ValidateId(id);
            DummyValidator.ValidateUpdate(update);

            var entity = await this.Get(id);
            if (entity == null)
            {
                throw new EntityNotFoundException();
            }

            // TODO : Updater
            var updater = new Updater<ITaxonRankEntity>(update);
            await updater.Invoke(entity);

            return await this.Context.Update(entity);
        }
    }
}
