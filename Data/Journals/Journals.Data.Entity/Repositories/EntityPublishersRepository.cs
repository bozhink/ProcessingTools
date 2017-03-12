namespace ProcessingTools.Journals.Data.Entity.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Repositories;
    using Models;
    using ProcessingTools.Data.Common.Entity.Abstractions.Repositories;
    using ProcessingTools.Data.Common.Entity.Contracts.Repositories;
    using ProcessingTools.Journals.Data.Common.Contracts.Models;

    public class EntityPublishersRepository : AbstractEntityRepository<IPublisher, IJournalsDbContext, Publisher>, IEntityPublishersRepository
    {
        public EntityPublishersRepository(IGenericRepository<IJournalsDbContext, Publisher> repository) : base(repository)
        {
        }

        protected override Func<IPublisher, Publisher> MapEntityToDbModel => p => new Publisher
        {
            Id = p.Id,
            Name = p.Name,
            AbbreviatedName = p.AbbreviatedName,
            CreatedByUser = p.CreatedByUser,
            ModifiedByUser = p.ModifiedByUser,
            DateCreated = p.DateCreated,
            DateModified = p.DateModified,
            Addresses = p.Addresses.Select(this.MapAddressToAddress).ToList()
        };

        private Func<IAddress, Address> MapAddressToAddress => a => new Address
        {
            Id = a.Id,
            AddressString = a.AddressString,
            CityId = a.CityId,
            CountryId = a.CountryId,
            CreatedByUser = a.CreatedByUser,
            ModifiedByUser = a.ModifiedByUser,
            DateCreated = a.DateCreated,
            DateModified = a.DateModified
        };

        public async Task<object> AddAddress(object entityId, IAddress address)
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

            var dbaddress = await this.Repository.Context.Addresses
                .FirstOrDefaultAsync(a => a.AddressString.ToLower() == address.AddressString.ToLower() &&
                    a.CityId == address.CityId &&
                    a.CountryId == address.CountryId);

            if (dbaddress == null)
            {
                dbaddress = this.MapAddressToAddress(address);
            }

            entity.Addresses.Add(dbaddress);

            this.Repository.Update(entity);

            return entity;
        }

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

            // This code is for validation that the entity with entityId contains
            // an address with addressId
            var address = await this.Repository.DbSet.AsQueryable()
                .Where(e => e.Id.ToString() == entityId.ToString())
                .Include(e => e.Addresses)
                .SelectMany(e => e.Addresses
                    .Where(a => a.Id.ToString() == addressId.ToString())
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
    }
}
