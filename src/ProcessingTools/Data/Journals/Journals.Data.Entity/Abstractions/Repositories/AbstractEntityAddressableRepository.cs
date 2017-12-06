namespace ProcessingTools.Journals.Data.Entity.Abstractions.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Common.Entity.Abstractions.Repositories;
    using ProcessingTools.Data.Common.Entity.Contracts.Repositories;
    using ProcessingTools.Journals.Data.Entity.Contracts;
    using ProcessingTools.Journals.Data.Entity.Models;
    using ProcessingTools.Contracts.Models.Journals;

    public abstract class AbstractEntityAddressableRepository<TEntity, TDbModel> : AbstractEntityRepository<TEntity, IJournalsDbContext, TDbModel>
        where TEntity : class, IAddressable
        where TDbModel : Addressable, TEntity, ProcessingTools.Contracts.Models.IStringIdentifiable
    {
        protected AbstractEntityAddressableRepository(IGenericRepository<IJournalsDbContext, TDbModel> repository)
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
            dbaddress.Id = Guid.NewGuid().ToString();

            entity.Addresses.Add(dbaddress);

            this.Repository.Update(entity);

            return await Task.FromResult(entity).ConfigureAwait(false);
        }

        public virtual async Task<object> UpdateAddress(object entityId, IAddress address)
        {
            if (entityId == null)
            {
                throw new ArgumentNullException(nameof(entityId));
            }

            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            var query = this.Repository.DbSet.AsQueryable()
                .Where(e => e.Id == entityId.ToString())
                .Include(e => e.Addresses)
                .SelectMany(e => e.Addresses
                    .Where(a => a.Id == address.Id.ToString())
                    .OrderBy(a => a.Id)
                    .Skip(0)
                    .Take(1));

            var dbaddress = await query.FirstOrDefaultAsync().ConfigureAwait(false);
            if (dbaddress == null)
            {
                return null;
            }

            dbaddress.AddressString = address.AddressString;
            dbaddress.CityId = address.CityId;
            dbaddress.CountryId = address.CountryId;

            var entry = this.Repository.Context.Entry(dbaddress);
            if (entry.State == EntityState.Detached)
            {
                this.Repository.Context.Addresses.Attach(dbaddress);
            }

            entry.State = EntityState.Modified;

            return await Task.FromResult(dbaddress.Id).ConfigureAwait(false);
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
            var query = this.Repository.DbSet.AsQueryable()
                .Where(e => e.Id == entityId.ToString())
                .Include(e => e.Addresses)
                .SelectMany(e => e.Addresses
                    .Where(a => a.Id == addressId.ToString())
                    .OrderBy(a => a.Id)
                    .Skip(0)
                    .Take(1));

            var dbaddress = await query.FirstOrDefaultAsync().ConfigureAwait(false);
            if (dbaddress == null)
            {
                return null;
            }

            var entry = this.Repository.Context.Entry(dbaddress);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.Repository.Context.Addresses.Attach(dbaddress);
                this.Repository.Context.Addresses.Remove(dbaddress);
            }

            return addressId;
        }

        public override Task<object> DeleteAsync(object id)
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

            entity.Addresses
                .ToList()
                .ForEach(a => this.Repository.Context.Addresses.Remove(a));

            this.Repository.Delete(entity);
            return Task.FromResult(id);
        }
    }
}
