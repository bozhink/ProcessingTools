namespace ProcessingTools.Documents.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Models;

    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Data.Common.Entity.Repositories;
    using ProcessingTools.Documents.Data.Common.Models.Contracts;
    using ProcessingTools.Documents.Data.Common.Repositories.Contracts;
    using ProcessingTools.Documents.Data.Contracts;

    public abstract class EntityAddressableRepository<TDbModel, TEntity> : EntityCrudRepository<DocumentsDbContext, TDbModel, TEntity>, IAddressableRepository
        where TEntity : class, IAddressableEntity
        where TDbModel : class, TEntity, IAddressableEntity
    {
        public EntityAddressableRepository(IDocumentsDbContextProvider contextProvider)
            : base(contextProvider)
        {
            this.AddressSet = this.GetDbSet<Address>();
        }

        private IDbSet<Address> AddressSet { get; set; }

        public abstract Task<object> AddAddress(object entityId, IAddressEntity address);
        ////{
        ////    if (entityId == null)
        ////    {
        ////        throw new ArgumentNullException(nameof(entityId));
        ////    }

        ////    if (address == null)
        ////    {
        ////        throw new ArgumentNullException(nameof(address));
        ////    }

        ////    var dbmodel = await this.Get(entityId, this.DbSet);
        ////    if (dbmodel == null)
        ////    {
        ////        throw new EntityNotFoundException();
        ////    }

        ////    var dbaddress = await this.GetOrAddAddress(address);
        ////    dbmodel.Addresses.Add(dbaddress);

        ////    return dbmodel;
        ////}

        public abstract Task<object> RemoveAddress(object entityId, object addressId);
        ////{
        ////    if (entityId == null)
        ////    {
        ////        throw new ArgumentNullException(nameof(entityId));
        ////    }

        ////    if (addressId == null)
        ////    {
        ////        throw new ArgumentNullException(nameof(addressId));
        ////    }

        ////    Guid id;
        ////    if (!Guid.TryParse(addressId.ToString(), out id))
        ////    {
        ////        throw new ArgumentException(nameof(addressId));
        ////    }

        ////    var dbmodel = await this.Get(entityId, this.DbSet);
        ////    if (dbmodel == null)
        ////    {
        ////        throw new EntityNotFoundException();
        ////    }

        ////    return this.RemoveAddressFromDbModel(dbmodel, id);
        ////}

        protected virtual async Task<Address> GetOrAddAddress(IAddressEntity address)
        {
            var dbaddress = await this.AddOrGet(
                new Address(address),
                this.AddressSet,
                t => (t.AddressString == address.AddressString) && (t.CountryId == address.CountryId) && (t.CityId == address.CityId));

            return dbaddress;
        }

        protected virtual Task<object> RemoveAddressFromDbModel(TDbModel dbmodel, Guid addressId) => Task.Run<object>(() =>
        {
            var addressToBeRemoved = dbmodel.Addresses.Cast<Address>().FirstOrDefault(a => a.Id == addressId);
            if (addressToBeRemoved == null)
            {
                return null;
            }

            var numberOfReferences = addressToBeRemoved.Affiliations.Count + addressToBeRemoved.Institutions.Count + addressToBeRemoved.Publishers.Count;
            if (numberOfReferences < 2)
            {
                this.AddressSet.Remove(addressToBeRemoved);
            }

            dbmodel.Addresses.Remove(addressToBeRemoved);

            return dbmodel;
        });
    }
}
