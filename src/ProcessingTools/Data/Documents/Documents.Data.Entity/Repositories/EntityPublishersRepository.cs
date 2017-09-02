namespace ProcessingTools.Documents.Data.Entity.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Data.Documents.Models;
    using ProcessingTools.Documents.Data.Entity.Contracts;
    using ProcessingTools.Documents.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Documents.Data.Entity.Models;

    public class EntityPublishersRepository : EntityAddressableRepository<Publisher, IPublisherEntity>, IEntityPublishersRepository
    {
        public EntityPublishersRepository(IDocumentsDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }

        protected override Func<IPublisherEntity, Publisher> MapEntityToDbModel => e => new Publisher(e);

        public override async Task<object> AddAsync(IPublisherEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dbmodel = new Publisher(entity);
            foreach (var entityAddress in entity.Addresses)
            {
                var dbaddress = await this.AddOrGetAddressAsync(entityAddress);
                dbmodel.Addresses.Add(dbaddress);
            }

            return await this.AddAsync(dbmodel, this.DbSet);
        }

        public override Task<long> CountAsync() => this.DbSet.LongCountAsync();

        public override Task<long> CountAsync(Expression<Func<IPublisherEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var query = this.DbSet.AsQueryable<IPublisherEntity>();
            return query.LongCountAsync(filter);
        }

        public override async Task<IPublisherEntity> GetByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var query = this.DbSet
                .Include(p => p.Addresses)
                .Where(p => p.Id.ToString() == id.ToString());

            var entity = await query.FirstOrDefaultAsync().ConfigureAwait(false);

            return entity;
        }
    }
}
