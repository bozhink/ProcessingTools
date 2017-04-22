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

    public class EntityDistrictSynonymsDataService : AbstractGeoDataSynonymsService<DistrictSynonym, IDistrictSynonym, IDistrictSynonymsFilter>, IEntityDistrictSynonymsDataService
    {
        public EntityDistrictSynonymsDataService(IGeoRepository<DistrictSynonym> repository, IEnvironment environment)
            : base(repository, environment)
        {
        }

        protected override Func<DistrictSynonym, IDistrictSynonym> MapEntityToModel => s => new ProcessingTools.Geo.Services.Data.Entity.Models.DistrictSynonym
        {
            Id = s.Id,
            Name = s.Name,
            LanguageCode = s.LanguageCode,
            ParentId = s.DistrictId
        };

        protected override Func<IDistrictSynonym, DistrictSynonym> MapModelToEntity => s => new DistrictSynonym
        {
            Id = s.Id,
            Name = s.Name,
            DistrictId = s.ParentId,
            LanguageCode = s.LanguageCode
        };
    }
}
