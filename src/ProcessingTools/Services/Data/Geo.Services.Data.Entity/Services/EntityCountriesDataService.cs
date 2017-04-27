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

    public class EntityCountriesDataService : AbstractGeoDataService<Country, ICountry, ICountriesFilter>, IEntityCountriesDataService
    {
        public EntityCountriesDataService(IGeoRepository<Country> repository, IEnvironment environment)
            : base(repository, environment)
        {
        }

        protected override Func<Country, ICountry> MapEntityToModel => m => new ProcessingTools.Geo.Services.Data.Entity.Models.Country
        {
            Id = m.Id,
            Name = m.Name,
            CallingCode = m.CallingCode,
            Iso639xCode = m.Iso639xCode,
            LanguageCode = m.LanguageCode,
            Continents = m.Continents
                .Select(
                    c => new ProcessingTools.Geo.Services.Data.Entity.Models.Continent
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                .ToList<IContinent>(),
            Synonyms = m.Synonyms
                .Select(
                    s => new ProcessingTools.Geo.Services.Data.Entity.Models.CountrySynonym
                    {
                        Id = s.Id,
                        LanguageCode = s.LanguageCode,
                        Name = s.Name,
                        ParentId = m.Id
                    })
                .ToList<ICountrySynonym>()
        };

        protected override Func<ICountry, Country> MapModelToEntity => m => new Country
        {
            Id = m.Id,
            Name = m.Name,
            CallingCode = m.CallingCode,
            Iso639xCode = m.Iso639xCode,
            LanguageCode = m.LanguageCode
        };

        protected override IQueryable<Country> GetQuery(ICountriesFilter filter)
        {
            var query = this.Repository.Queryable()
                .Include(e => e.Continents)
                .Include(e => e.Synonyms);

            if (filter != null)
            {
                query = query.Where(
                    c => (!filter.Id.HasValue || c.Id == filter.Id) &&
                         (string.IsNullOrEmpty(filter.Name) || c.Name.ToLower().Contains(filter.Name.ToLower())) &&
                         (string.IsNullOrEmpty(filter.Synonym) || c.Synonyms.Any(s => s.Name.ToLower().Contains(filter.Synonym.ToLower()))));
            }

            return query;
        }
    }
}
