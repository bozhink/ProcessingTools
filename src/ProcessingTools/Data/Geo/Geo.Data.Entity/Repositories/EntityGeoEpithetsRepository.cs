namespace ProcessingTools.Geo.Data.Entity.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Extensions.Linq;
    using ProcessingTools.Contracts.Data.Repositories.Geo;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions.Linq;
    using ProcessingTools.Geo.Data.Entity.Abstractions.Repositories;
    using ProcessingTools.Geo.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Geo.Data.Entity.Models;

    public class EntityGeoEpithetsRepository : AbstractGeoRepository<GeoEpithet, IGeoEpithet, ITextFilter>, IGeoEpithetsRepository
    {
        private readonly Func<GeoEpithet, IGeoEpithet> mapEntityToModel;

        public EntityGeoEpithetsRepository(IGeoRepository<GeoEpithet> repository, IEnvironment environment)
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

        private Expression<Func<GeoEpithet, IGeoEpithet>> MapEntityToModelExpression => m => new GeoEpithetModel
        {
            Id = m.Id,
            Name = m.Name
        };

        public override async Task<IGeoEpithet[]> SelectAsync(ITextFilter filter)
        {
            var query = this.GetQuery(filter)
                .Select(this.MapEntityToModelExpression);

            return await query.ToArrayAsync().ConfigureAwait(false);
        }

        public override async Task<IGeoEpithet[]> SelectAsync(ITextFilter filter, int skip, int take, string sortColumn, SortOrder sortOrder = SortOrder.Ascending)
        {
            var query = this.GetQuery(filter)
                .OrderByName(sortColumn, sortOrder)
                .Skip(skip)
                .Take(take)
                .Select(this.MapEntityToModelExpression);

            return await query.ToArrayAsync().ConfigureAwait(false);
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

            return await this.UpdateEntityAsync(entity).ConfigureAwait(false);
        }

        protected override IQueryable<GeoEpithet> GetQuery(ITextFilter filter)
        {
            var query = this.Repository.Queryable();

            if (filter != null)
            {
                query = query.Where(
                    n =>
                        (string.IsNullOrEmpty(filter.Text) || n.Name.ToLower().Contains(filter.Text.ToLower())));
            }

            return query;
        }
    }
}
