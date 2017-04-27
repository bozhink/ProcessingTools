namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Data.Geo.Filters;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;
    using ProcessingTools.Geo.Data.Entity.Contracts;
    using ProcessingTools.Geo.Data.Entity.Models;
    using ProcessingTools.Geo.Services.Data.Entity.Abstractions;
    using ProcessingTools.Geo.Services.Data.Entity.Contracts.Services;

    public class EntityRegionsDataService : AbstractGeoDataService<Region, IRegion, IRegionsFilter>, IEntityRegionsDataService
    {
        public EntityRegionsDataService(IGeoRepository<Region> repository, IEnvironment environment)
            : base(repository, environment)
        {
        }

        protected override Func<Region, IRegion> MapEntityToModel => m => new ProcessingTools.Geo.Services.Data.Entity.Models.Region
        {
            Id = m.Id,
            Name = m.Name,
            CountryId = m.CountryId,
            ProvinceId = m.ProvinceId,
            StateId = m.StateId,
            Synonyms = m.Synonyms
                .Select(
                    s => new ProcessingTools.Geo.Services.Data.Entity.Models.RegionSynonym
                    {
                        Id = s.Id,
                        LanguageCode = s.LanguageCode,
                        Name = s.Name,
                        ParentId = m.Id
                    })
                .ToList<IRegionSynonym>()
        };

        protected override Func<IRegion, Region> MapModelToEntity => m => new Region
        {
            Id = m.Id,
            Name = m.Name,
            CountryId = m.CountryId,
            ProvinceId = m.ProvinceId,
            StateId = m.StateId
        };

        protected override IQueryable<Region> GetQuery(IRegionsFilter filter)
        {
            var query = this.Repository.Queryable()
                .Include(e => e.Country)
                .Include(e => e.Synonyms);

            if (filter != null)
            {
                query = query.Where(
                    c => (!filter.Id.HasValue || c.Id == filter.Id) &&
                         (string.IsNullOrEmpty(filter.Name) || c.Name.ToLower().Contains(filter.Name.ToLower())) &&
                         (string.IsNullOrEmpty(filter.Country) || c.Country.Name.ToLower().Contains(filter.Country.ToLower())) &&
                         (string.IsNullOrEmpty(filter.State) || c.State.Name.ToLower().Contains(filter.State.ToLower())) &&
                         (string.IsNullOrEmpty(filter.Province) || c.Province.Name.ToLower().Contains(filter.Province.ToLower())) &&
                         (string.IsNullOrEmpty(filter.District) || c.Districts.Any(s => s.Name.ToLower().Contains(filter.District.ToLower()))) &&
                         (string.IsNullOrEmpty(filter.Municipality) || c.Municipalities.Any(s => s.Name.ToLower().Contains(filter.Municipality.ToLower()))) &&
                         (string.IsNullOrEmpty(filter.County) || c.Counties.Any(s => s.Name.ToLower().Contains(filter.County.ToLower()))) &&
                         (string.IsNullOrEmpty(filter.City) || c.Cities.Any(s => s.Name.ToLower().Contains(filter.City.ToLower()))) &&
                         (string.IsNullOrEmpty(filter.Synonym) || c.Synonyms.Any(s => s.Name.ToLower().Contains(filter.Synonym.ToLower()))));
            }

            return query;
        }
    }
}
