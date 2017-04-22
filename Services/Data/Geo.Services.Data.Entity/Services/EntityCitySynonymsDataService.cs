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

    public class EntityCitySynonymsDataService : AbstractGeoDataSynonymsService<CitySynonym, ICitySynonym, ICitySynonymsFilter>, IEntityCitySynonymsDataService
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
    }
}
