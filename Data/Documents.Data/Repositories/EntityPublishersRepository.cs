namespace ProcessingTools.Documents.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;
    using Models;

    using ProcessingTools.Common.Validation;
    using ProcessingTools.Documents.Data.Common.Models.Contracts;
    using ProcessingTools.Documents.Data.Contracts;

    public class EntityPublishersRepository : EntityAddressableRepository<Publisher>, IEntityPublishersRepository
    {
        public EntityPublishersRepository(IDocumentsDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }

        public async Task<object> Add(IPublisherEntity entity)
        {
            DummyValidator.ValidateEntity(entity);

            var dbmodel = new Publisher(entity);
            foreach (var entityAddress in entity.Addresses)
            {
                await this.AddAddressToDbModel(dbmodel, entityAddress);
            }

            return await this.Add(dbmodel, this.DbSet);
        }

        public Task<IQueryable<IPublisherEntity>> All() => Task.Run(() =>
        {
            var query = this.DbSet.AsQueryable<IPublisherEntity>();
            return query;
        });

        public Task<long> Count() => this.DbSet.LongCountAsync();

        public Task<long> Count(Expression<Func<IPublisherEntity, bool>> filter)
        {
            DummyValidator.ValidateFilter(filter);

            var query = this.DbSet.AsQueryable<IPublisherEntity>();
            return query.LongCountAsync(filter);
        }

        public async Task<object> Delete(object id)
        {
            DummyValidator.ValidateId(id);

            var entity = await this.Get(id, this.DbSet);
            if (entity == null)
            {
                return null;
            }

            entity.Addresses.Clear();
            var result = await this.Delete(entity, this.DbSet);
            return result;
        }

        public Task<object> Delete(IPublisherEntity entity)
        {
            DummyValidator.ValidateEntity(entity);
            return this.Delete(entity.Id);
        }

        public async Task<IPublisherEntity> Get(object id) => await this.Get(id, this.DbSet);

        public async Task<object> Update(IPublisherEntity entity)
        {
            DummyValidator.ValidateEntity(entity);

            var dbmodel = new Publisher(entity);

            return await this.Update(dbmodel, this.DbSet);
        }
    }
}
