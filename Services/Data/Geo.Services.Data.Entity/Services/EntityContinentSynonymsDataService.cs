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

    public class EntityContinentSynonymsDataService : AbstractGeoDataSynonymsService<ContinentSynonym, IContinentSynonym, IContinentSynonymsFilter>, IEntityContinentSynonymsDataService
    {
        public EntityContinentSynonymsDataService(IGeoRepository<ContinentSynonym> repository, IEnvironment environment)
            : base(repository, environment)
        {
        }

        protected override Func<ContinentSynonym, IContinentSynonym> MapEntityToModel => s => new ProcessingTools.Geo.Services.Data.Entity.Models.ContinentSynonym
        {
            Id = s.Id,
            Name = s.Name,
            LanguageCode = s.LanguageCode,
            ParentId = s.ContinentId
        };

        protected override Func<IContinentSynonym, ContinentSynonym> MapModelToEntity => s => new ContinentSynonym
        {
            Id = s.Id,
            Name = s.Name,
            ContinentId = s.ParentId,
            LanguageCode = s.LanguageCode
        };
    }
}
