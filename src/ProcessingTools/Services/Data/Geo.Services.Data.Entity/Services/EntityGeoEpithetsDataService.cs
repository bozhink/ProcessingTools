namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Extensions.Linq;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Data.Geo.Filters;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Geo.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Geo.Data.Entity.Models;
    using ProcessingTools.Geo.Services.Data.Entity.Abstractions;
    using ProcessingTools.Geo.Services.Data.Entity.Contracts.Services;

    public class EntityGeoEpithetsDataService : AbstractGeoDataService<GeoEpithet, IGeoEpithet, IGeoEpithetsFilter>, IEntityGeoEpithetsDataService
    {
        private readonly Func<GeoEpithet, IGeoEpithet> mapEntityToModel;

        public EntityGeoEpithetsDataService(IGeoRepository<GeoEpithet> repository, IEnvironment environment)
            : base(repository, environment)
        {
            this.mapEntityToModel = this.MapEntityToModelExpression.Compile();
        }

        protected override Func<GeoEpithet, IGeoEpithet> MapEntityToModel => this.mapEntityToModel;

        protected override Func<IGeoEpithet, GeoEpithet> MapModelToEntity => m => new GeoEpithet
        {
            Id = m.Id,
            Name = m.Name
        };

        private Expression<Func<GeoEpithet, IGeoEpithet>> MapEntityToModelExpression => m => new ProcessingTools.Geo.Services.Data.Entity.Models.GeoEpithet
        {
            Id = m.Id,
            Name = m.Name
        };

        public override async Task<IGeoEpithet[]> SelectAsync(IGeoEpithetsFilter filter)
        {
            var query = this.GetQuery(filter)
                .Select(this.MapEntityToModelExpression);

            return await query.ToArrayAsync();
        }

        public override async Task<IGeoEpithet[]> SelectAsync(IGeoEpithetsFilter filter, int skip, int take, string sortColumn, SortOrder sortOrder = SortOrder.Ascending)
        {
            var query = this.GetQuery(filter)
                .OrderByName(sortColumn, sortOrder)
                .Skip(skip)
                .Take(take)
                .Select(this.MapEntityToModelExpression);

            return await query.ToArrayAsync();
        }

        public override async Task<object> UpdateAsync(IGeoEpithet model)
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

            return await this.UpdateEntityAsync(entity);
        }

        protected override IQueryable<GeoEpithet> GetQuery(IGeoEpithetsFilter filter)
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
