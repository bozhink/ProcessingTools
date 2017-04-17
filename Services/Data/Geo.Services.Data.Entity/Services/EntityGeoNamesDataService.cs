namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Data.Geo.Filters;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;
    using ProcessingTools.Geo.Data.Entity.Contracts;
    using ProcessingTools.Geo.Data.Entity.Models;
    using ProcessingTools.Geo.Services.Data.Entity.Abstractions;
    using ProcessingTools.Geo.Services.Data.Entity.Contracts.Services;

    public class EntityGeoNamesDataService : AbstractGeoDataService<GeoName, IGeoName, IGeoNamesFilter>, IEntityGeoNamesDataService
    {
        public EntityGeoNamesDataService(IGeoRepository<GeoName> repository, IEnvironment environment)
            : base(repository, environment)
        {
        }

        protected override Func<GeoName, IGeoName> MapEntityToModel => m => new ProcessingTools.Geo.Services.Data.Entity.Models.GeoName
        {
            Id = m.Id,
            Name = m.Name
        };

        protected override Func<IGeoName, GeoName> MapModelToEntity => m => new GeoName
        {
            Id = m.Id,
            Name = m.Name
        };

        public override async Task<object> UpdateAsync(IGeoName model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var entity = this.Repository.Get(model.Id);
            if (entity == null)
            {
                return null;
            }

            entity.Name = model.Name;

            return await base.UpdateEntityAsync(entity);
        }

        protected override IQueryable<GeoName> GetQuery(IGeoNamesFilter filter)
        {
            var query = this.Repository.Queryable();

            if (filter != null)
            {
                query = query.Where(
                    n =>
                        (!filter.Id.HasValue || n.Id == filter.Id) &&
                        (string.IsNullOrEmpty(filter.Name) || n.Name.ToLower().Contains(filter.Name.ToLower())));
            }

            return query;
        }
    }
}
