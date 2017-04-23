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

    public class EntityDistrictsDataService : AbstractGeoDataService<District, IDistrict, IDistrictsFilter>, IEntityDistrictsDataService
    {
        public EntityDistrictsDataService(IGeoRepository<District> repository, IEnvironment environment)
            : base(repository, environment)
        {
        }

        protected override Func<District, IDistrict> MapEntityToModel => m => new ProcessingTools.Geo.Services.Data.Entity.Models.District
        {
            Id = m.Id,
            Name = m.Name,
            CountryId = m.CountryId,
            ProvinceId = m.ProvinceId,
            RegionId = m.RegionId,
            StateId = m.StateId,
            Synonyms = m.Synonyms
                .Select(
                    s => new ProcessingTools.Geo.Services.Data.Entity.Models.DistrictSynonym
                    {
                        Id = s.Id,
                        LanguageCode = s.LanguageCode,
                        Name = s.Name,
                        ParentId = m.Id
                    })
                .ToList<IDistrictSynonym>()
        };

        protected override Func<IDistrict, District> MapModelToEntity => m => new District
        {
            Id = m.Id,
            Name = m.Name,
            CountryId = m.CountryId,
            ProvinceId = m.ProvinceId,
            RegionId = m.RegionId,
            StateId = m.StateId
        };

        protected override IQueryable<District> GetQuery(IDistrictsFilter filter)
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
                         (string.IsNullOrEmpty(filter.Region) || c.Region.Name.ToLower().Contains(filter.Region.ToLower())) &&
                         (string.IsNullOrEmpty(filter.Municipality) || c.Municipalities.Any(s => s.Name.ToLower().Contains(filter.Municipality.ToLower()))) &&
                         (string.IsNullOrEmpty(filter.County) || c.Counties.Any(s => s.Name.ToLower().Contains(filter.County.ToLower()))) &&
                         (string.IsNullOrEmpty(filter.City) || c.Cities.Any(s => s.Name.ToLower().Contains(filter.City.ToLower()))) &&
                         (string.IsNullOrEmpty(filter.Synonym) || c.Synonyms.Any(s => s.Name.ToLower().Contains(filter.Synonym.ToLower()))));
            }

            return query;
        }
    }
}
