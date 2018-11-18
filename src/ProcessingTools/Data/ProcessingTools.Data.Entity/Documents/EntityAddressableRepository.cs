namespace ProcessingTools.Data.Entity.Documents
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Data.Contracts.Documents;
    using ProcessingTools.Data.Entity.Abstractions;
    using ProcessingTools.Data.Models.Entity.Documents;
    using ProcessingTools.Models.Contracts.Documents;

    public abstract class EntityAddressableRepository<TDbModel, TEntity> : EntityRepository<DocumentsDbContext, TDbModel, TEntity>, IAddressableRepository
        where TEntity : class, IAddressable
        where TDbModel : AddressableEntity, TEntity
    {
        protected EntityAddressableRepository(IDbContextProvider<DocumentsDbContext> contextProvider)
            : base(contextProvider)
        {
            this.AddressSet = this.GetDbSet<Address>();
        }

        private DbSet<Address> AddressSet { get; set; }

        public virtual async Task<object> AddAddressAsync(object entityId, IAddress address)
        {
            if (entityId == null)
            {
                throw new ArgumentNullException(nameof(entityId));
            }

            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            var dbmodel = await this.GetAsync(entityId, this.DbSet).ConfigureAwait(false);
            if (dbmodel == null)
            {
                throw new EntityNotFoundException();
            }

            var dbaddress = await this.AddOrGetAddressAsync(address).ConfigureAwait(false);
            dbmodel.Addresses.Add(dbaddress);

            return dbmodel;
        }

        public virtual async Task<object> RemoveAddressAsync(object entityId, object addressId)
        {
            if (entityId == null)
            {
                throw new ArgumentNullException(nameof(entityId));
            }

            if (addressId == null)
            {
                throw new ArgumentNullException(nameof(addressId));
            }

            if (!Guid.TryParse(addressId.ToString(), out Guid addressIdAsGuid))
            {
                throw new ArgumentException($"Parameter '{nameof(addressId)}' should be valid GUID", nameof(addressId));
            }

            var dbmodel = await this.GetAsync(entityId, this.DbSet).ConfigureAwait(false);
            if (dbmodel == null)
            {
                throw new EntityNotFoundException();
            }

            return this.RemoveAddressFromDbModelAsync(dbmodel, addressIdAsGuid);
        }

        protected virtual async Task<Address> AddOrGetAddressAsync(IAddress address)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            var dbaddress = await this.AddOrGetAsync(
                new Address(address),
                this.AddressSet,
                t => (t.AddressString == address.AddressString) &&
                     (t.CountryId == address.CountryId) &&
                     (t.CityId == address.CityId))
                .ConfigureAwait(false);

            return dbaddress;
        }

        protected virtual Task<object> RemoveAddressFromDbModelAsync(TDbModel dbmodel, Guid addressId)
        {
            if (dbmodel == null)
            {
                throw new ArgumentNullException(nameof(dbmodel));
            }

            return Task.Run<object>(() =>
            {
                var addressToBeRemoved = dbmodel.Addresses.FirstOrDefault(a => a.Id == addressId);
                if (addressToBeRemoved == null)
                {
                    return null;
                }

                this.RemoveAddressFromAddressSetIfOneOrLessEntityReferencesIt(addressToBeRemoved);

                dbmodel.Addresses.Remove(addressToBeRemoved);

                return dbmodel;
            });
        }

        private async Task<T> AddOrGetAsync<T>(T entity, DbSet<T> set, Expression<Func<T, bool>> filter)
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

            var dbmodel = await set.AsQueryable().FirstOrDefaultAsync(filter).ConfigureAwait(false);
            if (dbmodel == null)
            {
                var result = await this.AddAsync(entity, set).ConfigureAwait(false);
                await this.SaveChangesAsync().ConfigureAwait(false);

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
