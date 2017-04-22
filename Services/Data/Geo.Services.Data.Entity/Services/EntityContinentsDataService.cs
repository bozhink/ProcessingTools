namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using System;
    using System.Linq;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Data.Geo.Filters;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;
    using ProcessingTools.Geo.Data.Entity.Contracts;
    using ProcessingTools.Geo.Data.Entity.Models;
    using ProcessingTools.Geo.Services.Data.Entity.Abstractions;
    using ProcessingTools.Geo.Services.Data.Entity.Contracts.Services;

    public class EntityContinentsDataService : AbstractGeoDataService<Continent, IContinent, IContinentsFilter>, IEntityContinentsDataService
    {
        public EntityContinentsDataService(IGeoRepository<Continent> repository, IEnvironment environment)
            : base(repository, environment)
        {
        }

        protected override Func<Continent, IContinent> MapEntityToModel => m => new ProcessingTools.Geo.Services.Data.Entity.Models.Continent
        {
            Id = m.Id,
            Name = m.Name,
            Synonyms = m.Synonyms
                .Select(s => new ProcessingTools.Geo.Services.Data.Entity.Models.ContinentSynonym
                {
                    Id = s.Id,
                    Name = s.Name,
                    LanguageCode = s.LanguageCode,
                    ParentId = m.Id
                })
                .ToList<IContinentSynonym>()
        };

        protected override Func<IContinent, Continent> MapModelToEntity => m => new Continent
        {
            Id = m.Id,
            Name = m.Name
        };

        protected override IQueryable<Continent> GetQuery(IContinentsFilter filter)
        {
            {
                var query = this.Repository.Queryable();

                if (filter != null)
                {
                    query = query.Where(
                        c => (!filter.Id.HasValue || c.Id == filter.Id) &&
                             (string.IsNullOrEmpty(filter.Name) || c.Name.ToLower().Contains(filter.Name.ToLower())) &&
                             (string.IsNullOrEmpty(filter.Country) || c.Countries.Any(s => s.Name.ToLower().Contains(filter.Country.ToLower()))) &&
                             (string.IsNullOrEmpty(filter.Synonym) || c.Synonyms.Any(s => s.Name.ToLower().Contains(filter.Synonym.ToLower()))));
                }

                return query;
            }
        }
    }
}
