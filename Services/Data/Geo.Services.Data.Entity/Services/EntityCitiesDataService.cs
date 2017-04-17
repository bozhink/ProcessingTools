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

    public class EntityCitiesDataService : AbstractGeoDataService<City, ICity, ICitiesFilter>, IEntityCitiesDataService
    {
        public EntityCitiesDataService(IGeoRepository<City> repository, IEnvironment environment)
            : base(repository, environment)
        {
        }

        protected override Func<City, ICity> MapEntityToModel => m => new ProcessingTools.Geo.Services.Data.Entity.Models.City
        {
            Id = m.Id,
            Name = m.Name,
            CountryId = m.CountryId,
            CountyId = m.CountyId,
            DistrictId = m.DistrictId,
            MunicipalityId = m.MunicipalityId,
            ProvinceId = m.ProvinceId,
            RegionId = m.RegionId,
            StateId = m.StateId,
            Country = new ProcessingTools.Geo.Services.Data.Entity.Models.Country
            {
                Id = m.Country.Id,
                Name = m.Country.Name,
                CallingCode = m.Country.CallingCode,
                Iso639xCode = m.Country.Iso639xCode,
                LanguageCode = m.Country.LanguageCode
            },
            PostCodes = m.PostCodes
                .Select(
                    p => new ProcessingTools.Geo.Services.Data.Entity.Models.PostCode
                    {
                        Id = p.Id,
                        Code = p.Code,
                        Type = p.Type,
                        CityId = m.Id
                    })
                .ToList<IPostCode>(),
            Synonyms = m.Synonyms
                .Select(
                    s => new ProcessingTools.Geo.Services.Data.Entity.Models.CitySynonym
                    {
                        Id = s.Id,
                        LanguageCode = s.LanguageCode,
                        Name = s.Name,
                        ParentId = m.Id
                    })
                .ToList<ICitySynonym>()
        };

        protected override Func<ICity, City> MapModelToEntity => m => new City
        {
            Id = m.Id,
            Name = m.Name,
            CountryId = m.CountryId,
            CountyId = m.CountyId,
            DistrictId = m.DistrictId,
            MunicipalityId = m.MunicipalityId,
            ProvinceId = m.ProvinceId,
            RegionId = m.RegionId,
            StateId = m.StateId
        };

        protected override IQueryable<City> GetQuery(ICitiesFilter filter)
        {
            var query = this.Repository.Queryable()
                .Include(e => e.Country)
                .Include(e => e.PostCodes);

            if (filter != null)
            {
                query = query.Where(
                    c => (!filter.Id.HasValue || c.Id == filter.Id) &&
                         (string.IsNullOrEmpty(filter.Name) || c.Name.ToLower().Contains(filter.Name.ToLower())) &&
                         (string.IsNullOrEmpty(filter.Country) || c.Country.Name.ToLower().Contains(filter.Country.ToLower())) &&
                         (string.IsNullOrEmpty(filter.County) || c.County.Name.ToLower().Contains(filter.County.ToLower())) &&
                         (string.IsNullOrEmpty(filter.District) || c.District.Name.ToLower().Contains(filter.District.ToLower())) &&
                         (string.IsNullOrEmpty(filter.Municipality) || c.Municipality.Name.ToLower().Contains(filter.Municipality.ToLower())) &&
                         (string.IsNullOrEmpty(filter.PostCode) || c.PostCodes.Any(p => p.Code.ToLower().Contains(filter.PostCode.ToLower()))) &&
                         (string.IsNullOrEmpty(filter.Province) || c.Province.Name.ToLower().Contains(filter.Province.ToLower())) &&
                         (string.IsNullOrEmpty(filter.Region) || c.Region.Name.ToLower().Contains(filter.Region.ToLower())) &&
                         (string.IsNullOrEmpty(filter.State) || c.State.Name.ToLower().Contains(filter.State.ToLower())) &&
                         (string.IsNullOrEmpty(filter.Synonym) || c.Synonyms.Any(s => s.Name.ToLower().Contains(filter.Synonym.ToLower()))));
            }

            return query;
        }
    }
}
