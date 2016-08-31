namespace ProcessingTools.Documents.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;
    using Models;

    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Common.Validation;
    using ProcessingTools.Documents.Data.Common.Models.Contracts;
    using ProcessingTools.Documents.Data.Contracts;

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
                var dbaddress = await this.GetOrAddAddress(entityAddress);
                dbmodel.Addresses.Add(dbaddress);
            }

            return await this.Add(dbmodel, this.DbSet);
        }

        public override async Task<object> AddAddress(object entityId, IAddressEntity address)
        {
            if (entityId == null)
            {
                throw new ArgumentNullException(nameof(entityId));
            }

            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            var dbmodel = await this.Get(entityId, this.DbSet);
            if (dbmodel == null)
            {
                throw new EntityNotFoundException();
            }

            var dbaddress = await this.GetOrAddAddress(address);
            dbmodel.Addresses.Add(dbaddress);

            return dbmodel;
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

        public override async Task<object> RemoveAddress(object entityId, object addressId)
        {
            if (entityId == null)
            {
                throw new ArgumentNullException(nameof(entityId));
            }

            if (addressId == null)
            {
                throw new ArgumentNullException(nameof(addressId));
            }

            Guid id;
            if (!Guid.TryParse(addressId.ToString(), out id))
            {
                throw new ArgumentException(nameof(addressId));
            }

            var dbmodel = await this.Get(entityId, this.DbSet);
            if (dbmodel == null)
            {
                throw new EntityNotFoundException();
            }

            // TODO
            return this.RemoveAddressFromDbModel(dbmodel, id);
        }
    }
}
