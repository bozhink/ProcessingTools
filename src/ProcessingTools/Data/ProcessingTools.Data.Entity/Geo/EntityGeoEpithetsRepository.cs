﻿namespace ProcessingTools.Data.Entity.Geo
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Data.Models.Entity.Geo;
    using ProcessingTools.Extensions.Linq;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Geo;

    public class EntityGeoEpithetsRepository : AbstractGeoRepository<GeoEpithet, IGeoEpithet, ITextFilter>, IGeoEpithetsRepository
    {
        private readonly Func<GeoEpithet, IGeoEpithet> mapEntityToModel;

        public EntityGeoEpithetsRepository(IGeoRepository<GeoEpithet> repository, IApplicationContext applicationContext)
            : base(repository, applicationContext)
        {
            this.mapEntityToModel = this.MapEntityToModelExpression.Compile();
        }

        /// <inheritdoc/>
        protected override Func<GeoEpithet, IGeoEpithet> MapEntityToModel => this.mapEntityToModel;

        /// <inheritdoc/>
        protected override Func<IGeoEpithet, GeoEpithet> MapModelToEntity => m => new GeoEpithet
        {
            Id = m.Id,
            Name = m.Name,
        };

        private Expression<Func<GeoEpithet, IGeoEpithet>> MapEntityToModelExpression => m => new GeoEpithetModel
        {
            Id = m.Id,
            Name = m.Name,
        };

        /// <inheritdoc/>
        public override async Task<IGeoEpithet[]> SelectAsync(ITextFilter filter)
        {
            var query = this.GetQuery(filter)
                .Select(this.MapEntityToModelExpression);

            return await query.ToArrayAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public override async Task<IGeoEpithet[]> SelectAsync(ITextFilter filter, int skip, int take, string sortColumn, SortOrder sortOrder)
        {
            var query = this.GetQuery(filter)
                .OrderByName(sortColumn, sortOrder)
                .Skip(skip)
                .Take(take)
                .Select(this.MapEntityToModelExpression);

            return await query.ToArrayAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        protected override IQueryable<GeoEpithet> GetQuery(ITextFilter filter)
        {
            var query = this.Repository.Queryable();

            if (filter != null)
            {
                query = query.Where(
                    n =>
                        (string.IsNullOrEmpty(filter.Text) || n.Name.ToLower().Contains(filter.Text.ToLower())));
            }

            return query.AsNoTracking();
        }
    }
}
