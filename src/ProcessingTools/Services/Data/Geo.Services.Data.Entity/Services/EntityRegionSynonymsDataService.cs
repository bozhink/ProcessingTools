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

    public class EntityRegionSynonymsDataService : AbstractGeoDataSynonymsService<RegionSynonym, IRegionSynonym, IRegionSynonymsFilter>, IEntityRegionSynonymsDataService
    {
        public EntityRegionSynonymsDataService(IGeoRepository<RegionSynonym> repository, IEnvironment environment)
            : base(repository, environment)
        {
        }

        protected override Func<RegionSynonym, IRegionSynonym> MapEntityToModel => s => new ProcessingTools.Geo.Services.Data.Entity.Models.RegionSynonym
        {
            Id = s.Id,
            Name = s.Name,
            LanguageCode = s.LanguageCode,
            ParentId = s.RegionId
        };

        protected override Func<IRegionSynonym, RegionSynonym> MapModelToEntity => s => new RegionSynonym
        {
            Id = s.Id,
            Name = s.Name,
            RegionId = s.ParentId,
            LanguageCode = s.LanguageCode
        };
    }
}
