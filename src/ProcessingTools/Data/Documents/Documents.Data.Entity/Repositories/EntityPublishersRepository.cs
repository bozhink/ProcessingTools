namespace ProcessingTools.Documents.Data.Entity.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Documents;
    using ProcessingTools.Documents.Data.Entity.Contracts;
    using ProcessingTools.Documents.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Documents.Data.Entity.Models;

    public class EntityPublishersRepository : EntityAddressableRepository<Publisher, IPublisher>, IEntityPublishersRepository
    {
        public EntityPublishersRepository(IDocumentsDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }

        protected override Func<IPublisher, Publisher> MapEntityToDbModel => e => new Publisher(e);

        public override async Task<object> AddAsync(IPublisher entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dbmodel = new Publisher(entity);
            foreach (var entityAddress in entity.Addresses)
            {
                var dbaddress = await this.AddOrGetAddressAsync(entityAddress).ConfigureAwait(false);
                dbmodel.Addresses.Add(dbaddress);
            }

            return await this.AddAsync(dbmodel, this.DbSet).ConfigureAwait(false);
        }

        public override Task<long> CountAsync() => this.DbSet.LongCountAsync();

        public override Task<long> CountAsync(Expression<Func<IPublisher, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var query = this.DbSet.AsQueryable<IPublisher>();
            return query.LongCountAsync(filter);
        }

        public override async Task<IPublisher> GetByIdAsync(object id)
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
