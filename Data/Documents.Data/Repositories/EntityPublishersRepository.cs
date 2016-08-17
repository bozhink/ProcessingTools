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
    using ProcessingTools.Data.Common.Entity.Repositories;
    using ProcessingTools.Documents.Data.Common.Models.Contracts;
    using ProcessingTools.Documents.Data.Contracts;

    public class EntityPublishersRepository : EntityRepository<DocumentsDbContext, Publisher>, IEntityPublishersRepository
    {
        public EntityPublishersRepository(IDocumentsDbContextProvider contextProvider)
            : base(contextProvider)
        {
            this.AddressSet = this.GetDbSet<Address>();
        }

        private IDbSet<Address> AddressSet { get; set; }

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

        public async Task<object> AddAddress(object entityId, IAddressEntity address)
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

            await this.AddAddressToDbModel(dbmodel, address);

            return dbmodel;
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

        public async Task<object> Delete(object id) => await this.Delete(id, this.DbSet);

        public Task<object> Delete(IPublisherEntity entity)
        {
            DummyValidator.ValidateEntity(entity);
            return this.Delete(entity.Id);
        }

        public async Task<IPublisherEntity> Get(object id) => await this.Get(id, this.DbSet);

        public async Task<object> RemoveAddress(object entityId, object addressId)
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

            return this.RemoveAddressFromDbModel(dbmodel, id);
        }

        public async Task<object> Update(IPublisherEntity entity)
        {
            DummyValidator.ValidateEntity(entity);

            var dbmodel = new Publisher(entity);

            return await this.Update(dbmodel, this.DbSet);
        }

        private async Task AddAddressToDbModel(Publisher dbmodel, IAddressEntity address)
        {
            var dbaddress = await this.Upsert(
                new Address(address),
                this.AddressSet,
                t => (t.AddressString == address.AddressString) && (t.CountryId == address.CountryId) && (t.CityId == address.CityId));

            dbmodel.Addresses.Add(dbaddress);
        }

        private Task<object> RemoveAddressFromDbModel(Publisher dbmodel, Guid addressId) => Task.Run<object>(() =>
        {
            var addredToBeRemoved = dbmodel.Addresses.FirstOrDefault(a => a.Id == addressId);
            if (addredToBeRemoved == null)
            {
                return null;
            }

            var numberOfReferences = addredToBeRemoved.Affiliations.Count + addredToBeRemoved.Institutions.Count + addredToBeRemoved.Publishers.Count;
            if (numberOfReferences < 2)
            {
                this.AddressSet.Remove(addredToBeRemoved);
            }

            dbmodel.Addresses.Remove(addredToBeRemoved);

            return dbmodel;
        });
    }
}
