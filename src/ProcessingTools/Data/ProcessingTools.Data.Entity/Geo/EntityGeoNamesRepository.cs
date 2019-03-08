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

    public class EntityGeoNamesRepository : AbstractGeoRepository<GeoName, IGeoName, ITextFilter>, IGeoNamesRepository
    {
        private readonly Func<GeoName, IGeoName> mapEntityToModel;

        public EntityGeoNamesRepository(IGeoRepository<GeoName> repository, IApplicationContext applicationContext)
            : base(repository, applicationContext)
        {
            this.mapEntityToModel = this.MapEntityToModelExpression.Compile();
        }

        protected override Func<GeoName, IGeoName> MapEntityToModel => this.mapEntityToModel;

        protected override Func<IGeoName, GeoName> MapModelToEntity => m => new GeoName
        {
            Id = m.Id,
            Name = m.Name
        };

        private Expression<Func<GeoName, IGeoName>> MapEntityToModelExpression => m => new GeoNameModel
        {
            Id = m.Id,
            Name = m.Name
        };

        public override async Task<IGeoName[]> SelectAsync(ITextFilter filter)
        {
            var query = this.GetQuery(filter)
                .Select(this.MapEntityToModelExpression);

            return await query.ToArrayAsync().ConfigureAwait(false);
        }

        public override async Task<IGeoName[]> SelectAsync(ITextFilter filter, int skip, int take, string sortColumn, SortOrder sortOrder)
        {
            var query = this.GetQuery(filter)
                .OrderByName(sortColumn, sortOrder)
                .Skip(skip)
                .Take(take)
                .Select(this.MapEntityToModelExpression);

            return await query.ToArrayAsync().ConfigureAwait(false);
        }

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

            return await this.UpdateEntityAsync(entity).ConfigureAwait(false);
        }

        protected override IQueryable<GeoName> GetQuery(ITextFilter filter)
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