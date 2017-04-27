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

    public class EntityMunicipalitySynonymsDataService : AbstractGeoDataSynonymsService<MunicipalitySynonym, IMunicipalitySynonym, IMunicipalitySynonymsFilter>, IEntityMunicipalitySynonymsDataService
    {
        public EntityMunicipalitySynonymsDataService(IGeoRepository<MunicipalitySynonym> repository, IEnvironment environment)
            : base(repository, environment)
        {
        }

        protected override Func<MunicipalitySynonym, IMunicipalitySynonym> MapEntityToModel => s => new ProcessingTools.Geo.Services.Data.Entity.Models.MunicipalitySynonym
        {
            Id = s.Id,
            Name = s.Name,
            LanguageCode = s.LanguageCode,
            ParentId = s.MunicipalityId
        };

        protected override Func<IMunicipalitySynonym, MunicipalitySynonym> MapModelToEntity => s => new MunicipalitySynonym
        {
            Id = s.Id,
            Name = s.Name,
            MunicipalityId = s.ParentId,
            LanguageCode = s.LanguageCode
        };
    }
}
