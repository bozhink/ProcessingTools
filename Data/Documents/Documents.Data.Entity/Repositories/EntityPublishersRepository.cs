namespace ProcessingTools.Documents.Data.Entity.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Repositories;
    using Models;
    using ProcessingTools.Common.Validation;
    using ProcessingTools.Documents.Data.Common.Contracts.Models;

    public class EntityPublishersRepository : EntityAddressableRepository<Publisher, IPublisherEntity>, IEntityPublishersRepository
    {
        public EntityPublishersRepository(IDocumentsDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }

        protected override Func<IPublisherEntity, Publisher> MapEntityToDbModel => e => new Publisher(e);

        public override async Task<object> Add(IPublisherEntity entity)
        {
            DummyValidator.ValidateEntity(entity);

            var dbmodel = new Publisher(entity);
            foreach (var entityAddress in entity.Addresses)
            {
                var dbaddress = await this.AddOrGetAddress(entityAddress);
                dbmodel.Addresses.Add(dbaddress);
            }

            return await this.Add(dbmodel, this.DbSet);
        }

        public virtual Task<long> Count() => this.DbSet.LongCountAsync();

        public virtual Task<long> Count(Expression<Func<IPublisherEntity, bool>> filter)
        {
            DummyValidator.ValidateFilter(filter);

            var query = this.DbSet.AsQueryable<IPublisherEntity>();
            return query.LongCountAsync(filter);
        }

        public override async Task<IPublisherEntity> Get(object id)
        {
            DummyValidator.ValidateId(id);

            var query = this.DbSet
                .Include(p => p.Addresses)
                .Where(p => p.Id.ToString() == id.ToString());

            var entity = await query.FirstOrDefaultAsync();

            return entity;
        }
    }
}
