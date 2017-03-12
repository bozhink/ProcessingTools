namespace ProcessingTools.Journals.Data.Entity.Abstractions.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Models;
    using ProcessingTools.Data.Common.Entity.Abstractions.Repositories;
    using ProcessingTools.Data.Common.Entity.Contracts.Repositories;
    using ProcessingTools.Journals.Data.Common.Contracts.Models;

    public abstract class AbstractEntityAddressableRepository<TEntity, TDbModel> : AbstractEntityRepository<TEntity, IJournalsDbContext, TDbModel>
        where TEntity : class, IAddressable
        where TDbModel : Addressable, TEntity, ProcessingTools.Contracts.Models.IStringIdentifiable
    {
        public AbstractEntityAddressableRepository(IGenericRepository<IJournalsDbContext, TDbModel> repository)
            : base(repository)
        {
        }

        protected virtual Func<IAddress, Address> MapAddressToAddress => a => new Address
        {
            Id = a.Id,
            AddressString = a.AddressString,
            CityId = a.CityId,
            CountryId = a.CountryId
        };

        public virtual async Task<object> AddAddress(object entityId, IAddress address)
        {
            if (entityId == null)
            {
                throw new ArgumentNullException(nameof(entityId));
            }

            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            var entity = this.Repository.Get(entityId);
            if (entity == null)
            {
                return null;
            }

            var dbaddress = this.MapAddressToAddress(address);

            entity.Addresses.Add(dbaddress);

            this.Repository.Update(entity);

            return await Task.FromResult(entity);
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

            // This code is for validation that the entity with entityId contains
            // an address with addressId
            var address = await this.Repository.DbSet.AsQueryable()
                .Where(e => e.Id == entityId.ToString())
                .Include(e => e.Addresses)
                .SelectMany(e => e.Addresses
                    .Where(a => a.Id == addressId.ToString())
                    .Skip(0)
                    .Take(1))
                .FirstOrDefaultAsync();

            if (address == null)
            {
                return null;
            }

            var entry = this.Repository.Context.Entry(address);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.Repository.Context.Addresses.Attach(address);
                this.Repository.Context.Addresses.Remove(address);
            }

            return addressId;
        }

        public override async Task<object> Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = this.Repository.Get(id);
            if (entity == null)
            {
                return null;
            }

            await entity.Addresses
                .AsQueryable()
                .ForEachAsync(a => this.Repository.Context.Addresses.Remove(a));

            this.Repository.Delete(entity);
            return id;
        }
    }
}
