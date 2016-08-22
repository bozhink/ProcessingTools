namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;
    using Models;

    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Common.Types;
    using ProcessingTools.Common.Validation;
    using ProcessingTools.Configurator;
    using ProcessingTools.Data.Common.Expressions;
    using ProcessingTools.Data.Common.Expressions.Contracts;

    public class XmlBiotaxonomicBlackListRepository : IXmlBiotaxonomicBlackListRepository
    {
        public XmlBiotaxonomicBlackListRepository(IXmlBiotaxonomicBlackListContextProvider contextProvider, Config config)
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
        }

        private Config Config { get; set; }

        private IXmlBiotaxonomicBlackListContext Context { get; set; }

        public virtual async Task<object> Add(IBlackListEntity entity)
        {
            if (entity == null || string.IsNullOrWhiteSpace(entity.Content))
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var result = await this.Context.Add(entity);

            return result;
        }

        public Task<IQueryable<IBlackListEntity>> All() => Task.FromResult(this.Context.DataSet);

        public virtual async Task<object> Delete(object id)
        {
            if (id == null || string.IsNullOrWhiteSpace(id.ToString()))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var result = await this.Context.Delete(id);

            return result;
        }

        public Task<IQueryable<IBlackListEntity>> Find(Expression<Func<IBlackListEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<IBlackListEntity>> Find(Expression<Func<IBlackListEntity, bool>> filter, Expression<Func<IBlackListEntity, object>> sort, SortOrder sortOrder = SortOrder.Ascending, int skip = 0, int take = 10)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Tout>> Find<Tout>(Expression<Func<IBlackListEntity, bool>> filter, Expression<Func<IBlackListEntity, Tout>> projection)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Tout>> Find<Tout>(Expression<Func<IBlackListEntity, bool>> filter, Expression<Func<IBlackListEntity, Tout>> projection, Expression<Func<IBlackListEntity, object>> sort, SortOrder sortOrder = SortOrder.Ascending, int skip = 0, int take = 10)
        {
            throw new NotImplementedException();
        }

        public Task<IBlackListEntity> FindFirst(Expression<Func<IBlackListEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<Tout> FindFirst<Tout>(Expression<Func<IBlackListEntity, bool>> filter, Expression<Func<IBlackListEntity, Tout>> projection)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IBlackListEntity> Get(object id)
        {
            if (id == null || string.IsNullOrWhiteSpace(id.ToString()))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var query = await this.All();

            var result = query.FirstOrDefault(s => s.Content == id.ToString());

            return result;
        }

        public virtual Task<long> SaveChanges() => this.Context.WriteToFile(this.Config.BlackListXmlFilePath);

        public virtual Task<object> Update(IBlackListEntity entity) => this.Add(entity);

        public virtual async Task<object> Update(object id, IUpdateExpression<IBlackListEntity> update)
        {
            DummyValidator.ValidateId(id);
            DummyValidator.ValidateUpdate(update);

            var entity = await this.Get(id);
            if (entity == null)
            {
                throw new EntityNotFoundException();
            }

            await this.Delete(entity);

            // TODO : Updater
            var updater = new Updater<IBlackListEntity>(update);
            await updater.Invoke(entity);

            return await this.Add(entity);
        }
    }
}
