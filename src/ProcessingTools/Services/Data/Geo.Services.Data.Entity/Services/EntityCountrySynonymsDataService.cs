namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using System;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Data.Geo.Filters;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;
    using ProcessingTools.Geo.Data.Entity.Contracts;
    using ProcessingTools.Geo.Data.Entity.Models;
    using ProcessingTools.Geo.Services.Data.Entity.Abstractions;
    using ProcessingTools.Geo.Services.Data.Entity.Contracts.Services;

    public class EntityCountrySynonymsDataService : AbstractGeoDataSynonymsService<CountrySynonym, ICountrySynonym, ICountrySynonymsFilter>, IEntityCountrySynonymsDataService
    {
        public EntityCountrySynonymsDataService(IGeoRepository<CountrySynonym> repository, IEnvironment environment)
            : base(repository, environment)
        {
        }

        protected override Func<CountrySynonym, ICountrySynonym> MapEntityToModel => s => new ProcessingTools.Geo.Services.Data.Entity.Models.CountrySynonym
        {
            Id = s.Id,
            Name = s.Name,
            LanguageCode = s.LanguageCode,
            ParentId = s.CountryId
        };

        protected override Func<ICountrySynonym, CountrySynonym> MapModelToEntity => s => new CountrySynonym
        {
            Id = s.Id,
            Name = s.Name,
            CountryId = s.ParentId,
            LanguageCode = s.LanguageCode
        };
    }
}
