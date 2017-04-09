namespace ProcessingTools.Documents.Data.Entity.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Common.Entity.Repositories;
    using ProcessingTools.Documents.Data.Common.Contracts.Models;
    using ProcessingTools.Documents.Data.Common.Contracts.Repositories;
    using ProcessingTools.Documents.Data.Entity.Contracts;
    using ProcessingTools.Documents.Data.Entity.Models;
    using ProcessingTools.Exceptions;

    public abstract class EntityAddressableRepository<TDbModel, TEntity> : EntityRepository<DocumentsDbContext, TDbModel, TEntity>, IAddressableRepository
        where TEntity : class, IAddressableEntity
        where TDbModel : AddressableEntity, TEntity
    {
        public EntityAddressableRepository(IDocumentsDbContextProvider contextProvider)
            : base(contextProvider)
        {
            this.AddressSet = this.GetDbSet<Address>();
        }

        private IDbSet<Address> AddressSet { get; set; }

        public virtual async Task<object> AddAddress(object entityId, IAddressEntity address)
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

            var dbaddress = await this.AddOrGetAddress(address);
            dbmodel.Addresses.Add(dbaddress);

            return dbmodel;
        }

        public virtual async Task<object> RemoveAddress(object entityId, object addressId)
        {
            if (entityId == null)
            {
                throw new ArgumentNullException(nameof(entityId));
            }

            if (addressId == null)
            {
                throw new ArgumentNullException(nameof(addressId));
            }

            Guid addressIdAsGuid;
            if (!Guid.TryParse(addressId.ToString(), out addressIdAsGuid))
            {
                throw new ArgumentException($"Parameter '{nameof(addressId)}' should be valid GUID", nameof(addressId));
            }

            var dbmodel = await this.Get(entityId, this.DbSet);
            if (dbmodel == null)
            {
                throw new EntityNotFoundException();
            }

            return this.RemoveAddressFromDbModel(dbmodel, addressIdAsGuid);
        }

        protected virtual async Task<Address> AddOrGetAddress(IAddressEntity address)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            var dbaddress = await this.AddOrGet(
                new Address(address),
                this.AddressSet,
                t => (t.AddressString == address.AddressString) &&
                     (t.CountryId == address.CountryId) &&
                     (t.CityId == address.CityId));

            return dbaddress;
        }

        protected virtual Task<object> RemoveAddressFromDbModel(TDbModel dbmodel, Guid addressId) => Task.Run<object>(() =>
        {
            if (dbmodel == null)
            {
                throw new ArgumentNullException(nameof(dbmodel));
            }

            var addressToBeRemoved = dbmodel.Addresses.FirstOrDefault(a => a.Id == addressId);
            if (addressToBeRemoved == null)
            {
                return null;
            }

            this.RemoveAddressFromAddressSetIfOneOrLessEntityReferencesIt(addressToBeRemoved);

            dbmodel.Addresses.Remove(addressToBeRemoved);

            return dbmodel;
        });

        private async Task<T> AddOrGet<T>(T entity, IDbSet<T> set, Expression<Func<T, bool>> filter)
            where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (set == null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var dbmodel = await set.AsQueryable().FirstOrDefaultAsync(filter);
            if (dbmodel == null)
            {
                var result = await this.Add(entity, set);
                await this.SaveChangesAsync();

                return result;
            }

            return dbmodel;
        }

        private void RemoveAddressFromAddressSetIfOneOrLessEntityReferencesIt(Address addressToBeRemoved)
        {
            var numberOfReferences = addressToBeRemoved.Affiliations.Count +
                                     addressToBeRemoved.Institutions.Count +
                                     addressToBeRemoved.Publishers.Count;

            if (numberOfReferences <= 1)
            {
                this.AddressSet.Remove(addressToBeRemoved);
            }
        }
    }
}
