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

    public class EntityCitySynonymsDataService : AbstractGeoDataService<CitySynonym, ICitySynonym, ICitySynonymsFilter>, IEntityCitySynonymsDataService
    {
        public EntityCitySynonymsDataService(IGeoRepository<CitySynonym> repository, IEnvironment environment)
            : base(repository, environment)
        {
        }

        protected override Func<CitySynonym, ICitySynonym> MapEntityToModel => s => new ProcessingTools.Geo.Services.Data.Entity.Models.CitySynonym
        {
            Id = s.Id,
            Name = s.Name,
            LanguageCode = s.LanguageCode,
            ParentId = s.CityId
        };

        protected override Func<ICitySynonym, CitySynonym> MapModelToEntity => s => new CitySynonym
        {
            Id = s.Id,
            Name = s.Name,
            CityId = s.ParentId,
            LanguageCode = s.LanguageCode
        };

        public override async Task<object> UpdateAsync(ICitySynonym model)
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

            entity.LanguageCode = model.LanguageCode;
            entity.Name = model.Name;

            return await base.UpdateEntityAsync(entity);
        }

        protected override IQueryable<CitySynonym> GetQuery(ICitySynonymsFilter filter)
        {
            var query = this.Repository.Queryable();

            if (filter != null)
            {
                query = query.Where(
                    s => (!filter.Id.HasValue || s.Id == filter.Id) &&
                         (!filter.LanguageCode.HasValue || s.LanguageCode == filter.LanguageCode) &&
                         (string.IsNullOrEmpty(filter.Name) || s.Name.ToLower().Contains(filter.Name.ToLower())));
            }

            return query;
        }
    }
}
